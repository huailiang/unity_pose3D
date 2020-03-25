using System;
using UnityEngine;

public class AvatarJoint : JointBase
{

    public class AvatarTree
    {
        public Transform transf;
        public AvatarTree[] childs;
        public AvatarTree parent;
        public int idx;  // pose_joint's index

        public AvatarTree(Transform tf, int count, int idx, AvatarTree parent = null)
        {
            this.transf = tf;
            this.parent = parent;
            this.idx = idx;
            if (count > 0)
            {
                childs = new AvatarTree[count];
            }
        }

        public Vector3 GetDir()
        {
            if (parent != null)
            {
                return transf.position - parent.transf.position;
            }
            return Vector3.up;
        }
    }

    private AvatarTree tree, lhip, rhip, spine, lknee, lfoot, rknee, rfoot, head;
    private AvatarTree thorax, neck, lshoulder, leblow, lwrist, rshoulder, reblow, rwrist;

    protected override float speed { get { return 5f; } }

    void Start()
    {
        InitData();
        BuildTree();
    }

    void BuildTree()
    {
        tree = new AvatarTree(Hip.transform, 3, 0);
        lhip = tree.childs[0] = new AvatarTree(LHip.transform, 1, 4, tree);
        rhip = tree.childs[1] = new AvatarTree(RHip.transform, 1, 1, tree);
        spine = tree.childs[2] = new AvatarTree(Spine.transform, 1, 7, tree);

        lknee = lhip.childs[0] = new AvatarTree(LKnee.transform, 1, 5, lhip);
        lfoot = lknee.childs[0] = new AvatarTree(LFoot.transform, 0, 6, lknee);

        rknee = rhip.childs[0] = new AvatarTree(RKnee.transform, 1, 2, rhip);
        rfoot = rknee.childs[0] = new AvatarTree(RFoot.transform, 0, 3, rknee);

        thorax = spine.childs[0] = new AvatarTree(Thorax.transform, 3, 8, spine);
        neck = thorax.childs[0] = new AvatarTree(Neck.transform, 1, 9, thorax);
        head = neck.childs[0] = new AvatarTree(Head.transform, 0, 10, neck);
        lshoulder = thorax.childs[1] = new AvatarTree(LShoulder.transform, 1, 11, thorax);
        leblow = lshoulder.childs[0] = new AvatarTree(LEblow.transform, 1, 12, lshoulder);
        lwrist = leblow.childs[0] = new AvatarTree(LWrist.transform, 0, 13, leblow);
        rshoulder = thorax.childs[2] = new AvatarTree(RShoulder.transform, 1, 14, thorax);
        reblow = rshoulder.childs[0] = new AvatarTree(REblow.transform, 1, 15, rshoulder);
        rwrist = reblow.childs[0] = new AvatarTree(RWrist.transform, 0, 16, reblow);
    }


    protected override void LerpUpdate(float lerp)
    {
        //UpdateTree(tree, lerp);

        UpdateBone(lwrist, lerp);
        UpdateBone(rwrist, lerp);
        UpdateBone(leblow, lerp);
        UpdateBone(reblow, lerp);
        UpdateBone(lfoot, lerp);
        UpdateBone(rfoot, lerp);
        UpdateBone(lknee, lerp);
        UpdateBone(rknee, lerp);
        UpdateBone(lhip, lerp);
        UpdateBone(rhip, lerp);
        UpdateBone(spine, lerp);
        UpdateBone(thorax, lerp);
    }


    private void UpdateTree(AvatarTree tree, float lerp)
    {
        if (tree.parent != null)
        {
            UpdateBone(tree, lerp);
        }
        if (tree.childs != null)
        {
            for (int i = 0; i < tree.childs.Length; i++)
                UpdateTree(tree.childs[i], lerp);
        }
    }

    private void UpdateBone(AvatarTree tree, float lerp)
    {
        var dir1 = tree.GetDir();
        var dir2 = pose_joint[tree.idx] - pose_joint[tree.parent.idx];
        dir2.y = -dir2.y;
        Quaternion rot = Quaternion.FromToRotation(dir1, dir2);
        Quaternion rot1 = tree.parent.transf.rotation;
        tree.parent.transf.rotation = Quaternion.Lerp(rot1, rot * rot1, lerp);
    }

}