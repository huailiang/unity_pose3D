﻿using UnityEditor;
using UnityEngine;
using System.IO;


public class BaseEditor : Editor
{
    protected string[] paths;
    protected int select;
    protected bool folder;

    protected JointBase joint;

    protected virtual void OnEnable()
    {
        string pref = "Assets/dataset";
        DirectoryInfo dir = new DirectoryInfo(pref);
        var files = dir.GetFiles("*.bytes");
        int len = files.Length;
        paths = new string[len];
        for (int i = 0; i < len; i++)
        {
            paths[i] = files[i].Name.Replace(".bytes", "");
        }
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.Space();
        folder = EditorGUILayout.Foldout(folder, "bones");
        if (folder)
        {
            base.OnInspectorGUI();
        }

        joint.path = "Assets/dataset/" + paths[select] + ".bytes";
        EditorGUILayout.Space();
        select = EditorGUILayout.Popup("model", select, paths);


        if (GUILayout.Button("Make Effect"))
        {
            joint.Reinit();
        }
    }
}

[CustomEditor(typeof(MoveJoints))]
public class JointEditor : BaseEditor
{
    protected override void OnEnable()
    {
        joint = target as MoveJoints;
        base.OnEnable();
    }
}


[CustomEditor(typeof(AvatarJoint))]
public class AvatarEditor : BaseEditor
{
    private Transform hip, rhip, rknee, rfoot, lhip, lknee, lfoot, head, neck, spine;
    private Transform lshoulder, rshoulder, leblow, reblow, lwrist, rwrist, thorax;

    private float size = 0.04f;

    protected override void OnEnable()
    {
        base.OnEnable();
        joint = target as AvatarJoint;
        if (joint.Hip != null)
        {
            hip = joint.Hip.transform;
            rhip = joint.RHip.transform;
            rknee = joint.RKnee.transform;
            rfoot = joint.RFoot.transform;
            lhip = joint.LHip.transform;
            lknee = joint.LKnee.transform;
            lfoot = joint.LFoot.transform;
            head = joint.Head.transform;
            neck = joint.Neck.transform;
            spine = joint.Spine.transform;

            lshoulder = joint.LShoulder.transform;
            rshoulder = joint.RShoulder.transform;
            leblow = joint.LEblow.transform;
            reblow = joint.REblow.transform;
            lwrist = joint.LWrist.transform;
            rwrist = joint.RWrist.transform;
            thorax = joint.Thorax.transform;
        }
    }

    private void OnSceneGUI()
    {
#pragma warning disable 618
        if (hip != null)
        {
            Handles.color = Color.red;
            Handles.DrawSphere(1, hip.position, hip.rotation, size);
            Handles.DrawSphere(2, rhip.position, rhip.rotation, size);
            Handles.DrawSphere(3, rknee.position, rknee.rotation, size);
            Handles.DrawSphere(4, rfoot.position, rfoot.rotation, size);
            Handles.DrawSphere(5, lhip.position, lhip.rotation, size);
            Handles.DrawSphere(6, lknee.position, lknee.rotation, size);
            Handles.DrawSphere(7, lfoot.position, lfoot.rotation, size);

            Handles.color = Color.cyan;
            Handles.DrawSphere(2, neck.position, neck.rotation, size);
            Handles.DrawSphere(3, head.position, head.rotation, size);
            Handles.DrawSphere(4, spine.position, spine.rotation, size);
            Handles.DrawSphere(1, thorax.position, thorax.rotation, size);

            Handles.color = Color.green;
            Handles.DrawSphere(1, lshoulder.position, lshoulder.rotation, size);
            Handles.DrawSphere(1, rshoulder.position, rshoulder.rotation, size);
            Handles.DrawSphere(1, lwrist.position, lwrist.rotation, size);
            Handles.DrawSphere(1, rwrist.position, rwrist.rotation, size);
            Handles.DrawSphere(1, reblow.position, reblow.rotation, size);
            Handles.DrawSphere(1, leblow.position, leblow.rotation, size);

            Handles.color = Color.white;
            Handles.DrawPolyLine(lwrist.position, leblow.position, lshoulder.position, thorax.position, rshoulder.position, reblow.position, rwrist.position);
            Handles.DrawPolyLine(head.position, neck.position, thorax.position, spine.position, hip.position);
            Handles.DrawPolyLine(rfoot.position, rknee.position, rhip.position, hip.position, lhip.position, lknee.position, lfoot.position);
        }
#pragma warning restore 618
    }
}