using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Controller;
using Model;

namespace ControllerTest
{
    [TestClass]
    public class PlayerTest
    {
        private static Player player;

        [ClassInitialize]
        public static void MyClassInitialize(TestContext testContext)
        {
            View.Window.Init("test", false, 10, 10);
            player = new Player();
        }

        [ClassCleanup]
        public static void MyClassCleanup()
        {
            View.Window.Close();
        }

        [TestMethod]
        public void Player_Update()
        {
            setBody();
            player.Scale = 0.5f;
            Assert.AreEqual(true, player.Update(), "Spieler konnte nicht aktualisiert werden.");
            setBody();
            Assert.AreEqual(0.15f, Body.Instance.Spine.Y, "Spieler wurde in der Y Achse nicht korrekt positioniert.");
            Assert.AreEqual(-2.25f, Body.Instance.Spine.Z, "Spieler wurde in der Z Achse nicht korrekt positioniert.");
        }

        [TestMethod]
        public void Player_GetPositionAttachedToCamera()
        {
            setBody();
            player.Scale = 0.5f;
            player.Attach = true;
            player.Update();
            setBody();
            player.Update();
            View.Camera.PositionCamera(1, 3, 2);
            Assert.AreEqual(-0.25, player.GetPosition(), "Es wurde eine falsche Spielerpositionierung zurück gegeben.");
        }

        [TestMethod]
        public void Player_GetPositionNotAttachedToCamera()
        {
            setBody();
            player.Scale = 0.5f;
            player.Attach = false;
            player.Update();
            setBody();
            player.Update();
            View.Camera.PositionCamera(1, 3, 2);
            Assert.AreEqual(-2.25, player.GetPosition(), "Es wurde eine falsche Spielerpositionierung zurück gegeben.");
        }

        private static void setBody(){
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
