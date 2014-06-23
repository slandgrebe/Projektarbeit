using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JumpAndRun;
using System.Collections.Generic;
using JumpAndRun.GameLogic;

namespace JumpAndRun
{
    [TestClass]
    public class LevelTest
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
        public void Level_AddXmlPath()
        {
            Level level = new Level();
            level.AddXmlPath("/data/levels/test/level.xml");
            Assert.AreEqual(1, level.SegmentsXmlPath.Count, "Falsche anzahl XML Pfade.");
        }

        [TestMethod]
        public void Level_AddSegment()
        {
            Level level = new Level();
            LevelSegment ls = new LevelSegment();
            level.AddSegment(ls);
            Assert.AreEqual(1, level.Segments.Count, "Falsche anzahl Segmente.");
        }

        [TestMethod]
        public void Level_Deserialize()
        {
            Level level = new Level();
            level.AddXmlPath("/data/levels/test/segments/start.xml");
            level.AddXmlPath("/data/levels/test/segments/segment1.xml");
            level.AddXmlPath("/data/levels/test/segments/end.xml");
            level.Deserialize();
            Assert.AreEqual(3, level.Segments.Count, "Falsche anzahl XML Pfade.");
        }

        [TestMethod]
        public void Level_Load()
        {
            Level level = new Level();
            level.AddXmlPath("/data/levels/test/segments/start.xml");
            level.AddXmlPath("/data/levels/test/segments/segment1.xml");
            level.AddXmlPath("/data/levels/test/segments/end.xml");
            level.Deserialize();
            level.Load();
            Assert.AreEqual(3, level.Segments.Count, "Falsche anzahl XML Pfade.");
        }

        [TestMethod]
        public void Level_Dispose()
        {
            Level level = new Level();
            level.AddXmlPath("/data/levels/test/segments/start.xml");
            level.AddXmlPath("/data/levels/test/segments/segment1.xml");
            level.AddXmlPath("/data/levels/test/segments/end.xml");
            level.Deserialize();
            level.Load();
            Assert.AreNotEqual((System.UInt32)0, level.Segments[0].objects[0].Model.Id, "Objekt wurde nicht entfernt.");
            level.Dispose();
            Assert.AreEqual((System.UInt32)0, level.Segments[0].objects[0].Model.Id, "Objekt wurde nicht entfernt.");
        }
    }
}
