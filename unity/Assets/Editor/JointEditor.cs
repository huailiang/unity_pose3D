using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(MoveJoints))]
public class JointEditor : Editor
{

    MoveJoints joint;
    TextAsset txa;
    const string defPath = "Assets/dataset/kun_3d_output.bytes";


    private void OnEnable()
    {
        joint = target as MoveJoints;
        if (txa == null && string.IsNullOrEmpty(joint.path))
        {
            joint.path = defPath;
        }
        if (!string.IsNullOrEmpty(joint.path))
        {
            txa = AssetDatabase.LoadAssetAtPath<TextAsset>(joint.path);
        }
    }


    public override void OnInspectorGUI()
    {
        EditorGUILayout.Space();
        txa = (TextAsset)EditorGUILayout.ObjectField(txa, typeof(TextAsset), false);
        string path = AssetDatabase.GetAssetPath(txa);
        EditorGUILayout.LabelField(path);
        joint.path = path;
    }

}
