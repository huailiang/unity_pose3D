import os
import time

from common.arguments import parse_args
from common.camera import *
from common.generators import UnchunkedGenerator
from common.loss import *
from common.model import *
from common.utils import Timer, evaluate, add_path

from Alphapose.gene_npz import generate_kpts as alpha_pose

os.environ["CUDA_DEVICE_ORDER"] = "PCI_BUS_ID"  # see issue #152
os.environ["CUDA_VISIBLE_DEVICES"] = "0"
metadata = {'layout_name': 'coco', 'num_joints': 17, 'keypoints_symmetry': [[1, 3, 5, 7, 9, 11, 13, 15], [2, 4, 6, 8, 10, 12, 14, 16]]}

add_path()


# record time
def ckpt_time(ckpt=None):
    if not ckpt:
        return time.time()
    else:
        return time.time() - float(ckpt), time.time()


time0 = ckpt_time()


class Skeleton:
    def parents(self):
        return np.array([-1, 0, 1, 2, 0, 4, 5, 0, 7, 8, 9, 8, 11, 12, 8, 14, 15])

    def joints_right(self):
        return [1, 2, 3, 9, 10]


def main(args):
    assert alpha_pose, 'detector_2d should be in ({alpha, hr, open}_pose)'

    # 2D kpts loads or generate
    if not args.input_npz:
        video_name = args.viz_video
        keypoints = alpha_pose(video_name)
    else:
        npz = np.load(args.input_npz)
        keypoints = npz['kpts']  # (N, 17, 2)

    keypoints_symmetry = metadata['keypoints_symmetry']
    kps_left, kps_right = list(keypoints_symmetry[0]), list(keypoints_symmetry[1])
    joints_left, joints_right = list([4, 5, 6, 11, 12, 13]), list([1, 2, 3, 14, 15, 16])

    # normlization keypoints  Suppose using the camera parameter
    keypoints = normalize_screen_coordinates(keypoints[..., :2], w=1000, h=1002)

    model_pos = TemporalModel(17, 2, 17, filter_widths=[3, 3, 3, 3, 3], causal=args.causal, dropout=args.dropout, channels=args.channels, dense=args.dense)

    if torch.cuda.is_available():
        model_pos = model_pos.cuda()

    ckpt, time1 = ckpt_time(time0)
    print('-------------- load data spends {:.2f} seconds'.format(ckpt))

    # load trained model
    chk_filename = os.path.join(args.checkpoint, args.resume if args.resume else args.evaluate)
    print('Loading checkpoint', chk_filename)
    checkpoint = torch.load(chk_filename, map_location=lambda storage, loc: storage)  # 把loc映射到storage
    model_pos.load_state_dict(checkpoint['model_pos'])

    ckpt, time2 = ckpt_time(time1)
    print('-------------- load 3D model spends {:.2f} seconds'.format(ckpt))

    #  Receptive field: 243 frames for args.arc [3, 3, 3, 3, 3]
    receptive_field = model_pos.receptive_field()
    pad = (receptive_field - 1) // 2  # Padding on each side
    causal_shift = 0

    print('Rendering...')
    input_keypoints = keypoints.copy()
    gen = UnchunkedGenerator(None, None, [input_keypoints], pad=pad, causal_shift=causal_shift, augment=args.test_time_augmentation,
                             kps_left=kps_left, kps_right=kps_right, joints_left=joints_left, joints_right=joints_right)
    prediction = evaluate(gen, model_pos, return_predictions=True)

    # save 3D joint points
    np.save('outputs/test_3d_output.npy', prediction, allow_pickle=True)

    rot = np.array([0.14070565, -0.15007018, -0.7552408, 0.62232804], dtype=np.float32)
    prediction = camera_to_world(prediction, R=rot, t=0)

    # We don't have the trajectory, but at least we can rebase the height
    prediction[:, :, 2] -= np.min(prediction[:, :, 2])
    anim_output = {'Reconstruction': prediction}
    input_keypoints = image_coordinates(input_keypoints[..., :2], w=1000, h=1002)

    ckpt, time3 = ckpt_time(time2)
    print('-------------- generate reconstruction 3D data spends {:.2f} seconds'.format(ckpt))

    from common.visualization import render_animation
    render_animation(input_keypoints, anim_output, Skeleton(), 25, args.viz_bitrate, np.array(70., dtype=np.float32), args.viz_output,
                     limit=args.viz_limit, downsample=args.viz_downsample, size=args.viz_size,
                     input_video_path=args.viz_video, viewport=(1000, 1002), input_video_skip=args.viz_skip)

    ckpt, time4 = ckpt_time(time3)
    print('total spend {:2f} second'.format(ckpt))


def inference_video(video_path):
    """
    Do image -> 2d points -> 3d points to video.
    :param video_path: relative to outputs
    :return: None
    """
    args = parse_args()

    dir_name = os.path.dirname(video_path)
    basename = os.path.basename(video_path)
    video_name = basename[:basename.rfind('.')]
    args.viz_video = video_path
    args.viz_output = '{0}/o_{1}.mp4'.format(dir_name, video_name)
    # args.viz_limit = 20
    # args.input_npz = 'outputs/alpha_pose_dance/dance.npz'

    args.evaluate = 'pretrained_h36m_detectron_coco.bin'

    with Timer(video_path):
        main(args)


if __name__ == '__main__':
    inference_video('outputs/v02.mp4')
