using System;
using System.IO;
using UnityEngine;

public class MoveJoints : MonoBehaviour
{
    public string path;
    private Vector3[] sktn;
    Vector3[] pose_joint = new Vector3[17];
    private int idx = 0, max = 100;

    private GameObject Nose, LEye, REye, LEar, REar, LShoulder, RShoulder, LElbow, RElbow;
    private GameObject LWrist, RWrist, LHip, RHip, LKnee, RKnee, LAnkle, RAnkle;
    int lineLength = 300;
    float speed = 5f, step = 0f;
    private LineRenderer lineRenderer1, lineRenderer2, lineRenderer3, lineRenderer4;

    void initData()
    {
        using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
        {
            BinaryReader reader = new BinaryReader(fs);
            int x = reader.ReadInt32();
            int y = reader.ReadInt32();
            int z = reader.ReadInt32();
            max = x;

            sktn = new Vector3[x * y];

            int pt = 0;
            for (int i = 0; i < x; i++)
                for (int j = 0; j < y; j++)
                {
                    Vector3 v = Vector3.zero;
                    v.x = reader.ReadSingle();
                    v.y = reader.ReadSingle();
                    v.z = reader.ReadSingle();
                    sktn[pt++] = v;
                }
        }
    }

    void InitObject()
    {
        Nose = GameObject.Find("Nose");
        LEye = GameObject.Find("LEye");
        REye = GameObject.Find("RLEye");
        LEar = GameObject.Find("LEar");
        REar = GameObject.Find("REar");
        LShoulder = GameObject.Find("LShoulder");
        RShoulder = GameObject.Find("RShoulder");
        LElbow = GameObject.Find("LElbow");
        RElbow = GameObject.Find("RElbow");
        LWrist = GameObject.Find("LWrist");
        RWrist = GameObject.Find("RWrist");
        LHip = GameObject.Find("LHip");
        RHip = GameObject.Find("RHip");
        LKnee = GameObject.Find("LKnee");
        RKnee = GameObject.Find("RKnee");
        LAnkle = GameObject.Find("LAnkle");
        RAnkle = GameObject.Find("RAnkle");


        lineRenderer1 = (LineRenderer)Nose.GetComponent("LineRenderer");
        lineRenderer2 = (LineRenderer)LShoulder.GetComponent("LineRenderer");
        lineRenderer3 = (LineRenderer)LHip.GetComponent("LineRenderer");
        lineRenderer4 = (LineRenderer)RHip.GetComponent("LineRenderer");
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
        lineRenderer4.positionCount = lineLength;

        lineRenderer1.positionCount = 7;
        lineRenderer2.positionCount = 7;
        lineRenderer3.positionCount = 4;
        lineRenderer4.positionCount = 4;

        Array.Copy(sktn, idx * 17, pose_joint, 0, 17);
    }

    void Update()
    {
        step += speed * Time.deltaTime;
        Nose.transform.localPosition = Vector3.Lerp(Nose.transform.position, pose_joint[0], step);
        LEye.transform.localPosition = Vector3.Lerp(LEye.transform.position, pose_joint[1], step);
        REye.transform.localPosition = Vector3.Lerp(REye.transform.position, pose_joint[2], step);
        LEar.transform.localPosition = Vector3.Lerp(LEar.transform.position, pose_joint[3], step);
        REar.transform.localPosition = Vector3.Lerp(REar.transform.position, pose_joint[4], step);
        LShoulder.transform.localPosition = Vector3.Lerp(LShoulder.transform.position, pose_joint[5], step);
        RShoulder.transform.localPosition = Vector3.Lerp(RShoulder.transform.position, pose_joint[6], step);
        LElbow.transform.localPosition = Vector3.Lerp(LElbow.transform.position, pose_joint[7], step);
        RElbow.transform.localPosition = Vector3.Lerp(RElbow.transform.position, pose_joint[8], step);
        LWrist.transform.localPosition = Vector3.Lerp(LWrist.transform.position, pose_joint[9], step);
        RWrist.transform.localPosition = Vector3.Lerp(RWrist.transform.position, pose_joint[10], step);
        LHip.transform.localPosition = Vector3.Lerp(LHip.transform.position, pose_joint[11], step);
        RHip.transform.localPosition = Vector3.Lerp(RHip.transform.position, pose_joint[12], step);
        LKnee.transform.localPosition = Vector3.Lerp(LKnee.transform.position, pose_joint[13], step);
        RKnee.transform.localPosition = Vector3.Lerp(RKnee.transform.position, pose_joint[14], step);
        LAnkle.transform.localPosition = Vector3.Lerp(LAnkle.transform.position, pose_joint[15], step);
        RAnkle.transform.localPosition = Vector3.Lerp(RAnkle.transform.position, pose_joint[16], step);

        lineRenderer1.SetPosition(0, LEar.transform.position);
        lineRenderer1.SetPosition(1, LEye.transform.position);
        lineRenderer1.SetPosition(2, Nose.transform.position);
        lineRenderer1.SetPosition(3, REye.transform.position);
        lineRenderer1.SetPosition(4, REar.transform.position);

        lineRenderer2.SetPosition(0, LWrist.transform.position);
        lineRenderer2.SetPosition(1, LElbow.transform.position);
        lineRenderer2.SetPosition(2, LShoulder.transform.position);
        lineRenderer2.SetPosition(3, RShoulder.transform.position);
        lineRenderer2.SetPosition(4, RElbow.transform.position);
        lineRenderer2.SetPosition(5, RWrist.transform.position);

        Vector3 mid = (pose_joint[5] + pose_joint[6]) / 2;

        lineRenderer3.SetPosition(0, mid);
        lineRenderer3.SetPosition(1, LHip.transform.position);
        lineRenderer3.SetPosition(2, LKnee.transform.position);
        lineRenderer3.SetPosition(3, LAnkle.transform.position);

        lineRenderer4.SetPosition(0, mid);
        lineRenderer4.SetPosition(1, RHip.transform.position);
        lineRenderer4.SetPosition(2, RKnee.transform.position);
        lineRenderer4.SetPosition(3, RAnkle.transform.position);
        if (step >= 1)
        {
            idx++;
            if (idx >= max) idx = 0;
            Array.Copy(sktn, idx * 17, pose_joint, 0, 17);
        }
    }
}
