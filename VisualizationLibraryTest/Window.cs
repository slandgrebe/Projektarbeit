using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VisualizationLibraryTest
{
    [TestClass]
    public class Window
    {
        private static String resultMessage = "";

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
        }

        // Nicht funktionierende Test Cases .. oder doch?
        [TestMethod]
        public void Window_NotYetOpen()
        {
            Assert.AreEqual(false, Library.isRunning());
            Library.close();
        }

        [TestMethod]
        public void Window_init()
        {
            Assert.AreEqual(true, Library.init("Fenstertitel", false, 640, 480));
            Library.close();
        }

        [TestMethod]
        public void Window_close()
        {
            Library.init("Fenstertitel", false, 640, 480);
            Library.close();
            Assert.AreEqual(false, Library.isRunning());
        }
    }
}
