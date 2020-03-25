using System;
using UnityEngine;


public class MoveJoints : JointBase
{
    const int lineLength = 300;
    LineRenderer lineRenderer1, lineRenderer2, lineRenderer3;


    void InitObject()
    {
        lineRenderer1 = (LineRenderer) RFoot.GetComponent<LineRenderer>();
        lineRenderer2 = (LineRenderer) LWrist.GetComponent<LineRenderer>();
        lineRenderer3 = (LineRenderer) Hip.GetComponent<LineRenderer>();
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

    void Update()
    {
        UpdatePos();

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
}
