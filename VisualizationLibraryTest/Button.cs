using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace VisualizationLibraryTest
{
    [TestClass]
    public class Button
    {
        [TestMethod]
        public void Button_AddWithoutExistingWindowWithData()
        {
            // Setup
            uint id = Library.addButton("data/fonts/arial.ttf");

            // Test
            Assert.AreNotEqual(0, id);

            // Tear Down
            if (!Utility.TearDown(id)) Assert.AreEqual(1, 0); // da ging was schief
        }

        [TestMethod]
        public void Button_AddWithExistingWindowWithData()
        {
            // Setup
            Library.init("Test", false, 640, 480);
            uint id = Library.addButton("data/fonts/arial.ttf");

            // Test
            Assert.AreNotEqual(0, id);

            // Tear Down
            if (!Utility.TearDown(id)) Assert.AreEqual(1, 0); // da ging was schief
        }

        [TestMethod]
        public void Button_AddWithoutExistingWindowWithoutData()
        {
            // Setup
            uint id = Library.addButton("");

            // Test
            Assert.AreNotEqual(0, id);

            // Tear Down
            if (!Utility.TearDown(id)) Assert.AreEqual(1, 0); // da ging was schief
        }

        [TestMethod]
        public void Button_AddWithExistingWindowWithoutData()
        {
            // Setup
            Library.init("Test", false, 640, 480);
            uint id = Library.addButton("");

            // Test
            Assert.AreNotEqual(0, id);

            // Tear Down
            if (!Utility.TearDown(id)) Assert.AreEqual(1, 0); // da ging was schief
        }

        [TestMethod]
        public void Button_IsCreated()
        {
            // Setup
            uint id = Utility.SetupButton();

            // Test
            Assert.AreEqual(true, Library.isCreated(id));

            // Tear Down
            if (!Utility.TearDown(id)) Assert.AreEqual(1, 0); // da ging was schief
        }

        [TestMethod]
        public void Button_IsCreatedNegative()
        {
            // Setup
            Library.init("Test", false, 640, 480);

            // Test
            Assert.AreNotEqual(0, Library.isCreated(99999));

            // Tear Down
            Library.close();
        }

        [TestMethod]
        public void Button_Dispose()
        {
            // Setup
            uint id = Utility.SetupButton();
            Library.dispose(id);
            Utility.CheckRemoval(id, 2);

            // Test
            Assert.AreEqual(false, Library.isCreated(id));

            // Tear Down
            if (!Utility.TearDown(id)) Assert.AreEqual(1, 0); // da ging was schief
        }

        [TestMethod]
        public void Button_DisposeNegavite() // Point löschen den es nicht gibt
        {
            // Setup
            uint id = Utility.SetupButton();
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
        public void Button_Position()
        {
            // Setup
            uint id = Utility.SetupButton();

            // Test
            Assert.AreEqual(true, Library.position(id, 1.0f, 1.0f, 1.0f));

            // Tear Down
            if (!Utility.TearDown(id)) Assert.AreEqual(1, 0); // da ging was schief
        }

        [TestMethod]
        public void Button_PositionNegavite() // Model positionieren den es nicht gibt
        {
            // Setup
            uint id = Utility.SetupButton();
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
        public void Button_Rotate() // Text kann nicht rotiert werden
        {
            // Setup
            uint id = Utility.SetupButton();

            // Test
            Assert.AreEqual(false, Library.rotate(id, 90, 1, 1, 1));

            // Tear Down
            if (!Utility.TearDown(id)) Assert.AreEqual(1, 0); // da ging was schief
        }

        [TestMethod]
        public void Button_Scale() // Text kann nicht skaliert werden
        {
            // Setup
            uint id = Utility.SetupButton();

            // Test
            Assert.AreEqual(true, Library.scale(id, 0.5f, 0.5f, 0.5f));

            // Tear Down
            if (!Utility.TearDown(id)) Assert.AreEqual(1, 0); // da ging was schief
        }

        [TestMethod]
        public void Button_HighlightColor() // Text kann nicht hervorgehoben werden
        {
            // Setup
            uint id = Utility.SetupButton();

            // Test
            Assert.AreEqual(true, Library.highlightColor(id, 1.0f, 1.0f, 1.0f, 1.0f));

            // Tear Down
            if (!Utility.TearDown(id)) Assert.AreEqual(1, 0); // da ging was schief
        }

        [TestMethod]
        public void Button_IsHighlighted() // Text kann nicht hervorgehoben werden
        {
            // Setup
            uint id = Utility.SetupButton();

            // Test
            Assert.AreEqual(true, Library.isHighlighted(id, true));

            // Tear Down
            if (!Utility.TearDown(id)) Assert.AreEqual(1, 0); // da ging was schief
        }

        [TestMethod]
        public void Button_AttachToCamera() // Text kann nicht an die Kamera angehängt werden
        {
            // Setup
            uint id = Utility.SetupButton();

            // Test
            Assert.AreEqual(false, Library.attachToCamera(id, true));

            // Tear Down
            if (!Utility.TearDown(id)) Assert.AreEqual(1, 0); // da ging was schief
        }

        [TestMethod]
        public void Button_Text()
        {
            // Setup
            uint id = Utility.SetupButton();

            // Test
            Assert.AreEqual(true, Library.isCreated(id) && Library.text(id, "anderer text"));

            // Tear Down
            if (!Utility.TearDown(id)) Assert.AreEqual(1, 0); // da ging was schief
        }
        [TestMethod]
        public void Button_TextNegative()
        {
            // Setup
            uint id = Utility.SetupButton();

            // Test
            Assert.AreEqual(true, Library.text(id, ""));

            // Tear Down
            if (!Utility.TearDown(id)) Assert.AreEqual(1, 0); // da ging was schief
        }

        [TestMethod]
        public void Button_TextSize()
        {
            // Setup
            uint id = Utility.SetupButton();

            // Test
            Assert.AreEqual(true, Library.textSize(id, 20));

            // Tear Down
            if (!Utility.TearDown(id)) Assert.AreEqual(1, 0); // da ging was schief
        }

        [TestMethod]
        public void Button_TextSizeNegativeSize()
        {
            // Setup
            uint id = Utility.SetupButton();

            // Test
            Assert.AreEqual(true, Library.textSize(id, -20));

            // Tear Down
            if (!Utility.TearDown(id)) Assert.AreEqual(1, 0); // da ging was schief
        }

        [TestMethod]
        public void Button_TextSizeZero()
        {
            // Setup
            uint id = Utility.SetupButton();

            // Test
            Assert.AreEqual(true, Library.textSize(id, 0));

            // Tear Down
            if (!Utility.TearDown(id)) Assert.AreEqual(1, 0); // da ging was schief
        }

        [TestMethod]
        public void Button_TextSizeNegative()
        {
            // Setup
            uint id = Utility.SetupButton();
            uint inexistantId = id;
            while (Library.isCreated(inexistantId)) // finde nicht existierende Id
            {
                inexistantId++;
            }

            // Test
            Assert.AreEqual(false, Library.textSize(inexistantId, 20));

            // Tear Down
            if (!Utility.TearDown(id)) Assert.AreEqual(1, 0); // da ging was schief
        }

        [TestMethod]
        public void Button_TextColor()
        {
            // Setup
            uint id = Utility.SetupButton();

            // Test
            Assert.AreEqual(true, Library.textColor(id, 1, 1, 1, 1));

            // Tear Down
            if (!Utility.TearDown(id)) Assert.AreEqual(1, 0); // da ging was schief
        }

        [TestMethod]
        public void Button_TextColorOutOfBounds()
        {
            // Setup
            uint id = Utility.SetupButton();

            // Test
            Assert.AreEqual(true, Library.textColor(id, 2, 2, 2, 2));

            // Tear Down
            if (!Utility.TearDown(id)) Assert.AreEqual(1, 0); // da ging was schief
        }

        [TestMethod]
        public void Button_TextColorOutOfBoundsNegative()
        {
            // Setup
            uint id = Utility.SetupButton();

            // Test
            Assert.AreEqual(true, Library.textColor(id, -2, -2, -2, -2));

            // Tear Down
            if (!Utility.TearDown(id)) Assert.AreEqual(1, 0); // da ging was schief
        }

        [TestMethod]
        public void Button_TextColorNegative() // Model das nicht existiert
        {
            // Setup
            uint id = Utility.SetupButton();
            uint inexistantId = id;
            while (Library.isCreated(inexistantId)) // finde nicht existierende Id
            {
                inexistantId++;
            }

            // Test
            Assert.AreEqual(false, Library.textColor(inexistantId, 1, 1, 1, 1));

            // Tear Down
            if (!Utility.TearDown(id)) Assert.AreEqual(1, 0); // da ging was schief
        }
    }
}
