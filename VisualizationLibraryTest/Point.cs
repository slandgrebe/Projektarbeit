using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VisualizationLibraryTest
{
    [TestClass]
    public class Point
    {
        /*private static String resultMessage = "";

        public static void RunAllTests()
        {
            int errors = 0;
            Console.WriteLine("Point:");

            if (!AddWithoutExistingWindowWithData()) errors++;
            if (!AddWithExistingWindowWithData()) errors++;
            if (!AddWithoutExistingWindowWithoutData()) errors++;
            if (!AddWithExistingWindowWithoutData()) errors++;
            if (!IsCreated()) errors++;
            if (!IsCreatedNegative()) errors++;
            if (!Dispose()) errors++;
            
            if (!Position()) errors++;
            if (!Rotate()) errors++;
            if (!RotateWithoutAxis()) errors++;
            if (!Scale()) errors++;
            if (!ScaleNegative()) errors++;
            if (!ScalingIsNormalized()) errors++;
            if (!HighlightColor()) errors++;
            if (!HighlightColorOutOfBoundsNegative()) errors++;
            if (!HighlightColorOutOfBounds()) errors++;
            if (!IsHighlighted()) errors++;




            Console.WriteLine(errors + " Tests failed");
            Console.WriteLine();

            if (errors > 0)
            {
                resultMessage = "Point: " + errors + " Tests failed.";
            }
            else
            {
                resultMessage = "Point: All Tests run successfully";
            }
        }

        public static String ResultMessage()
        {
            return resultMessage;
        }

        public static bool AddWithoutExistingWindowWithData()
        {
            bool result = false;

            // Setup
            uint id = Library.addPoint("data/textures/sample.png");

            // Test
            if (id != 0)
            {
                Console.WriteLine("SUCCESS: AddWithoutExistingWindowWithData");
                result = true;
            }
            else 
            {
                Console.WriteLine("FAIL: AddWithoutExistingWindowWithData");
            }

            // Tear Down
            Library.close();

            return result;
        }

        public static bool AddWithExistingWindowWithData()
        {
            bool result = false;

            // Setup
            Library.init("Test", false, 640, 480);
            uint id = Library.addPoint("data/textures/sample.png");

            // Test
            if (id != 0)
            {
                Console.WriteLine("SUCCESS: AddWithExistingWindowWithData");
                result = true;
            }
            else
            {
                Console.WriteLine("FAIL: AddWithExistingWindowWithData");
            }

            // Tear Down
            Library.close();

            return result;
        }

        public static bool AddWithoutExistingWindowWithoutData()
        {
            bool result = false;

            // Setup
            uint id = Library.addPoint("");

            // Test
            if (id != 0)
            {
                Console.WriteLine("SUCCESS: AddWithoutExistingWindowWithoutData");
                result = true;
            }
            else
            {
                Console.WriteLine("FAIL: AddWithoutExistingWindowWithoutData");
            }

            // Tear Down
            Library.close();

            return result;
        }

        public static bool AddWithExistingWindowWithoutData()
        {
            bool result = false;

            // Setup
            Library.init("Test", false, 640, 480);
            uint id = Library.addPoint("");

            // Test
            if (id != 0)
            {
                Console.WriteLine("SUCCESS: AddWithExistingWindowWithoutData");
                result = true;
            }
            else
            {
                Console.WriteLine("FAIL: AddWithExistingWindowWithoutData");
            }

            // Tear Down
            Library.close();

            return result;
        }

        public static bool IsCreated()
        {
            bool result = false;

            // Setup
            Library.init("Test", false, 640, 480);
            uint id = Library.addPoint("data/textures/sample.png");

            //while (!Library.isCreated(id)) { }
            System.Threading.Thread.Sleep(100);

            // Test
            if (Library.isCreated(id))
            {
                Console.WriteLine("SUCCESS: IsCreated");
                result = true;
            }
            else
            {
                Console.WriteLine("FAIL: IsCreated");
            }

            // Tear Down
            Library.close();

            return result;
        }

        public static bool IsCreatedNegative()
        {
            bool result = false;

            // Setup
            Library.init("Test", false, 640, 480);

            // Test
            if (Library.isCreated(99))
            {
                Console.WriteLine("FAIL: IsCreatedNegative");
            }
            else
            {
                Console.WriteLine("SUCCESS: IsCreatedNegative");
                result = true;
            }

            // Tear Down
            Library.close();

            return result;
        }

        public static bool Dispose()
        {
            bool result = false;

            // Setup
            Library.init("Test", false, 640, 480);
            uint id = Library.addPoint("data/textures/sample.png");
            System.Threading.Thread.Sleep(100); // Multithreading
            Library.dispose(id);
            System.Threading.Thread.Sleep(100); // Multithreading

            // Test
            if (Library.isCreated(id))
            {
                Console.WriteLine("FAIL: Dispose");
            }
            else
            {
                Console.WriteLine("SUCCESS: Dispose");
                result = true;
            }

            // Tear Down
            Library.close();

            return result;
        }

        public static bool Position()
        {
            bool result = false;

            // Setup
            Library.init("Test", false, 640, 480);
            uint id = Library.addPoint("data/textures/sample.png");
            System.Threading.Thread.Sleep(100); // Multithreading

            // Test
            if (Library.position(id, 1, 1, 1))
            {
                Console.WriteLine("SUCCESS: Position");
                result = true;
            }
            else
            {
                Console.WriteLine("FAIL: Position");
            }

            // Tear Down
            Library.close();

            return result;
        }

        public static bool Rotate()
        {
            bool result = false;

            // Setup
            Library.init("Test", false, 640, 480);
            uint id = Library.addPoint("data/textures/sample.png");
            System.Threading.Thread.Sleep(100); // Multithreading

            // Test
            if (Library.rotate(id, 90, 1, 1, 1))
            {
                Console.WriteLine("SUCCESS: Rotate");
                result = true;
            }
            else
            {
                Console.WriteLine("FAIL: Rotate");
            }

            // Tear Down
            Library.close();

            return result;
        }

        public static bool RotateWithoutAxis()
        {
            bool result = false;

            // Setup
            Library.init("Test", false, 640, 480);
            uint id = Library.addPoint("data/textures/sample.png");
            System.Threading.Thread.Sleep(100); // Multithreading

            // Test
            if (Library.rotate(id, 90, 0, 0, 0))
            {
                Console.WriteLine("SUCCESS: RotateWithoutAxis");
                result = true;
            }
            else
            {
                Console.WriteLine("FAIL: RotateWithoutAxis");
            }

            // Tear Down
            Library.close();

            return result;
        }

        public static bool Scale()
        {
            bool result = false;

            // Setup
            Library.init("Test", false, 640, 480);
            uint id = Library.addPoint("data/textures/sample.png");
            System.Threading.Thread.Sleep(100); // Multithreading

            // Test
            if (Library.scale(id, 0.5f, 0.5f, 0.5f))
            {
                Console.WriteLine("SUCCESS: Scale");
                result = true;
            }
            else
            {
                Console.WriteLine("FAIL: Scale");
            }

            // Tear Down
            Library.close();

            return result;
        }

        public static bool ScaleNegative()
        {
            bool result = false;

            // Setup
            Library.init("Test", false, 640, 480);
            uint id = Library.addPoint("data/textures/sample.png");
            System.Threading.Thread.Sleep(100); // Multithreading

            // Test
            if (Library.scale(id, -0.5f, -0.5f, -0.5f))
            {
                Console.WriteLine("SUCCESS: ScaleNegative");
                result = true;
            }
            else
            {
                Console.WriteLine("FAIL: ScaleNegative");
            }

            // Tear Down
            Library.close();

            return result;
        }

        public static bool ScalingIsNormalized()
        {
            bool result = false;

            // Setup
            Library.init("Test", false, 640, 480);
            uint id = Library.addPoint("data/textures/sample.png");
            System.Threading.Thread.Sleep(100); // Multithreading

            // Test
            if (Library.scalingIsNormalized(id, true))
            {
                Console.WriteLine("SUCCESS: ScalingIsNormalized");
                result = true;
            }
            else
            {
                Console.WriteLine("FAIL: ScalingIsNormalized");
            }

            // Tear Down
            Library.close();

            return result;
        }

        public static bool HighlightColor()
        {
            bool result = false;

            // Setup
            Library.init("Test", false, 640, 480);
            uint id = Library.addPoint("data/textures/sample.png");
            System.Threading.Thread.Sleep(100); // Multithreading

            // Test
            if (Library.highlightColor(id, 1.0f, 1.0f, 1.0f, 1.0f))
            {
                Console.WriteLine("SUCCESS: HighlightColor");
                result = true;
            }
            else
            {
                Console.WriteLine("FAIL: HighlightColor");
            }

            // Tear Down
            Library.close();

            return result;
        }

        public static bool HighlightColorOutOfBounds()
        {
            bool result = false;

            // Setup
            Library.init("Test", false, 640, 480);
            uint id = Library.addPoint("data/textures/sample.png");
            System.Threading.Thread.Sleep(100); // Multithreading

            // Test
            if (Library.highlightColor(id, 2.0f, 2.0f, 2.0f, 2.0f))
            {
                Console.WriteLine("SUCCESS: HighlightColorOutOfBounds");
                result = true;
            }
            else
            {
                Console.WriteLine("FAIL: HighlightColorOutOfBounds");
            }

            // Tear Down
            Library.close();

            return result;
        }

        public static bool HighlightColorOutOfBoundsNegative()
        {
            bool result = false;

            // Setup
            Library.init("Test", false, 640, 480);
            uint id = Library.addPoint("data/textures/sample.png");
            System.Threading.Thread.Sleep(100); // Multithreading

            // Test
            if (Library.highlightColor(id, -2.0f, -2.0f, -2.0f, -2.0f))
            {
                Console.WriteLine("SUCCESS: HighlightColorOutOfBoundsNegative");
                result = true;
            }
            else
            {
                Console.WriteLine("FAIL: HighlightColorOutOfBoundsNegative");
            }

            // Tear Down
            Library.close();

            return result;
        }

        public static bool IsHighlighted()
        {
            bool result = false;

            // Setup
            Library.init("Test", false, 640, 480);
            uint id = Library.addPoint("data/textures/sample.png");
            System.Threading.Thread.Sleep(100); // Multithreading

            // Test
            if (Library.isHighlighted(id, true))
            {
                Console.WriteLine("SUCCESS: IsHighlighted");
                result = true;
            }
            else
            {
                Console.WriteLine("FAIL: IsHighlighted");
            }

            // Tear Down
            Library.close();

            return result;
        }*/




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
            //System.Threading.Thread.Sleep(100); // Multithreading

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
            while (Library.isCreated(id)) // finde nicht existierende Id
            {
                id++;
            }
            Library.dispose(id);

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
            while (Library.isCreated(id)) // finde nicht existierende Id
            {
                id++;
            }

            // Test
            Assert.AreEqual(false, Library.position(id, 1, 1, 1));

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
            while (Library.isCreated(id)) // finde nicht existierende Id
            {
                id++;
            }

            // Test
            Assert.AreEqual(false, Library.rotate(id, 90, 1, 1, 1));

            // Tear Down
            //if (!Utility.TearDown(id)) Assert.AreEqual(1, 0); // da ging was schief
            Library.close();
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
            while (Library.isCreated(id)) // finde nicht existierende Id
            {
                id++;
            }

            // Test
            Assert.AreEqual(false, Library.scale(id, 1, 1, 1));

            // Tear Down
            //if (!Utility.TearDown(id)) Assert.AreEqual(1, 0); // da ging was schief
            Library.close();
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
            while (Library.isCreated(id)) // finde nicht existierende Id
            {
                id++;
            }

            // Test
            Assert.AreEqual(false, Library.scalingIsNormalized(id, true));

            // Tear Down
            //if (!Utility.TearDown(id)) Assert.AreEqual(1, 0); // da ging was schief
            Library.close();
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
            while (Library.isCreated(id)) // finde nicht existierende Id
            {
                id++;
            }

            // Test
            Assert.AreEqual(false, Library.highlightColor(id, 1, 1, 1, 1));

            // Tear Down
            //if (!Utility.TearDown(id)) Assert.AreEqual(1, 0); // da ging was schief
            Library.close();
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
            while (Library.isCreated(id)) // finde nicht existierende Id
            {
                id++;
            }

            // Test
            Assert.AreEqual(false, Library.isHighlighted(id, true));

            // Tear Down
            //if (!Utility.TearDown(id)) Assert.AreEqual(1, 0); // da ging was schief
            Library.close();
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
            while (Library.isCreated(id)) // finde nicht existierende Id
            {
                id++;
            }

            // Test
            Assert.AreEqual(false, Library.attachToCamera(id, true));

            // Tear Down
            //if (!Utility.TearDown(id)) Assert.AreEqual(1, 0); // da ging was schief
            Library.close();
        }
    }
}
