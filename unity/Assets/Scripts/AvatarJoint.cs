using System;
using UnityEngine;

public class AvatarJoint : JointBase
{

    void Start()
    {
        InitData();
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

        if (step >= 1)
        {
            idx++;
            if (idx >= max) idx = 0;
            Array.Copy(skeleton, idx * 17, pose_joint, 0, 17);
        }
    }

}