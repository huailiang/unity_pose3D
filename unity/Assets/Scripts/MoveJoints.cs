using System;
using UnityEngine;


public class MoveJoints : JointBase
{
    const int lineLength = 300;
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
    }

    protected override void Update()
    {
        base.Update();
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
    }

    protected override void LerpUpdate(float lerp)
    {
        Hip.transform.position = Vector3.Lerp(Hip.transform.position, pose_joint[0], lerp);
        RHip.transform.position = Vector3.Lerp(RHip.transform.position, pose_joint[1], lerp);
        RKnee.transform.position = Vector3.Lerp(RKnee.transform.position, pose_joint[2], lerp);
        RFoot.transform.position = Vector3.Lerp(RFoot.transform.position, pose_joint[3], lerp);
        LHip.transform.position = Vector3.Lerp(LHip.transform.position, pose_joint[4], lerp);
        LKnee.transform.position = Vector3.Lerp(LKnee.transform.position, pose_joint[5], lerp);
        LFoot.transform.position = Vector3.Lerp(LFoot.transform.position, pose_joint[6], lerp);
        Spine.transform.position = Vector3.Lerp(Spine.transform.position, pose_joint[7], lerp);
        Thorax.transform.position = Vector3.Lerp(Thorax.transform.position, pose_joint[8], lerp);
        Neck.transform.position = Vector3.Lerp(Neck.transform.position, pose_joint[9], lerp);
        Head.transform.position = Vector3.Lerp(Head.transform.position, pose_joint[10], lerp);
        LShoulder.transform.position = Vector3.Lerp(LShoulder.transform.position, pose_joint[11], lerp);
        LEblow.transform.position = Vector3.Lerp(LEblow.transform.position, pose_joint[12], lerp);
        LWrist.transform.position = Vector3.Lerp(LWrist.transform.position, pose_joint[13], lerp);
        RShoulder.transform.position = Vector3.Lerp(RShoulder.transform.position, pose_joint[14], lerp);
        REblow.transform.position = Vector3.Lerp(REblow.transform.position, pose_joint[15], lerp);
        RWrist.transform.position = Vector3.Lerp(RWrist.transform.position, pose_joint[16], lerp);
    }
}
