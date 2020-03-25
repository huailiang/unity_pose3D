using UnityEngine;

public class AvatarJoint : JointBase
{
    public class  AvatarTree
    {
        public Transform root;
        public AvatarTree[] childs;
        public AvatarTree parent;

        public AvatarTree(Transform tf, int count)
        {
            root = tf;
            if (count > 0)
            {
                childs = new AvatarTree[count];
                for (int i = 0; i < count; i++)
                {
                    childs[i].parent = this;
                }
            }
        }
    }

    private AvatarTree tree;

    void Start()
    {
        InitData();
        // BuildTree();
    }

    void BuildTree()
    {
        tree = new AvatarTree(Hip.transform,3);
        var lhip= tree.childs[0] = new AvatarTree(LHip.transform,1);
        var rhip = tree.childs[1] = new AvatarTree(RHip.transform, 1);
        var spine = tree.childs[2] = new AvatarTree(Spine.transform, 1);

        var lknee = lhip.childs[0] = new AvatarTree(LKnee.transform, 1);
        var lfoot = lknee.childs[0] = new AvatarTree(LFoot.transform, 0);

        var rknee = rhip.childs[0] = new AvatarTree(RKnee.transform, 1);
        var rfoot = rknee.childs[0] = new AvatarTree(RFoot.transform, 0);

        var thorax = spine.childs[0] = new AvatarTree(Thorax.transform, 3);
        var neck = thorax.childs[0] = new AvatarTree(Neck.transform, 1);
        var head = neck.childs[0] = new AvatarTree(Head.transform, 0);
        var lshoulder = thorax.childs[1] = new AvatarTree(LShoulder.transform, 1);
        var leblow = lshoulder.childs[0] = new AvatarTree(LEblow.transform, 1);
        var lwrist = leblow.childs[0] = new AvatarTree(LWrist.transform, 0);
        var rshoulder = thorax.childs[2] = new AvatarTree(RShoulder.transform, 1);
        var reblow = rshoulder.childs[0] = new AvatarTree(REblow.transform, 1);
        var rwrist = reblow.childs[0] = new AvatarTree(RWrist.transform, 0);
    }

    void Update()
    {
    }
}
