using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JumpAndRun;

namespace JumpAndRun
{
    [TestClass]
    public class ModelTest
    {
        private JumpAndRun.Item.Model model;
        private JumpAndRun.Item.Model model2;

        [TestInitialize]
        public void MyClassInitialize()
        {
            View.Window.Init("test", false, 100, 100);
            model = new JumpAndRun.Item.Model("data/models/cube.obj", true);
            model2 = new JumpAndRun.Item.Model();
        }

        [TestCleanup]
        public void MyClassCleanup()
        {
            View.Window.Close();
        }

        [TestMethod]
        public void Model_Model()
        {
            JumpAndRun.Item.Model mod = new JumpAndRun.Item.Model("data/models/cube.obj", true);
            Assert.AreNotEqual(0, mod.Id, "head wurde nicht erstellt.");
            Assert.AreEqual("data/models/cube.obj", mod.Path, "Pfad wurde nicht gepseichert.");
        }

        [TestMethod]
        public void Model_Create()
        {
            model.Create();
            Assert.AreNotEqual(0, model.Id, "head wurde nicht erstellt.");
        }

        [TestMethod]
        public void Model_CreateWithoutPath()
        {
            JumpAndRun.Item.Model mod = new JumpAndRun.Item.Model();
            Assert.AreEqual(false, mod.Create(), "head darf ohne Pfad nicht erstellt werden");
        }

        [TestMethod]
        public void Model_CreateWithId()
        {
            JumpAndRun.Item.Model mod = new JumpAndRun.Item.Model("data/models/cube.obj", true);
            Assert.AreEqual(false, mod.Create(), "head darf nicht ein zweites mal erstellt werden.");
        }

        [TestMethod]
        public void Model_Scale()
        {
            Assert.AreEqual(true, model.Scale(2), "head wurde nicht Skaliert.");
        }

        [TestMethod]
        public void Model_ScaleWithoutId()
        {
            Assert.AreEqual(false, model2.Scale(2), "head darf nicht Skaliert werden.");
        }

        [TestMethod]
        public void Model_Rotate()
        {
            Assert.AreEqual(true, model.Rotate(2,2,2,2), "head wurde nicht gedreht.");
        }

        [TestMethod]
        public void Model_RotateWithoutId()
        {
            Assert.AreEqual(false, model2.Rotate(2, 2, 2, 2), "head darf nicht gedreht werden.");
        }

        [TestMethod]
        public void Model_AllignmentEqual()
        {
            Assert.AreEqual(false, model.Alignment(1, 1, 1, 1, 1, 1), "Ausgangspunkt darf nicht gleich wie Zielpunkt sein.");
        }

        [TestMethod]
        public void Model_AllignmentNotEqual()
        {
            JumpAndRun.Item.Model mod = new JumpAndRun.Item.Model("data/models/cube.obj", true); // Test schlägt sonst fehl.
            Assert.AreEqual(true, mod.Alignment(1, 1, 1, 2, 2, 2),"head konnte nicht ausgerichtet werden.");
        }

        [TestMethod]
        public void Model_AllignmentWithoutId()
        {
            JumpAndRun.Item.Model mod = new JumpAndRun.Item.Model();
            Assert.AreEqual(false, mod.Alignment(1, 1, 1, 2, 2, 2), "head darf nicht ausgerichtet werden.");
        }
        
        [TestMethod]
        public void Model_Position()
        {
            Assert.AreEqual(true, model.Position(3, 3, 3), "head wurde nicht neu positioniert.");
        }

        [TestMethod]
        public void Model_PositionWithoutId()
        {
            Assert.AreEqual(false, model2.Position(3, 3, 3), "head darf nicht neu positioniert werden.");
        }

        [TestMethod]
        public void Model_AttachToCameraTrue()
        {
            Assert.AreEqual(true, model.AttachToCamera(true), "head wurde nicht der Kamera angehängt.");
        }

        [TestMethod]
        public void Model_AttachToCameraFalse()
        {
            Assert.AreEqual(true, model.AttachToCamera(false), "head wurde nicht der Kamera abgehängt.");
        }

        [TestMethod]
        public void Model_AttachWithoutId()
        {
            Assert.AreEqual(false, model2.AttachToCamera(true), "head darf nicht der Kamera abge-/angehängt.");
        }

        [TestMethod]
        public void Model_Dispose()
        {
            model.Dispose();

        }
    }
}
