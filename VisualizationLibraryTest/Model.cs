using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VisualizationLibraryTest
{
    [TestClass]
    public class Model
    {
        [TestMethod]
        public void Model_AddWithoutExistingWindowWithData()
        {
            // Setup
            uint id = Library.addModel("data/models/cube.obj");

            // Test
            Assert.AreNotEqual(0, id);

            // Tear Down
            Utility.CheckCreation(id, 1);
            if (!Utility.TearDown(id)) Assert.AreEqual(1, 0); // da ging was schief
        }

        [TestMethod]
        public void Model_AddWithExistingWindowWithData()
        {
            // Setup
            Library.init("Test", false, 640, 480);
            uint id = Library.addModel("data/models/cube.obj");

            // Test
            Assert.AreNotEqual(0, id);

            // Tear Down
            Utility.CheckCreation(id, 1);
            if (!Utility.TearDown(id)) Assert.AreEqual(1, 0); // da ging was schief
        }

        [TestMethod]
        public void Model_AddWithoutExistingWindowWithoutData()
        {
            // Setup
            uint id = Library.addModel("");

            // Test
            Assert.AreNotEqual(0, id);

            // Tear Down
            Utility.CheckCreation(id, 1);
            if (!Utility.TearDown(id)) Assert.AreEqual(1, 0); // da ging was schief
        }

        [TestMethod]
        public void Model_AddWithExistingWindowWithoutData()
        {
            // Setup
            Library.init("Test", false, 640, 480);
            uint id = Library.addModel("");

            // Test
            Assert.AreNotEqual(0, id);

            // Tear Down
            Utility.CheckCreation(id, 1);
            if (!Utility.TearDown(id)) Assert.AreEqual(1, 0); // da ging was schief
        }

        [TestMethod]
        public void Model_IsCreated()
        {
            // Setup
            uint id = Utility.SetupModel();

            // Test
            Assert.AreEqual(true, Library.isCreated(id));

            // Tear Down
            if (!Utility.TearDown(id)) Assert.AreEqual(1, 0); // da ging was schief
        }

        [TestMethod]
        public void Model_IsCreatedNegative()
        {
            // Setup
            Library.init("Test", false, 640, 480);

            // Test
            Assert.AreNotEqual(0, Library.isCreated(99999));

            // Tear Down
            Library.close();
        }

        [TestMethod]
        public void Model_Dispose()
        {
            // Setup
            uint id = Utility.SetupModel();
            Library.dispose(id);
            System.Threading.Thread.Sleep(100); // Multithreading

            // Test
            Assert.AreEqual(false, Library.isCreated(id));

            // Tear Down
            if (!Utility.TearDown(id)) Assert.AreEqual(1, 0); // da ging was schief
        }

        [TestMethod]
        public void Model_DisposeNegative() // Point löschen den es nicht gibt
        {
            // Setup
            uint id = Utility.SetupModel();
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
        public void Model_Position()
        {
            // Setup
            uint id = Utility.SetupModel();

            // Test
            Assert.AreEqual(true, Library.position(id, 1.0f, 1.0f, 1.0f));

            // Tear Down
            if (!Utility.TearDown(id)) Assert.AreEqual(1, 0); // da ging was schief
        }

        [TestMethod]
        public void Model_PositionNegative() // Point positionieren den es nicht gibt
        {
            // Setup
            uint id = Utility.SetupModel();
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
        public void Model_PositionX()
        {
            // Setup
            uint id = Utility.SetupModel();
            Library.position(id, 1, 2, 3);

            // Test
            Assert.AreEqual(1, Library.positionX(id));

            // Tear Down
            if (!Utility.TearDown(id)) Assert.AreEqual(1, 0); // da ging was schief
        }

        [TestMethod]
        public void Model_PositionXNegative() // Point positionieren den es nicht gibt
        {
            // Setup
            uint id = Utility.SetupModel();
            Library.position(id, 1, 2, 3);

            uint inexistantId = id;
            while (Library.isCreated(inexistantId)) // finde nicht existierende Id
            {
                inexistantId++;
            }

            // Test
            Assert.AreEqual(0, Library.positionX(inexistantId));

            // Tear Down
            if (!Utility.TearDown(id)) Assert.AreEqual(1, 0); // da ging was schief
        }

        [TestMethod]
        public void Model_PositionY()
        {
            // Setup
            uint id = Utility.SetupModel();
            Library.position(id, 1, 2, 3);

            // Test
            Assert.AreEqual(2, Library.positionY(id));

            // Tear Down
            if (!Utility.TearDown(id)) Assert.AreEqual(1, 0); // da ging was schief
        }

        [TestMethod]
        public void Model_PositionYNegative() // Point positionieren den es nicht gibt
        {
            // Setup
            uint id = Utility.SetupModel();
            Library.position(id, 1, 2, 3);

            uint inexistantId = id;
            while (Library.isCreated(inexistantId)) // finde nicht existierende Id
            {
                inexistantId++;
            }

            // Test
            Assert.AreEqual(0, Library.positionY(inexistantId));

            // Tear Down
            if (!Utility.TearDown(id)) Assert.AreEqual(1, 0); // da ging was schief
        }

        [TestMethod]
        public void Model_PositionZ()
        {
            // Setup
            uint id = Utility.SetupModel();
            Library.position(id, 1, 2, 3);

            // Test
            Assert.AreEqual(3, Library.positionZ(id));

            // Tear Down
            if (!Utility.TearDown(id)) Assert.AreEqual(1, 0); // da ging was schief
        }

        [TestMethod]
        public void Model_PositionZNegative() // Point positionieren den es nicht gibt
        {
            // Setup
            uint id = Utility.SetupModel();
            Library.position(id, 1, 2, 3);

            uint inexistantId = id;
            while (Library.isCreated(inexistantId)) // finde nicht existierende Id
            {
                inexistantId++;
            }

            // Test
            Assert.AreEqual(0, Library.positionZ(inexistantId));

            // Tear Down
            if (!Utility.TearDown(id)) Assert.AreEqual(1, 0); // da ging was schief
        }

        [TestMethod]
        public void Model_Rotate()
        {
            // Setup
            uint id = Utility.SetupModel();

            // Test
            Assert.AreEqual(true, Library.rotate(id, 90, 1, 1, 1));

            // Tear Down
            if (!Utility.TearDown(id)) Assert.AreEqual(1, 0); // da ging was schief
        }

        [TestMethod]
        public void Model_RotateWithoutAxis()
        {
            // Setup
            uint id = Utility.SetupModel();

            // Test
            Assert.AreEqual(true, Library.rotate(id, 90, 0, 0, 0));

            // Tear Down
            if (!Utility.TearDown(id)) Assert.AreEqual(1, 0); // da ging was schief
        }

        [TestMethod]
        public void Model_RotateNegative() // Point positionieren den es nicht gibt
        {
            // Setup
            uint id = Utility.SetupModel();
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
        public void Model_Scale()
        {


            // Setup
            uint id = Utility.SetupModel();

            // Test
            Assert.AreEqual(true, Library.scale(id, 0.5f, 0.5f, 0.5f));

            // Tear Down
            if (!Utility.TearDown(id)) Assert.AreEqual(1, 0); // da ging was schief
        }

        [TestMethod]
        public void Model_ScaleNegativeNumbers()
        {
            // Setup
            uint id = Utility.SetupModel();

            // Test
            Assert.AreEqual(true, Library.scale(id, -0.5f, -0.5f, -0.5f));

            // Tear Down
            if (!Utility.TearDown(id)) Assert.AreEqual(1, 0); // da ging was schief
        }

        [TestMethod]
        public void Model_ScaleNegative() // Point skalieren den es nicht gibt
        {
            // Setup
            uint id = Utility.SetupModel();
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
        public void Model_ScalingIsNormalized()
        {
            // Setup
            uint id = Utility.SetupModel();

            // Test
            Assert.AreEqual(true, Library.scalingIsNormalized(id, true));

            // Tear Down
            if (!Utility.TearDown(id)) Assert.AreEqual(1, 0); // da ging was schief
        }

        [TestMethod]
        public void Model_ScalingIsNormalizedNegative() // Point Skalierungsnormaliesierung den es nicht gibt
        {
            // Setup
            uint id = Utility.SetupModel();
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
        public void Model_HighlightColor()
        {
            // Setup
            uint id = Utility.SetupModel();

            // Test
            Assert.AreEqual(true, Library.highlightColor(id, 1.0f, 1.0f, 1.0f, 1.0f));

            // Tear Down
            if (!Utility.TearDown(id)) Assert.AreEqual(1, 0); // da ging was schief
        }

        [TestMethod]
        public void Model_HighlightColornNegative() // Point positionieren den es nicht gibt
        {
            // Setup
            uint id = Utility.SetupModel();
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
        public void Model_HighlightColorOutOfBounds()
        {
            // Setup
            uint id = Utility.SetupModel();

            // Test
            Assert.AreEqual(true, Library.highlightColor(id, 2.0f, 2.0f, 2.0f, 2.0f));

            // Tear Down
            if (!Utility.TearDown(id)) Assert.AreEqual(1, 0); // da ging was schief
        }

        [TestMethod]
        public void Model_HighlightColorOutOfBoundsNegativeNumbers()
        {
            // Setup
            uint id = Utility.SetupModel();

            // Test
            Assert.AreEqual(true, Library.highlightColor(id, -2.0f, -2.0f, -2.0f, -2.0f));

            // Tear Down
            if (!Utility.TearDown(id)) Assert.AreEqual(1, 0); // da ging was schief
        }

        [TestMethod]
        public void Model_IsHighlighted()
        {
            // Setup
            uint id = Utility.SetupModel();

            // Test
            Assert.AreEqual(true, Library.isHighlighted(id, true));

            // Tear Down
            if (!Utility.TearDown(id)) Assert.AreEqual(1, 0); // da ging was schief
        }

        [TestMethod]
        public void Model_IsHighlightedNegative()
        {
            // Setup
            uint id = Utility.SetupModel();
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
        public void Model_AttachToCamera()
        {
            // Setup
            uint id = Utility.SetupModel();

            // Test
            Assert.AreEqual(true, Library.attachToCamera(id, true));

            // Tear Down
            if (!Utility.TearDown(id)) Assert.AreEqual(1, 0); // da ging was schief
        }

        [TestMethod]
        public void Model_DetachFromCamera()
        {
            // Setup
            uint id = Utility.SetupModel();
            Library.attachToCamera(id, true);

            // Test
            Assert.AreEqual(true, Library.attachToCamera(id, false));

            // Tear Down
            if (!Utility.TearDown(id)) Assert.AreEqual(1, 0); // da ging was schief
        }

        [TestMethod]
        public void Model_AttachToCameraNegative()
        {
            // Setup
            uint id = Utility.SetupModel();
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
