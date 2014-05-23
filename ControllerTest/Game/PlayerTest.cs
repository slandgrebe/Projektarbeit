using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Controller;
using Model;

namespace ControllerTest
{
    [TestClass]
    public class PlayerTest
    {
        private Player player;

        [TestInitialize]
        public void MyClassInitialize()
        {
            View.Window.Init("test", false, 100, 100);
            player = new Player();
        }

        [TestCleanup]
        public void MyClassCleanup()
        {
            View.Window.Close();
        }

        [TestMethod]
        public void Player_Update()
        {
            Data.SetBody();
            player.Scale = 0.5f;
            Assert.AreEqual(true, player.Update(), "Spieler konnte nicht aktualisiert werden.");
            Data.SetBody();
            Assert.AreEqual(0.15f, Body.Instance.Spine.Y, "Spieler wurde in der Y Achse nicht korrekt positioniert.");
            Assert.AreEqual(-2.25f, Body.Instance.Spine.Z, "Spieler wurde in der Z Achse nicht korrekt positioniert.");
        }

        [TestMethod]
        public void Player_GetPositionAttachedToCamera()
        {
            Data.SetBody();
            player.Scale = 0.5f;
            player.Attach = true;
            player.Update();
            Data.SetBody();
            player.Update();
            View.Camera.PositionCamera(1, 3, 2);
            Assert.AreEqual(-0.25, player.GetPosition(), "Es wurde eine falsche Spielerpositionierung zurück gegeben.");
        }

        [TestMethod]
        public void Player_GetPositionNotAttachedToCamera()
        {
            Data.SetBody();
            player.Scale = 0.5f;
            player.Attach = false;
            player.Update();
            Data.SetBody();
            player.Update();
            View.Camera.PositionCamera(1, 3, 2);
            Assert.AreEqual(-2.25, player.GetPosition(), "Es wurde eine falsche Spielerpositionierung zurück gegeben.");
        }
    }
}
