using UnityEditor;
using UnityEngine;
using System.IO;


[CustomEditor(typeof(MoveJoints))]
public class JointEditor : Editor
{

    MoveJoints joint;
    string[] paths;
    int select;


    private void OnEnable()
    {
        joint = target as MoveJoints;
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
        select = EditorGUILayout.Popup("model", select, paths);
        joint.path = "Assets/dataset/" + paths[select] + ".bytes";

        if (GUILayout.Button("Make Effect"))
        {
            joint.Reinit();
        }
    }

}