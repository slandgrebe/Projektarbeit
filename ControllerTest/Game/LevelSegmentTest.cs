using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Controller;

namespace ControllerTest
{
    [TestClass]
    public class LevelSegmentTest
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
        public void LevelSegment_AddObstacle()
        {
            LevelSegment ls = new LevelSegment();
            Controller.Object obj = new Controller.Object();
            ls.AddObstacle(obj);
            Assert.AreEqual(1, ls.obstacles.Count, "Objekt wurde nicht hinzugefügt.");
        }

        [TestMethod]
        public void LevelSegment_AddScore()
        {
            LevelSegment ls = new LevelSegment();
            Controller.Object obj = new Controller.Object();
            ls.AddScore(obj);
            Assert.AreEqual(1, ls.scores.Count, "Objekt wurde nicht hinzugefügt.");
        }

        [TestMethod]
        public void LevelSegment_AddObject()
        {
            LevelSegment ls = new LevelSegment();
            Controller.Object obj = new Controller.Object();
            ls.AddObject(obj);
            Assert.AreEqual(1, ls.objects.Count, "Objekt wurde nicht hinzugefügt.");
        }

        [TestMethod]
        public void LevelSegment_Create()
        {
            LevelSegment ls = new LevelSegment();

            Controller.Object obstacle = new Controller.Object();
            obstacle.ModelXmlPath = "/data/levels/jungle/models/banana.xml";
            obstacle.Deserialize();
            Controller.Object score = new Controller.Object();
            score.ModelXmlPath = "/data/levels/jungle/models/banana.xml";
            score.Deserialize();
            Controller.Object obj = new Controller.Object();
            obj.ModelXmlPath = "/data/levels/jungle/models/banana.xml";
            obj.Deserialize();

            ls.AddObstacle(obstacle);
            ls.AddScore(score);
            ls.AddObject(obj);

            Assert.AreEqual(true, ls.Create(0), "Segment wurde nicht erstellt.");
        }

        [TestMethod]
        public void LevelSegment_Deserialize()
        {
            LevelSegment ls = new LevelSegment();
            Controller.Object obstacle = new Controller.Object();

            obstacle.ModelXmlPath = "/data/levels/jungle/models/banana.xml";
            Controller.Object score = new Controller.Object();
            score.ModelXmlPath = "/data/levels/jungle/models/banana.xml";
            Controller.Object obj = new Controller.Object();
            obj.ModelXmlPath = "/data/levels/jungle/models/banana.xml";

            ls.AddObstacle(obstacle);
            ls.AddScore(score);
            ls.AddObject(obj);

            ls.Deserialize();

            Assert.AreEqual("data/models/banana/banana.3ds", ls.obstacles[0].Model.Path, "Hinternis wurde nicht deserialisiert.");
            Assert.AreEqual("data/models/banana/banana.3ds", ls.scores[0].Model.Path, "Score wurde nicht deserialisiert.");
            Assert.AreEqual("data/models/banana/banana.3ds", ls.objects[0].Model.Path, "Objekt wurde nicht deserialisiert.");
        }

        [TestMethod]
        public void LevelSegment_Dispose()
        {
            LevelSegment ls = new LevelSegment();
            Controller.Object obstacle = new Controller.Object();

            obstacle.ModelXmlPath = "/data/levels/jungle/models/banana.xml";
            Controller.Object score = new Controller.Object();
            score.ModelXmlPath = "/data/levels/jungle/models/banana.xml";
            Controller.Object obj = new Controller.Object();
            obj.ModelXmlPath = "/data/levels/jungle/models/banana.xml";

            ls.AddObstacle(obstacle);
            ls.AddScore(score);
            ls.AddObject(obj);

            ls.Deserialize();
            ls.Create(0);
            ls.Dispose();

            Assert.AreEqual((System.UInt32)0, ls.obstacles[0].Model.Id, "Hinternis wurde nicht deserialisiert.");
            Assert.AreEqual((System.UInt32)0, ls.scores[0].Model.Id, "Score wurde nicht deserialisiert.");
            Assert.AreEqual((System.UInt32)0, ls.objects[0].Model.Id, "Objekt wurde nicht deserialisiert.");
        }
    }
}
