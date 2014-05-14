using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ControllerTest
{
    [TestClass]
    public class ObjectTest
    {
        private static Object obj;
        
        [ClassInitialize]
        public static void MyClassInitialize(TestContext testContext)
        {
            View.Window.Init("test", false, 10, 10);
            obj = new Object();
        }

        [ClassCleanup]
        public static void MyClassCleanup()
        {
            View.Window.Close();
        }

        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}
