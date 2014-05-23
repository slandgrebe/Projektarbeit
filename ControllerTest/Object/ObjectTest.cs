using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ControllerTest
{
    [TestClass]
    public class ObjectTest
    {
        [TestInitialize]
        public void MyClassInitialize()
        {
            View.Window.Init("test", false, 100, 100);
        }

        [TestCleanup]
        public void MyClassCleanup()
        {
            View.Window.Close();
        }

        [TestMethod]
        public void Object_CreateWithPath()
        {
            Controller.Object obj = new Controller.Object();
            obj.PosX = 1;
            obj.PosY = 1;
            obj.PosZ = 1;
            Assert.AreEqual(true, obj.Create(0, "data/models/banana/banana.3ds"), "Objekt konnte nicht erstellt werden.");
        }
        
        [TestMethod]
        public void Object_CreateWithoutPath()
        {
            Controller.Object obj = new Controller.Object();
            obj.PosX = 1;
            obj.PosY = 1;
            obj.PosZ = 1;
            Assert.AreEqual(false, obj.Create(), "Objekt darf nicht erstellt werden können.");
        }

        [TestMethod]
        public void Object_Deserialize()
        {
            Controller.Object obj = new Controller.Object();
            obj.ModelXmlPath = "/data/levels/jungle/models/banana.xml";
            obj.Deserialize();
            Assert.AreEqual("data/models/banana/banana.3ds", obj.Model.Path, "Objekt wurde nicht deserialisiert.");
        }

        [TestMethod]
        public void Object_DeserializeCreate()
        {
            Controller.Object obj = new Controller.Object();
            obj.ModelXmlPath = "/data/levels/jungle/models/banana.xml";
            obj.Deserialize();
            obj.PosZ = 2;
            Assert.AreEqual(true, obj.Create(), "Objekt wurde nicht deserialisiert.");
            Assert.AreEqual(-2, View.Model.PositionZ(obj.Model.Id), "Objekt wurde falsch Positioniert.");
        }

        [TestMethod]
        public void Object_DeserializeCreateZPosition()
        {
            Controller.Object obj = new Controller.Object();
            obj.ModelXmlPath = "/data/levels/jungle/models/banana.xml";
            obj.Deserialize();
            obj.PosZ = 2;
            Assert.AreEqual(true, obj.Create(10), "Objekt wurde nicht deserialisiert.");
            Assert.AreEqual(-12, View.Model.PositionZ(obj.Model.Id), "Objekt wurde falsch Positioniert.");
        }

        [TestMethod]
        public void Object_Dispose()
        {
            Controller.Object obj = new Controller.Object();
            obj.ModelXmlPath = "/data/levels/jungle/models/banana.xml";
            obj.Deserialize();
            obj.Create();
            obj.Dispose();
            Assert.AreEqual((System.UInt32)0, obj.Model.Id, "Model wurde nicht gelöscht.");
        }

        [TestMethod]
        public void Object_CollisionFalse(){
            Controller.Object obj = new Controller.Object();
            obj.ModelXmlPath = "/data/levels/jungle/models/banana.xml";
            obj.Deserialize();
            obj.Create();
            Controller.Player player = new Controller.Player();
            Data.SetBody();
            player.Scale = 0.5f;
            player.Attach = true;
            player.Update();
            Data.SetBody();
            player.Update();
            obj.Model.Position(100, 100, 100);
            View.Camera.PositionCamera(200, 200, 200);
            Data.SetBody();
            player.Update();
            Assert.AreEqual(true, obj.Collision(player, false), "Keine Kolision erwartet.");
        }

        [TestMethod]
        public void Object_CollisionTrue()
        {
            Controller.Object obj = new Controller.Object();
            obj.ModelXmlPath = "/data/levels/jungle/models/banana.xml";
            obj.Deserialize();
            obj.Create();
            Controller.Player player = new Controller.Player();
            Data.SetBody();
            player.Scale = 0.5f;
            player.Attach = false;
            player.Update();
            Data.SetBody();
            player.Update();
            obj.Model.Position(10, 10, -2.25f);
            Assert.AreEqual(true, obj.Collision(player, false), "Kolision erwartet.");
        }

        [TestMethod]
        public void Object_CollisionTrueWithDispose()
        {
            Controller.Object obj = new Controller.Object();
            obj.ModelXmlPath = "/data/levels/jungle/models/banana.xml";
            obj.Deserialize();
            obj.Create();
            Controller.Player player = new Controller.Player();
            Data.SetBody();
            player.Scale = 0.5f;
            player.Attach = false;
            player.Update();
            Data.SetBody();
            player.Update();
            obj.Model.Position(10, 10, -2.25f);
            Assert.AreEqual(true, obj.Collision(player, true), "Kolision erwartet.");
            Assert.AreEqual((System.UInt32)0, obj.Model.Id, "Objekt wurde nicht entfernt.");
        }
    }
}
