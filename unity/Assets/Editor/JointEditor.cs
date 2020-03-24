using UnityEditor;
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
    protected override void OnEnable()
    {
        joint = target as AvatarJoint;
        base.OnEnable();
    }
    
}