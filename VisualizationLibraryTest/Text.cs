using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace VisualizationLibraryTest
{
    [TestClass]
    public class Text
    {
        [TestMethod]
        public void Text_AddWithoutExistingWindowWithData()
        {
            // Setup
            uint id = Library.addText("data/fonts/arial.ttf");

            // Test
            Assert.AreNotEqual(0, id);

            // Tear Down
            Library.close();
        }

        [TestMethod]
        public void Text_AddWithExistingWindowWithData()
        {
            // Setup
            Library.init("Test", false, 640, 480);
            uint id = Library.addText("data/fonts/arial.ttf");

            // Test
            Assert.AreNotEqual(0, id);

            // Tear Down
            Library.close();
        }

        [TestMethod]
        public void Text_AddWithoutExistingWindowWithoutData()
        {
            // Setup
            uint id = Library.addText("");

            // Test
            Assert.AreNotEqual(0, id);

            // Tear Down
            Library.close();
        }

        [TestMethod]
        public void Text_AddWithExistingWindowWithoutData()
        {
            // Setup
            Library.init("Test", false, 640, 480);
            uint id = Library.addText("");

            // Test
            Assert.AreNotEqual(0, id);

            // Tear Down
            Library.close();
        }

        [TestMethod]
        public void Text_IsCreated()
        {
            // Setup
            Library.init("Test", false, 640, 480);
            uint id = Library.addText("data/fonts/arial.ttf");
            System.Threading.Thread.Sleep(100); // Multithreading

            // Test
            Assert.AreEqual(true, Library.isCreated(id));

            // Tear Down
            Library.close();
        }

        [TestMethod]
        public void Text_IsCreatedNegative()
        {
            // Setup
            Library.init("Test", false, 640, 480);

            // Test
            Assert.AreNotEqual(0, Library.isCreated(99999));

            // Tear Down
            Library.close();
        }

        [TestMethod]
        public void Text_Dispose()
        {
            // Setup
            Library.init("Test", false, 640, 480);
            uint id = Library.addText("data/fonts/arial.ttf");
            System.Threading.Thread.Sleep(100); // Multithreading
            Library.dispose(id);
            System.Threading.Thread.Sleep(100); // Multithreading

            // Test
            Assert.AreEqual(false, Library.isCreated(id));

            // Tear Down
            Library.close();
        }

        [TestMethod]
        public void Text_DisposeNegavite() // Point löschen den es nicht gibt
        {
            // Setup
            Library.init("Test", false, 640, 480);
            uint id = Library.addText("data/fonts/arial.ttf");
            if (!Utility.CheckCreation(id, 1)) // Erstellung 1s lang prüfen
            {
                Assert.Equals(0, 1); // konnte das Objekt nicht innerhalb von 1s erstellen => Fehler
            }
            while (Library.isCreated(id)) // finde nicht existierende Id
            {
                id++;
            }
            Library.dispose(id);

            // Test
            Assert.AreEqual(1, 1); // keine Exception, mehr kann man nicht testen

            // Tear Down
            Library.close();
        }

        [TestMethod]
        public void Text_Position()
        {
            // Setup
            Library.init("Test", false, 640, 480);
            uint id = Library.addText("data/fonts/arial.ttf");
            if (!Utility.CheckCreation(id, 1)) // Erstellung 1s lang prüfen
            {
                Assert.Equals(0, 1); // konnte das Objekt nicht innerhalb von 1s erstellen => Fehler
            }

            // Test
            Assert.AreEqual(true, Library.position(id, 1.0f, 1.0f, 1.0f));

            // Tear Down
            Library.close();
        }

        [TestMethod]
        public void Text_PositionNegavite() // Model positionieren den es nicht gibt
        {
            // Setup
            Library.init("Test", false, 640, 480);
            uint id = Library.addText("data/fonts/arial.ttf");
            if (!Utility.CheckCreation(id, 1)) // Erstellung 1s lang prüfen
            {
                Assert.Equals(0, 1); // konnte das Objekt nicht innerhalb von 1s erstellen => Fehler
            }
            while (Library.isCreated(id)) // finde nicht existierende Id
            {
                id++;
            }

            // Test
            Assert.AreEqual(false, Library.position(id, 1, 1, 1));

            // Tear Down
            Library.close();
        }

        [TestMethod]
        public void Text_Rotate() // Text kann nicht rotiert werden
        {
            // Setup
            Library.init("Test", false, 640, 480);
            uint id = Library.addText("data/fonts/arial.ttf");
            if (!Utility.CheckCreation(id, 1)) // Erstellung 1s lang prüfen
            {
                Assert.Equals(0, 1); // konnte das Objekt nicht innerhalb von 1s erstellen => Fehler
            }

            // Test
            Assert.AreEqual(false, Library.rotate(id, 90, 1, 1, 1));

            // Tear Down
            Library.close();
        }

        [TestMethod]
        public void Text_Scale() // Text kann nicht skaliert werden
        {
            // Setup
            Library.init("Test", false, 640, 480);
            uint id = Library.addText("data/fonts/arial.ttf");
            if (!Utility.CheckCreation(id, 1)) // Erstellung 1s lang prüfen
            {
                Assert.Equals(0, 1); // konnte das Objekt nicht innerhalb von 1s erstellen => Fehler
            }

            // Test
            Assert.AreEqual(false, Library.scale(id, 0.5f, 0.5f, 0.5f));

            // Tear Down
            Library.close();
        }

        [TestMethod]
        public void Text_HighlightColor() // Text kann nicht hervorgehoben werden
        {
            // Setup
            Library.init("Test", false, 640, 480);
            uint id = Library.addText("data/fonts/arial.ttf");
            if (!Utility.CheckCreation(id, 1)) // Erstellung 1s lang prüfen
            {
                Assert.Equals(0, 1); // konnte das Objekt nicht innerhalb von 1s erstellen => Fehler
            }

            // Test
            Assert.AreEqual(false, Library.highlightColor(id, 1.0f, 1.0f, 1.0f, 1.0f));

            // Tear Down
            Library.close();
        }

        [TestMethod]
        public void Text_IsHighlighted() // Text kann nicht hervorgehoben werden
        {
            // Setup
            Library.init("Test", false, 640, 480);
            uint id = Library.addText("data/fonts/arial.ttf");
            if (!Utility.CheckCreation(id, 1)) // Erstellung 1s lang prüfen
            {
                Assert.Equals(0, 1); // konnte das Objekt nicht innerhalb von 1s erstellen => Fehler
            }

            // Test
            Assert.AreEqual(false, Library.isHighlighted(id, true));

            // Tear Down
            Library.close();
        }

        [TestMethod]
        public void Text_AttachToCamera() // Text kann nicht an die Kamera angehängt werden
        {
            // Setup
            Library.init("Test", false, 640, 480);
            uint id = Library.addText("data/fonts/arial.ttf");
            if (!Utility.CheckCreation(id, 1)) // Erstellung 1s lang prüfen
            {
                Assert.Equals(0, 1); // konnte das Objekt nicht innerhalb von 1s erstellen => Fehler
            }

            // Test
            Assert.AreEqual(false, Library.attachToCamera(id, true));

            // Tear Down
            Library.close();
        }

        [TestMethod]
        public void Text_Text()
        {
            // Setup
            Library.init("Test", false, 640, 480);
            uint id = Library.addText("data/fonts/arial.ttf");
            if (!Utility.CheckCreation(id, 1)) // Erstellung 1s lang prüfen
            {
                Assert.Equals(0, 1); // konnte das Objekt nicht innerhalb von 1s erstellen => Fehler
            }

            // Test
            Assert.AreEqual(true, Library.isCreated(id) && Library.text(id, "anderer text"));

            // Tear Down
            Library.close();
        }
        [TestMethod]
        public void Text_TextNegative()
        {
            // Setup
            Library.init("Test", false, 640, 480);
            uint id = Library.addText("data/fonts/arial.ttf");
            if (!Utility.CheckCreation(id, 1)) // Erstellung 1s lang prüfen
            {
                Assert.Equals(0, 1); // konnte das Objekt nicht innerhalb von 1s erstellen => Fehler
            }

            // Test
            Assert.AreEqual(true, Library.text(id, ""));

            // Tear Down
            Library.close();
        }

        [TestMethod]
        public void Text_TextSize()
        {
            // Setup
            Library.init("Test", false, 640, 480);
            uint id = Library.addText("data/fonts/arial.ttf");
            if (!Utility.CheckCreation(id, 1)) // Erstellung 1s lang prüfen
            {
                Assert.Equals(0, 1); // konnte das Objekt nicht innerhalb von 1s erstellen => Fehler
            }

            // Test
            Assert.AreEqual(true, Library.textSize(id, 20));

            // Tear Down
            Library.close();
        }

        [TestMethod]
        public void Text_TextSizeNegativeSize()
        {
            // Setup
            Library.init("Test", false, 640, 480);
            uint id = Library.addText("data/fonts/arial.ttf");
            if (!Utility.CheckCreation(id, 1)) // Erstellung 1s lang prüfen
            {
                Assert.Equals(0, 1); // konnte das Objekt nicht innerhalb von 1s erstellen => Fehler
            }

            // Test
            Assert.AreEqual(true, Library.textSize(id, -20));

            // Tear Down
            Library.close();
        }

        [TestMethod]
        public void Text_TextSizeZero()
        {
            // Setup
            Library.init("Test", false, 640, 480);
            uint id = Library.addText("data/fonts/arial.ttf");
            if (!Utility.CheckCreation(id, 1)) // Erstellung 1s lang prüfen
            {
                Assert.Equals(0, 1); // konnte das Objekt nicht innerhalb von 1s erstellen => Fehler
            }

            // Test
            Assert.AreEqual(true, Library.textSize(id, 0));

            // Tear Down
            Library.close();
        }

        [TestMethod]
        public void Text_TextSizeNegative()
        {
            // Setup
            Library.init("Test", false, 640, 480);
            uint id = Library.addText("data/fonts/arial.ttf");
            if (!Utility.CheckCreation(id, 1)) // Erstellung 1s lang prüfen
            {
                Assert.Equals(0, 1); // konnte das Objekt nicht innerhalb von 1s erstellen => Fehler
            }
            while (Library.isCreated(id)) // Model finden das nicht existiert
            {
                id++;
            }

            // Test
            Assert.AreEqual(false, Library.textSize(id, 20));

            // Tear Down
            Library.close();
        }

        [TestMethod]
        public void Text_TextColor()
        {
            // Setup
            Library.init("Test", false, 640, 480);
            uint id = Library.addText("data/fonts/arial.ttf");
            if (!Utility.CheckCreation(id, 1)) // Erstellung 1s lang prüfen
            {
                Assert.Equals(0, 1); // konnte das Objekt nicht innerhalb von 1s erstellen => Fehler
            }

            // Test
            Assert.AreEqual(true, Library.textColor(id, 1, 1, 1, 1));

            // Tear Down
            Library.close();
        }

        [TestMethod]
        public void Text_TextColorOutOfBounds()
        {
            // Setup
            Library.init("Test", false, 640, 480);
            uint id = Library.addText("data/fonts/arial.ttf");
            if (!Utility.CheckCreation(id, 1)) // Erstellung 1s lang prüfen
            {
                Assert.Equals(0, 1); // konnte das Objekt nicht innerhalb von 1s erstellen => Fehler
            }

            // Test
            Assert.AreEqual(true, Library.textColor(id, 2, 2, 2, 2));

            // Tear Down
            Library.close();
        }

        [TestMethod]
        public void Text_TextColorOutOfBoundsNegative()
        {
            // Setup
            Library.init("Test", false, 640, 480);
            uint id = Library.addText("data/fonts/arial.ttf");
            if (!Utility.CheckCreation(id, 1)) // Erstellung 1s lang prüfen
            {
                Assert.Equals(0, 1); // konnte das Objekt nicht innerhalb von 1s erstellen => Fehler
            }

            // Test
            Assert.AreEqual(true, Library.textColor(id, -2, -2, -2, -2));

            // Tear Down
            Library.close();
        }

        [TestMethod]
        public void Text_TextColorNegative() // Model das nicht existiert
        {
            // Setup
            Library.init("Test", false, 640, 480);
            uint id = Library.addText("data/fonts/arial.ttf");
            if (!Utility.CheckCreation(id, 1)) // Erstellung 1s lang prüfen
            {
                Assert.Equals(0, 1); // konnte das Objekt nicht innerhalb von 1s erstellen => Fehler
            }
            while (Library.isCreated(id)) // Model finden das nicht existiert
            {
                id++;
            }

            // Test
            Assert.AreEqual(false, Library.textColor(id, 1, 1, 1, 1));

            // Tear Down
            Library.close();
        }
    }
}
