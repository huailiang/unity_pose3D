using System;
using System.IO;
using UnityEngine;

/*
 *  human3.6m关节点标注顺序
 *  https://www.stubbornhuang.com/529/
 */

public class MoveJoints : MonoBehaviour
{
    public string path;
    private Vector3[] skeleton;
    Vector3[] pose_joint = new Vector3[17];
    private int idx = 0, max = 100;

    private GameObject Hip, RHip, RKnee, RFoot, LHip, LKnee, LFoot, Spine, Thorax;
    private GameObject Neck, Head, LShoulder, LEblow, LWrist, RShoulder, REblow, RWrist;
    int lineLength = 300;
    float speed = 5f, step = 0f;
    private LineRenderer lineRenderer1, lineRenderer2, lineRenderer3;

    void initData()
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
    }

    void InitObject()
    {
        Hip = GameObject.Find("Hip");               // 臀部
        RHip = GameObject.Find("RHip");             // 右臀部
        RKnee = GameObject.Find("RKnee");           // 右膝盖
        RFoot = GameObject.Find("RFoot");           // 右脚踝
        LHip = GameObject.Find("LHip");             // 左臀部
        LKnee = GameObject.Find("LKnee");           // 左膝盖
        LFoot = GameObject.Find("LFoot");           // 左脚踝
        Spine = GameObject.Find("Spine");           // 脊柱
        Thorax = GameObject.Find("Thorax");         // 胸部
        Neck = GameObject.Find("Neck");             // 颈部
        Head = GameObject.Find("Head");             // 头部
        LShoulder = GameObject.Find("LShoulder");   // 左肩
        LEblow = GameObject.Find("LEblow");         // 左手肘
        LWrist = GameObject.Find("LWrist");         // 左手腕
        RShoulder = GameObject.Find("RShoulder");   // 右肩
        REblow = GameObject.Find("REblow");         // 右手肘
        RWrist = GameObject.Find("RWrist");         // 右手腕

        lineRenderer1 = (LineRenderer)RFoot.GetComponent<LineRenderer>();
        lineRenderer2 = (LineRenderer)LWrist.GetComponent<LineRenderer>();
        lineRenderer3 = (LineRenderer)Hip.GetComponent<LineRenderer>();
    }

    public void Reinit()
    {
        initData();
        idx = 0;
        step = 0;
    }

    void Start()
    {
        InitObject();
        initData();
        lineRenderer1.positionCount = lineLength;
        lineRenderer2.positionCount = lineLength;
        lineRenderer3.positionCount = lineLength;

        lineRenderer1.positionCount = 7;
        lineRenderer2.positionCount = 7;
        lineRenderer3.positionCount = 5;

        Array.Copy(skeleton, idx * 17, pose_joint, 0, 17);
    }

    void Update()
    {
        step += speed * Time.deltaTime;
        Hip.transform.localPosition = Vector3.Lerp(Hip.transform.position, pose_joint[0], step);
        RHip.transform.localPosition = Vector3.Lerp(RHip.transform.position, pose_joint[1], step);
        RKnee.transform.localPosition = Vector3.Lerp(RKnee.transform.position, pose_joint[2], step);
        RFoot.transform.localPosition = Vector3.Lerp(RFoot.transform.position, pose_joint[3], step);
        LHip.transform.localPosition = Vector3.Lerp(LHip.transform.position, pose_joint[4], step);
        LKnee.transform.localPosition = Vector3.Lerp(LKnee.transform.position, pose_joint[5], step);
        LFoot.transform.localPosition = Vector3.Lerp(LFoot.transform.position, pose_joint[6], step);
        Spine.transform.localPosition = Vector3.Lerp(Spine.transform.position, pose_joint[7], step);
        Thorax.transform.localPosition = Vector3.Lerp(Thorax.transform.position, pose_joint[8], step);
        Neck.transform.localPosition = Vector3.Lerp(Neck.transform.position, pose_joint[9], step);
        Head.transform.localPosition = Vector3.Lerp(Head.transform.position, pose_joint[10], step);
        LShoulder.transform.localPosition = Vector3.Lerp(LShoulder.transform.position, pose_joint[11], step);
        LEblow.transform.localPosition = Vector3.Lerp(LEblow.transform.position, pose_joint[12], step);
        LWrist.transform.localPosition = Vector3.Lerp(LWrist.transform.position, pose_joint[13], step);
        RShoulder.transform.localPosition = Vector3.Lerp(RShoulder.transform.position, pose_joint[14], step);
        REblow.transform.localPosition = Vector3.Lerp(REblow.transform.position, pose_joint[15], step);
        RWrist.transform.localPosition = Vector3.Lerp(RWrist.transform.position, pose_joint[16], step);

        lineRenderer1.SetPosition(0, RFoot.transform.position);
        lineRenderer1.SetPosition(1, RKnee.transform.position);
        lineRenderer1.SetPosition(2, RHip.transform.position);
        lineRenderer1.SetPosition(3, Hip.transform.position);
        lineRenderer1.SetPosition(4, LHip.transform.position);
        lineRenderer1.SetPosition(5, LKnee.transform.position);
        lineRenderer1.SetPosition(6, LFoot.transform.position);

        lineRenderer2.SetPosition(0, LWrist.transform.position);
        lineRenderer2.SetPosition(1, LEblow.transform.position);
        lineRenderer2.SetPosition(2, LShoulder.transform.position);
        lineRenderer2.SetPosition(3, Thorax.transform.position);
        lineRenderer2.SetPosition(4, RShoulder.transform.position);
        lineRenderer2.SetPosition(5, REblow.transform.position);
        lineRenderer2.SetPosition(6, RWrist.transform.position);

        lineRenderer3.SetPosition(0, Hip.transform.position);
        lineRenderer3.SetPosition(1, Spine.transform.position);
        lineRenderer3.SetPosition(2, Thorax.transform.position);
        lineRenderer3.SetPosition(3, Neck.transform.position);
        lineRenderer3.SetPosition(4, Head.transform.position);

        if (step >= 1)
        {
            idx++;
            if (idx >= max) idx = 0;
            Array.Copy(skeleton, idx * 17, pose_joint, 0, 17);
        }
    }
}
