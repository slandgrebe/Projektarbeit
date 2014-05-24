using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MotionDetection;

namespace JumpAndRun
{
    public static class Data
    {
        public static void SetBody()
        {
            Body.Instance.Z = 1;
            Body.Instance.X = 0;
            Body.Instance.Y = 0;
            Body.Instance.AnkleLeft.X = -1;
            Body.Instance.AnkleLeft.Y = -5;
            Body.Instance.AnkleLeft.Z = 1;
            Body.Instance.AnkleRight.X = 1;
            Body.Instance.AnkleRight.Y = -5;
            Body.Instance.AnkleRight.Z = 1;
            Body.Instance.ElbowLeft.X = -2;
            Body.Instance.ElbowLeft.Y = 1;
            Body.Instance.ElbowLeft.Z = 1;
            Body.Instance.ElbowRight.X = 2;
            Body.Instance.ElbowRight.Y = 1;
            Body.Instance.ElbowRight.Z = 1;
            Body.Instance.FootLeft.X = -1.5f;
            Body.Instance.FootLeft.Y = -5;
            Body.Instance.FootLeft.Z = 1;
            Body.Instance.FootRight.X = 1.5f;
            Body.Instance.FootRight.Y = -5;
            Body.Instance.FootRight.Z = 1;
            Body.Instance.HandRight.X = 2.5f;
            Body.Instance.HandRight.Y = -1;
            Body.Instance.HandRight.Z = 1;
            Body.Instance.HandLeft.X = -2.5f;
            Body.Instance.HandLeft.Y = -1;
            Body.Instance.HandLeft.Z = 1;
            Body.Instance.Head.X = 0;
            Body.Instance.Head.Y = 4;
            Body.Instance.Head.Z = 1;
            Body.Instance.HipCenter.X = 0;
            Body.Instance.HipCenter.Y = 0;
            Body.Instance.HipCenter.Z = 1;
            Body.Instance.HipLeft.X = -1;
            Body.Instance.HipLeft.Y = -1;
            Body.Instance.HipLeft.Z = 1;
            Body.Instance.HipRight.X = 1;
            Body.Instance.HipRight.Y = -1;
            Body.Instance.HipRight.Z = 1;
            Body.Instance.KneeLeft.X = -1;
            Body.Instance.KneeLeft.Y = -3;
            Body.Instance.KneeLeft.Z = 1;
            Body.Instance.KneeRight.X = 1;
            Body.Instance.KneeRight.Y = -3;
            Body.Instance.KneeRight.Z = 1;
            Body.Instance.ShoulderCenter.X = 0;
            Body.Instance.ShoulderCenter.Y = 3;
            Body.Instance.ShoulderCenter.Z = 1;
            Body.Instance.ShoulderLeft.X = -1;
            Body.Instance.ShoulderLeft.Y = 3;
            Body.Instance.ShoulderLeft.Z = 1;
            Body.Instance.ShoulderRight.X = 1;
            Body.Instance.ShoulderRight.Y = 3;
            Body.Instance.ShoulderRight.Z = 1;
            Body.Instance.Spine.X = 0;
            Body.Instance.Spine.Y = 1;
            Body.Instance.Spine.Z = 1;
            Body.Instance.WristLeft.X = -2;
            Body.Instance.WristLeft.Y = -1;
            Body.Instance.WristLeft.Z = 1;
            Body.Instance.WristRight.X = 2;
            Body.Instance.WristRight.Y = -1;
            Body.Instance.WristRight.Z = 1;
        }
    }
}
