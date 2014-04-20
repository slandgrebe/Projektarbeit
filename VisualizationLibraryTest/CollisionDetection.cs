using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VisualizationLibraryTest
{
    [TestClass]
    public class CollisionDetection
    {
        [TestMethod]
        public void CollisionDetection_Collision()
        {
            // Setup
            uint id = Library.addModel("data/models/cube.obj");
            uint id2 = Library.addModel("data/models/cube.obj");

            Utility.CheckCreation(id, 1);
            Utility.CheckCreation(id2, 1);

            Library.scalingIsNormalized(id, true);
            Library.scalingIsNormalized(id2, true);

            Library.scale(id, 1, 1, 1);
            Library.scale(id2, 1, 1, 1);

            Library.position(id, 0, 0, -5);
            Library.position(id2, 0, 0, -5);

            System.Threading.Thread.Sleep(100); // Zeit lassen um min. 1 neues Bild zu zeichnen

            uint length = Library.collisionsTextLength();
            System.Text.StringBuilder str = new System.Text.StringBuilder((int)length + 1);
            Library.collisionsText(str, str.Capacity); // Daten aus DLL holen

            // Test
            String expectation = id + ":" + id2 + ";" + id2 + ":" + id;
            Assert.AreEqual(expectation, str.ToString());

            // Tear Down
            Utility.Dispose(id2);
            if (!Utility.TearDown(id)) Assert.AreEqual(1, 0); // da ging was schief
        }

        [TestMethod]
        public void CollisionDetection_CollisionBorderCase()
        {
            // Setup
            uint id = Library.addModel("data/models/cube.obj");
            uint id2 = Library.addModel("data/models/cube.obj");

            Utility.CheckCreation(id, 1);
            Utility.CheckCreation(id2, 1);

            Library.scalingIsNormalized(id, true);
            Library.scalingIsNormalized(id2, true);

            Library.scale(id, 1, 1, 1);
            Library.scale(id2, 1, 1, 1);

            Library.position(id, -0.43f, 0, -5);
            Library.position(id2, 0.43f, 0, -5);

            System.Threading.Thread.Sleep(100); // Zeit lassen um min. 1 neues Bild zu zeichnen

            uint length = Library.collisionsTextLength();
            System.Text.StringBuilder str = new System.Text.StringBuilder((int)length + 1);
            Library.collisionsText(str, str.Capacity); // Daten aus DLL holen

            // Test
            String expectation = id + ":" + id2 + ";" + id2 + ":" + id;
            Assert.AreEqual(expectation, str.ToString());

            // Tear Down
            Utility.Dispose(id2);
            if (!Utility.TearDown(id)) Assert.AreEqual(1, 0); // da ging was schief
        }

        [TestMethod]
        public void CollisionDetection_NoCollision()
        {
            // Setup
            uint id = Library.addModel("data/models/cube.obj");
            uint id2 = Library.addModel("data/models/cube.obj");

            Utility.CheckCreation(id, 1);
            Utility.CheckCreation(id2, 1);

            Library.scalingIsNormalized(id, true);
            Library.scalingIsNormalized(id2, true);

            Library.scale(id, 1, 1, 1);
            Library.scale(id2, 1, 1, 1);

            Library.position(id, -2, 0, -5);
            Library.position(id2, 2, 0, -5);

            System.Threading.Thread.Sleep(100); // Zeit lassen um min. 1 neues Bild zu zeichnen

            uint length = Library.collisionsTextLength();
            System.Text.StringBuilder str = new System.Text.StringBuilder((int)length + 1);
            Library.collisionsText(str, str.Capacity); // Daten aus DLL holen

            // Test
            String expectation = id + ":;" + id2 + ":";
            Assert.AreEqual(expectation, str.ToString());

            // Tear Down
            Utility.Dispose(id2);
            if (!Utility.TearDown(id)) Assert.AreEqual(1, 0); // da ging was schief
        }
    }
}
