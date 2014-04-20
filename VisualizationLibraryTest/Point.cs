using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VisualizationLibraryTest
{
    [TestClass]
    public class Point
    {
        [TestMethod]
        public void Point_AddWithoutExistingWindowWithData()
        {
            // Setup
            uint id = Library.addPoint("data/textures/sample.png");

            // Test
            Assert.AreNotEqual(0, id);

            // Tear Down
            Utility.CheckCreation(id, 1);
            if (!Utility.TearDown(id)) Assert.AreEqual(1, 0); // da ging was schief
        }

        [TestMethod]
        public void Point_AddWithExistingWindowWithData()
        {
            // Setup
            Library.init("Test", false, 640, 480);
            uint id = Library.addPoint("data/textures/sample.png");

            // Test
            Assert.AreNotEqual(0, id);

            // Tear Down
            Utility.CheckCreation(id, 1);
            if (!Utility.TearDown(id)) Assert.AreEqual(1, 0); // da ging was schief
        }

        [TestMethod]
        public void Point_AddWithoutExistingWindowWithoutData()
        {
            // Setup
            uint id = Library.addPoint("");

            // Test
            Assert.AreNotEqual(0, id);

            // Tear Down
            Utility.CheckCreation(id, 1);
            if (!Utility.TearDown(id)) Assert.AreEqual(1, 0); // da ging was schief
        }

        [TestMethod]
        public void Point_AddWithExistingWindowWithoutData()
        {
            // Setup
            Library.init("Test", false, 640, 480);
            uint id = Library.addPoint("");

            // Test
            Assert.AreNotEqual(0, id);

            // Tear Down
            Utility.CheckCreation(id, 1);
            if (!Utility.TearDown(id)) Assert.AreEqual(1, 0); // da ging was schief
        }

        [TestMethod]
        public void Point_IsCreated()
        {
            // Setup
            uint id = Utility.SetupPoint();

            // Test
            Assert.AreEqual(true, Library.isCreated(id));

            // Tear Down
            if (!Utility.TearDown(id)) Assert.AreEqual(1, 0); // da ging was schief
        }

        [TestMethod]
        public void Point_IsCreatedNegative()
        {
            // Setup
            Library.init("Test", false, 640, 480);

            // Test
            Assert.AreNotEqual(0, Library.isCreated(99999));

            // Tear Down
            Library.close();
        }

        [TestMethod]
        public void Point_Dispose()
        {
            // Setup
            uint id = Utility.SetupPoint();
            Library.dispose(id);
            System.Threading.Thread.Sleep(100); // Multithreading

            // Test
            Assert.AreEqual(false, Library.isCreated(id));

            // Tear Down
            if (!Utility.TearDown(id)) Assert.AreEqual(1, 0); // da ging was schief
        }

        [TestMethod]
        public void Point_DisposeNegavite() // Point löschen den es nicht gibt
        {
            // Setup
            uint id = Utility.SetupPoint();
            uint inexistantId = id;
            while (Library.isCreated(inexistantId)) // finde nicht existierende Id
            {
                inexistantId++;
            }
            Library.dispose(inexistantId);

            // Test
            Assert.AreEqual(1, 1); // keine Exception, mehr kann man nicht testen

            // Tear Down
            if (!Utility.TearDown(id)) Assert.AreEqual(1, 0); // da ging was schief
        }

        [TestMethod]
        public void Point_Position()
        {
            // Setup
            uint id = Utility.SetupPoint();

            // Test
            Assert.AreEqual(true, Library.position(id, 1.0f, 1.0f, 1.0f));

            // Tear Down
            if (!Utility.TearDown(id)) Assert.AreEqual(1, 0); // da ging was schief
        }

        [TestMethod]
        public void Point_PositionNegavite() // Point positionieren den es nicht gibt
        {
            // Setup
            uint id = Utility.SetupPoint();
            uint inexistantId = id;
            while (Library.isCreated(inexistantId)) // finde nicht existierende Id
            {
                inexistantId++;
            }

            // Test
            Assert.AreEqual(false, Library.position(inexistantId, 1, 1, 1));

            // Tear Down
            if (!Utility.TearDown(id)) Assert.AreEqual(1, 0); // da ging was schief
        }

        [TestMethod]
        public void Point_Rotate()
        {
            // Setup
            uint id = Utility.SetupPoint();

            // Test
            Assert.AreEqual(true, Library.rotate(id, 90, 1, 1, 1));

            // Tear Down
            if (!Utility.TearDown(id)) Assert.AreEqual(1, 0); // da ging was schief
        }

        [TestMethod]
        public void Point_RotateWithoutAxis()
        {
            // Setup
            uint id = Utility.SetupPoint();

            // Test
            Assert.AreEqual(true, Library.rotate(id, 90, 0, 0, 0));

            // Tear Down
            if (!Utility.TearDown(id)) Assert.AreEqual(1, 0); // da ging was schief
        }

        [TestMethod]
        public void Point_RotateNegavite() // Point positionieren den es nicht gibt
        {
            // Setup
            uint id = Utility.SetupPoint();
            uint inexistantId = id;
            while (Library.isCreated(inexistantId)) // finde nicht existierende Id
            {
                inexistantId++;
            }

            // Test
            Assert.AreEqual(false, Library.rotate(inexistantId, 90, 1, 1, 1));

            // Tear Down
            if (!Utility.TearDown(id)) Assert.AreEqual(1, 0); // da ging was schief
        }

        [TestMethod]
        public void Point_Scale()
        {
            // Setup
            uint id = Utility.SetupPoint();

            // Test
            Assert.AreEqual(true, Library.scale(id, 0.5f, 0.5f, 0.5f));

            // Tear Down
            if (!Utility.TearDown(id)) Assert.AreEqual(1, 0); // da ging was schief
        }

        [TestMethod]
        public void Point_ScaleNegativeNumbers()
        {
            // Setup
            uint id = Utility.SetupPoint();

            // Test
            Assert.AreEqual(true, Library.scale(id, -0.5f, -0.5f, -0.5f));

            // Tear Down
            if (!Utility.TearDown(id)) Assert.AreEqual(1, 0); // da ging was schief
        }

        [TestMethod]
        public void Point_ScaleNegavite() // Point skalieren den es nicht gibt
        {
            // Setup
            uint id = Utility.SetupPoint();
            uint inexistantId = id;
            while (Library.isCreated(inexistantId)) // finde nicht existierende Id
            {
                inexistantId++;
            }

            // Test
            Assert.AreEqual(false, Library.scale(inexistantId, 1, 1, 1));

            // Tear Down
            if (!Utility.TearDown(id)) Assert.AreEqual(1, 0); // da ging was schief
        }

        [TestMethod]
        public void Point_ScalingIsNormalized()
        {
            // Setup
            uint id = Utility.SetupPoint();

            // Test
            Assert.AreEqual(true, Library.scalingIsNormalized(id, true));

            // Tear Down
            if (!Utility.TearDown(id)) Assert.AreEqual(1, 0); // da ging was schief
        }

        [TestMethod]
        public void Point_ScalingIsNormalizedNegavite() // Point Skalierungsnormaliesierung den es nicht gibt
        {
            // Setup
            uint id = Utility.SetupPoint();
            uint inexistantId = id;
            while (Library.isCreated(inexistantId)) // finde nicht existierende Id
            {
                inexistantId++;
            }

            // Test
            Assert.AreEqual(false, Library.scalingIsNormalized(inexistantId, true));

            // Tear Down
            if (!Utility.TearDown(id)) Assert.AreEqual(1, 0); // da ging was schief
        }

        [TestMethod]
        public void Point_HighlightColor()
        {
            // Setup
            uint id = Utility.SetupPoint();

            // Test
            Assert.AreEqual(true, Library.highlightColor(id, 1.0f, 1.0f, 1.0f, 1.0f));

            // Tear Down
            if (!Utility.TearDown(id)) Assert.AreEqual(1, 0); // da ging was schief
        }

        [TestMethod]
        public void Point_HighlightColornNegavite() // Point positionieren den es nicht gibt
        {
            // Setup
            uint id = Utility.SetupPoint();
            uint inexistantId = id;
            while (Library.isCreated(inexistantId)) // finde nicht existierende Id
            {
                inexistantId++;
            }

            // Test
            Assert.AreEqual(false, Library.highlightColor(inexistantId, 1, 1, 1, 1));

            // Tear Down
            if (!Utility.TearDown(id)) Assert.AreEqual(1, 0); // da ging was schief
        }

        [TestMethod]
        public void Point_HighlightColorOutOfBounds()
        {
            // Setup
            uint id = Utility.SetupPoint();

            // Test
            Assert.AreEqual(true, Library.highlightColor(id, 2.0f, 2.0f, 2.0f, 2.0f));

            // Tear Down
            if (!Utility.TearDown(id)) Assert.AreEqual(1, 0); // da ging was schief
        }

        [TestMethod]
        public void Point_HighlightColorOutOfBoundsNegative()
        {
            // Setup
            uint id = Utility.SetupPoint();

            // Test
            Assert.AreEqual(true, Library.highlightColor(id, -2.0f, -2.0f, -2.0f, -2.0f));

            // Tear Down
            if (!Utility.TearDown(id)) Assert.AreEqual(1, 0); // da ging was schief
        }

        [TestMethod]
        public void Point_IsHighlighted()
        {
            // Setup
            uint id = Utility.SetupPoint();

            // Test
            Assert.AreEqual(true, Library.isHighlighted(id, true));

            // Tear Down
            if (!Utility.TearDown(id)) Assert.AreEqual(1, 0); // da ging was schief
        }

        [TestMethod]
        public void Point_IsHighlightedNegavite()
        {
            // Setup
            uint id = Utility.SetupPoint();
            uint inexistantId = id;
            while (Library.isCreated(inexistantId)) // finde nicht existierende Id
            {
                inexistantId++;
            }

            // Test
            Assert.AreEqual(false, Library.isHighlighted(inexistantId, true));

            // Tear Down
            if (!Utility.TearDown(id)) Assert.AreEqual(1, 0); // da ging was schief
        }

        [TestMethod]
        public void Point_AttachToCamera()
        {
            // Setup
            uint id = Utility.SetupPoint();
        
            // Test
            Assert.AreEqual(true, Library.attachToCamera(id, true));

            // Tear Down
            if (!Utility.TearDown(id)) Assert.AreEqual(1, 0); // da ging was schief
        }

        [TestMethod]
        public void Point_DetachFromCamera()
        {
            // Setup
            uint id = Utility.SetupPoint();
            Library.attachToCamera(id, true);

            // Test
            Assert.AreEqual(true, Library.attachToCamera(id, false));

            // Tear Down
            if (!Utility.TearDown(id)) Assert.AreEqual(1, 0); // da ging was schief
        }

        [TestMethod]
        public void Point_AttachToCameraNegative()
        {
            // Setup
            uint id = Utility.SetupPoint();
            Library.attachToCamera(id, true);
            uint inexistantId = id;
            while (Library.isCreated(inexistantId)) // finde nicht existierende Id
            {
                inexistantId++;
            }

            // Test
            Assert.AreEqual(false, Library.attachToCamera(inexistantId, true));

            // Tear Down
            if (!Utility.TearDown(id)) Assert.AreEqual(1, 0); // da ging was schief
        }
    }
}
