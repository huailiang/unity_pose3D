using System;
using UnityEngine;


public class MoveJoints : JointBase
{
    int lineLength = 300;
    LineRenderer lineRenderer1, lineRenderer2, lineRenderer3;

   
    void InitObject()
    {
        lineRenderer1 = (LineRenderer)RFoot.GetComponent<LineRenderer>();
        lineRenderer2 = (LineRenderer)LWrist.GetComponent<LineRenderer>();
        lineRenderer3 = (LineRenderer)Hip.GetComponent<LineRenderer>();
    }
    

    void Start()
    {
        InitObject();
        InitData();
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
