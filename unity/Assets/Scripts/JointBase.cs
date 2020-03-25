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
    private Vector3 offset;

    public GameObject Hip;         // 臀部
    public GameObject RHip;        // 右臀部
    public GameObject RKnee;       // 右膝盖
    public GameObject RFoot;       // 右脚踝
    public GameObject LHip;        // 左臀部
    public GameObject LKnee;       // 左膝盖
    public GameObject LFoot;       // 左脚踝
    public GameObject Spine;       // 脊柱
    public GameObject Thorax;      // 胸部
    public GameObject Neck;       // 颈部
    public GameObject Head;        // 头部
    public GameObject LShoulder;   // 左肩
    public GameObject LEblow;      // 左手肘
    public GameObject LWrist;      // 左手腕
    public GameObject RShoulder;   // 右肩
    public GameObject REblow;      // 右手肘
    public GameObject RWrist;      // 右手腕


    protected float speed = 5f, step = 0f;
    protected Vector3[] skeleton;
    protected int idx = 0, max = 100;

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
        offset = transform.position;
    }


    public void Reinit()
    {
        InitData();
        idx = 0;
        step = 0;
    }


    protected void UpdatePos()
    {
        step += speed * Time.deltaTime;
        Hip.transform.position = Vector3.Lerp(Hip.transform.position, pose_joint[0], step) + offset;
        RHip.transform.position = Vector3.Lerp(RHip.transform.position, pose_joint[1], step) + offset;
        RKnee.transform.position = Vector3.Lerp(RKnee.transform.position, pose_joint[2], step) + offset;
        RFoot.transform.position = Vector3.Lerp(RFoot.transform.position, pose_joint[3], step) + offset;
        LHip.transform.position = Vector3.Lerp(LHip.transform.position, pose_joint[4], step) + offset;
        LKnee.transform.position = Vector3.Lerp(LKnee.transform.position, pose_joint[5], step) + offset;
        LFoot.transform.position = Vector3.Lerp(LFoot.transform.position, pose_joint[6], step) + offset;
        Spine.transform.position = Vector3.Lerp(Spine.transform.position, pose_joint[7], step) + offset;
        Thorax.transform.position = Vector3.Lerp(Thorax.transform.position, pose_joint[8], step) + offset;
        Neck.transform.position = Vector3.Lerp(Neck.transform.position, pose_joint[9], step) + offset;
        Head.transform.position = Vector3.Lerp(Head.transform.position, pose_joint[10], step) + offset;
        LShoulder.transform.position = Vector3.Lerp(LShoulder.transform.position, pose_joint[11], step) + offset;
        LEblow.transform.position = Vector3.Lerp(LEblow.transform.position, pose_joint[12], step) + offset;
        LWrist.transform.position = Vector3.Lerp(LWrist.transform.position, pose_joint[13], step) + offset;
        RShoulder.transform.position = Vector3.Lerp(RShoulder.transform.position, pose_joint[14], step) + offset;
        REblow.transform.position = Vector3.Lerp(REblow.transform.position, pose_joint[15], step) + offset;
        RWrist.transform.position = Vector3.Lerp(RWrist.transform.position, pose_joint[16], step) + offset;
        
        if (step >= 1)
        {
            if (++idx >= max) idx = 0;
            Array.Copy(skeleton, idx * 17, pose_joint, 0, 17);
        }
    }

}
