#!/usr/bin/env python
# -*- coding: utf-8 -*-
# @Author: penghuailiang
# @Date  : 2020/3/23


from common.quaternion import *
from common.utils import *
import struct


def camera_to_world(X, R, t):
    return wrap(qrot, np.tile(R, (*X.shape[:-1], 1)), X) + t


def writef(f, v):
    p = struct.pack("f", v)
    f.write(p)


def writei(f, v):
    p = struct.pack("I", v)
    f.write(p)


name = "v02_3d_output"

prediction = np.load("outputs/{0}.npy".format(name))

rot = np.array([0.14070565, -0.15007018, -0.7552408, 0.62232804], dtype=np.float32)
prediction = camera_to_world(prediction, R=rot, t=0)

# We don't have the trajectory, but at least we can rebase the height
prediction[:, :, 2] -= np.min(prediction[:, :, 2])
anim_output = {'Reconstruction': prediction}

shape = prediction.shape
print(shape[0], shape[1], shape[2])

f = open("outputs/{0}.bytes".format(name), "wb")
writei(f, shape[0])
writei(f, shape[1])
writei(f, shape[2])
for i in (range(shape[0])):
    for j in range(shape[1]):
        stra = ""
        for k in range(shape[2]):
            p = str(prediction[i, j, k])
            stra = stra + p + " "
            writef(f, prediction[i, j, k])
        print(stra)
f.close()
