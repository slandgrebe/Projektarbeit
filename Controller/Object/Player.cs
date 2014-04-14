using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Controller
{
    class Player
    {
        public float Scale { private get; set; }
        public bool Attach { private get; set; }
        
        private Model Head = new Model("Resource Files/Models/Player/Head.3ds");
        private Model Torso = new Model("Resource Files/Models/Player/Torso.3ds");
        private Model UpperarmLeft = new Model("Resource Files/Models/Player/UpperArm.3ds");
        private Model UpperarmRight = new Model("Resource Files/Models/Player/UpperArm.3ds");
        private Model ForearmLeft = new Model("Resource Files/Models/Player/ForeArm.3ds");
        private Model ForearmRight = new Model("Resource Files/Models/Player/ForeArm.3ds");
        private Model ThighlegLeft = new Model("Resource Files/Models/Player/ThighLeg.3ds");
        private Model ThighlegRight = new Model("Resource Files/Models/Player/ThighLeg.3ds");
        private Model LowerlegLeft = new Model("Resource Files/Models/Player/LowerLeg.3ds");
        private Model LowerlegRight = new Model("Resource Files/Models/Player/LowerLeg.3ds");

        public void Update()
        {
            Body.Instance.ZModifikator(Body.Instance.Spine.Z + 3);
            Body.Instance.YModifikator(Body.Instance.Spine.Y -1.4f);
            ScalePlayer();
            Alignment();
            AttachToCamera();
        }

        private void Alignment()
        {
            Head.Position(Body.Instance.Head.X, Body.Instance.Head.Y, Body.Instance.Head.Z);
            Torso.Alignment(Body.Instance.HipCenter.X, Body.Instance.HipCenter.Y, Body.Instance.HipCenter.Z,
                Body.Instance.ShoulderCenter.X, Body.Instance.ShoulderCenter.Y, Body.Instance.ShoulderCenter.Z);
            UpperarmLeft.Alignment(Body.Instance.ShoulderLeft.X, Body.Instance.ShoulderLeft.Y, Body.Instance.ShoulderLeft.Z,
                Body.Instance.ElbowLeft.X, Body.Instance.ElbowLeft.Y, Body.Instance.ElbowLeft.Z);
            UpperarmRight.Alignment(Body.Instance.ShoulderRight.X, Body.Instance.ShoulderRight.Y, Body.Instance.ShoulderRight.Z,
                Body.Instance.ElbowRight.X, Body.Instance.ElbowRight.Y, Body.Instance.ElbowRight.Z);
            ForearmLeft.Alignment(Body.Instance.ElbowLeft.X, Body.Instance.ElbowLeft.Y, Body.Instance.ElbowLeft.Z,
                Body.Instance.WristLeft.X, Body.Instance.WristLeft.Y, Body.Instance.WristLeft.Z);
            ForearmRight.Alignment(Body.Instance.ElbowRight.X, Body.Instance.ElbowRight.Y, Body.Instance.ElbowRight.Z,
                Body.Instance.WristRight.X, Body.Instance.WristRight.Y, Body.Instance.WristRight.Z);
            ThighlegLeft.Alignment(Body.Instance.HipLeft.X, Body.Instance.HipLeft.Y, Body.Instance.HipLeft.Z,
                Body.Instance.KneeLeft.X, Body.Instance.KneeLeft.Y, Body.Instance.KneeLeft.Z);
            ThighlegRight.Alignment(Body.Instance.HipRight.X, Body.Instance.HipRight.Y, Body.Instance.HipRight.Z,
                Body.Instance.KneeRight.X, Body.Instance.KneeRight.Y, Body.Instance.KneeRight.Z);
            LowerlegLeft.Alignment(Body.Instance.KneeLeft.X, Body.Instance.KneeLeft.Y, Body.Instance.KneeLeft.Z,
                Body.Instance.AnkleLeft.X, Body.Instance.AnkleLeft.Y, Body.Instance.AnkleLeft.Z);
            LowerlegRight.Alignment(Body.Instance.KneeRight.X, Body.Instance.KneeRight.Y, Body.Instance.KneeRight.Z,
                Body.Instance.AnkleRight.X, Body.Instance.AnkleRight.Y, Body.Instance.AnkleRight.Z);
        }

        private void ScalePlayer()
        {
            Body.Instance.Scale(Scale);


            Head.Scale(Scale / 3.333f);
            Torso.Scale(Scale);
            UpperarmLeft.Scale(Scale / 1.333f);
            UpperarmRight.Scale(Scale / 1.333f);
            ForearmLeft.Scale(Scale / 1.1f);
            ForearmRight.Scale(Scale / 1.1f);
            ThighlegLeft.Scale(Scale);
            ThighlegRight.Scale(Scale);
            LowerlegLeft.Scale(Scale);
            LowerlegRight.Scale(Scale);
        }

        private void AttachToCamera()
        {
            Head.AttachToCamera(Attach);
            UpperarmLeft.AttachToCamera(Attach);
            UpperarmRight.AttachToCamera(Attach);
            ForearmLeft.AttachToCamera(Attach);
            ForearmRight.AttachToCamera(Attach);
            Torso.AttachToCamera(Attach);
            ThighlegLeft.AttachToCamera(Attach);
            ThighlegRight.AttachToCamera(Attach);
            LowerlegLeft.AttachToCamera(Attach);
            LowerlegRight.AttachToCamera(Attach);
        }
    }
}
