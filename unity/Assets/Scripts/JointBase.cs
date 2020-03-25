using System;
using System.IO;
using UnityEngine;

/*
 *  human3.6m关节点标注顺序
 *  https://www.stubbornhuang.com/529/
 */

public class JointBase : MonoBehaviour
{
    [SerializeField]
    [HideInInspector]
    public string path;

    protected Vector3[] pose_joint = new Vector3[17];

    public GameObject Hip;         // 臀部
    public GameObject RHip;        // 右臀部
    public GameObject RKnee;       // 右膝盖
    public GameObject RFoot;       // 右脚踝
    public GameObject LHip;        // 左臀部
    public GameObject LKnee;       // 左膝盖
    public GameObject LFoot;       // 左脚踝
    public GameObject Spine;       // 脊柱
    public GameObject Thorax;      // 胸部
    public GameObject Neck;        // 颈部
    public GameObject Head;        // 头部
    public GameObject LShoulder;   // 左肩
    public GameObject LEblow;      // 左手肘
    public GameObject LWrist;      // 左手腕
    public GameObject RShoulder;   // 右肩
    public GameObject REblow;      // 右手肘
    public GameObject RWrist;      // 右手腕

    protected virtual float speed { get { return 5f; } }

    protected float lerp = 0f;
    protected Vector3[] skeleton;
    private int idx = 0, max = 100;

    protected void InitData()
    {
        using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
        {
            BinaryReader reader = new BinaryReader(fs);
            int x = reader.ReadInt32();
            int y = reader.ReadInt32();
            int z = reader.ReadInt32();
            max = x;

            skeleton = new Vector3[x * y];

            int pt = 0;
            for (int i = 0; i < x; i++)
                for (int j = 0; j < y; j++)
                {
                    Vector3 v = Vector3.zero;
                    v.x = reader.ReadSingle();
                    v.y = reader.ReadSingle();
                    v.z = reader.ReadSingle();
                    skeleton[pt++] = v;
                }
        }
        Array.Copy(skeleton, idx * 17, pose_joint, 0, 17);
    }


    public void Reinit()
    {
        idx = 0;
        lerp = 0;
        InitData();
    }


    protected virtual void Update()
    {
        lerp += speed * Time.deltaTime;
        LerpUpdate(lerp);
        if (lerp >= 1)
        {
            if (++idx >= max) idx = 0;
            Array.Copy(skeleton, idx * 17, pose_joint, 0, 17);
        }
    }


    protected virtual void LerpUpdate(float lerp)
    {

    }

}
