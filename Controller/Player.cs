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
        public float Scale {private get; set;}
        
        private Model Head = new Model("Resource Files/Models/Player/Head.obj");
        private Model Torso = new Model("Resource Files/Models/Player/Torso.obj");
        private Model UpperarmLeft = new Model("Resource Files/Models/Player/UpperArm.obj");
        private Model UpperarmRight = new Model("Resource Files/Models/Player/UpperArm.obj");
        private Model ForearmLeft = new Model("Resource Files/Models/Player/ForeArm.obj");
        private Model ForearmRight = new Model("Resource Files/Models/Player/ForeArm.obj");
        private Model ThighlegLeft = new Model("Resource Files/Models/Player/ThighLeg.obj");
        private Model ThighlegRight = new Model("Resource Files/Models/Player/ThighLeg.obj");
        private Model LowerlegLeft = new Model("Resource Files/Models/Player/LowerLeg.obj");
        private Model LowerlegRight = new Model("Resource Files/Models/Player/LowerLeg.obj");

        public void Update()
        {
            Body.Instance.ZModifikator(Body.Instance.Spine.Z * -1 - 2); //Kamera Position mit einrechnen
            Body.Instance.Scale(Scale);
            Head.Position(Body.Instance.Head.X,Body.Instance.Head.Y,Body.Instance.Head.Z);
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
    }
}
