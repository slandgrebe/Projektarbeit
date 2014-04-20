using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VisualizationLibraryTest
{
    [TestClass]
    public class Window
    {
        /*private static String resultMessage = "";

        public static void RunAllTests()
        {
            int errors = 0;
            Console.WriteLine("Window:");


            if (!NotYetOpen()) errors++;
            if (!Init()) errors++;
            Close();

            Console.WriteLine(errors + " Tests failed");
            Console.WriteLine();
            
            if (errors > 0)
            {
                resultMessage = "Window: " + errors + " Tests failed.";
            }
            else
            {
                resultMessage = "Window: All Tests run successfully";
            }
        }

        public static String ResultMessage()
        {
            return resultMessage;
        }

        public static bool NotYetOpen()
        {
            bool result = false;

            // Setup

            // Test
            if (Library.isRunning())
            {
                Console.WriteLine("FAIL: NotYetOpen");
            }
            else {
                Console.WriteLine("SUCCESS: NotYetOpen");
                result = true;
            }

            // Tear Down

            return result;
        }

        public static bool Init()
        {
            bool result = false;

            // Setup

            // Test
            if (Library.init("Fenstertitel", false, 640, 480))
            {
                Console.WriteLine("SUCCESS: Init");
                result = true;
            }
            else
            {
                Console.WriteLine("FAIL: Init");
            }

            // Tear Down
            Library.close();

            return result;
        }

        public static bool Close()
        {
            bool result = false;

            // Setup
            Library.init("Fenstertitel", false, 640, 480);
            Library.close();

            // Test
            if (Library.isRunning())
            {
                Console.WriteLine("FAIL: Close");
            }
            else
            {
                Console.WriteLine("SUCCESS: Close");
                result = true;
            }

            // Tear Down

            return result;
        }*/

        
        
        // Nicht funktionierende Test Cases .. oder doch?
        [TestMethod]
        [TestCategory("Window")]
        public void Window_NotYetOpen()
        {
            Assert.AreEqual(false, Library.isRunning());
            Library.close();
        }

        [TestMethod]
        [TestCategory("Window")]
        public void Window_Init()
        {
            Assert.AreEqual(true, Library.init("Fenstertitel", false, 640, 480));
            Library.close();
        }

        [TestMethod]
        [TestCategory("Window")]
        public void Window_Init_EmptyTitle()
        {
            Assert.AreEqual(true, Library.init("", false, 640, 480));
            Library.close();
        }

        [TestMethod]
        [TestCategory("Window")]
        public void Window_Init_FullscreenLowResolution()
        {
            Assert.AreEqual(true, Library.init("Fenstertitel", true, 640, 480));
            Library.close();
        }

        [TestMethod]
        [TestCategory("Window")]
        public void Window_Init_FullscreenNativeResolution()
        {
            Assert.AreEqual(true, Library.init("Fenstertitel", false, 0, 0));
            Library.close();
        }

        [TestMethod]
        [TestCategory("Window")]
        public void Window_Init_NativeResolution()
        {
            Assert.AreEqual(true, Library.init("Fenstertitel", false, 0, 0));
            Library.close();
        }

        [TestMethod]
        [TestCategory("Window")]
        public void Window_Init_ResolutionOverkill()
        {
            Assert.AreEqual(true, Library.init("Fenstertitel", false, 64000, 48000));
            Library.close();
        }

        [TestMethod]
        [TestCategory("Window")]
        public void Window_Init_FullscreenResolutionOverkill()
        {
            Assert.AreEqual(true, Library.init("Fenstertitel", false, 64000, 48000));
            Library.close();
        }

        [TestMethod]
        public void Window_Init_()
        {
            Assert.AreEqual(true, Library.init("Fenstertitel", false, 640, 480));
            Library.close();
        }

        [TestMethod]
        public void Window_Close()
        {
            Library.init("Fenstertitel", false, 640, 480);
            Library.close();
            Assert.AreEqual(false, Library.isRunning());
        }

        [TestMethod]
        public void Window_InitTwoTimes()
        {
            Library.init("Fenstertitel", false, 640, 480);
            Assert.AreEqual(false, Library.init("Fenstertitel", false, 640, 480));
            Library.close();
        }

        [TestMethod]
        public void Window_CloseTwoTimes()
        {
            Library.init("Fenstertitel", false, 640, 480);
            Library.close();
            Library.close();
            Assert.AreEqual(false, Library.isRunning());
        }
    }
}
