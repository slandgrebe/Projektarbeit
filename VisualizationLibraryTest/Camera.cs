using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VisualizationLibraryTest
{
    [TestClass]
    public class Camera
    {
        [TestMethod]
        public void Camera_Position()
        {
            // Setup
            Utility.InitWindow();

            // Test
            Library.positionCamera(1, 1, 1);
            Assert.AreEqual(1, 1); // keine Execption, mehr kann nicht getestet werden

            // Tear Down
            Library.close();
        }
        [TestMethod]
        public void Camera_Position_WithoutWindow()
        {
            // Setup

            // Test
            Library.positionCamera(1, 1, 1);
            Assert.AreEqual(1, 1); // keine Execption, mehr kann nicht getestet werden

            // Tear Down
            Library.close();
        }

        [TestMethod]
        public void Camera_Rotate()
        {
            // Setup
            Utility.InitWindow();

            // Test
            Library.rotateCamera(90);
            Assert.AreEqual(1, 1); // keine Execption, mehr kann nicht getestet werden

            // Tear Down
            Library.close();
        }
        [TestMethod]
        public void Camera_Rotate_WithoutWindow()
        {
            // Setup

            // Test
            Library.rotateCamera(90);
            Assert.AreEqual(1, 1); // keine Execption, mehr kann nicht getestet werden

            // Tear Down
            Library.close();
        }

        [TestMethod]
        public void Camera_Tilt()
        {
            // Setup
            Utility.InitWindow();

            // Test
            Library.tiltCamera(90);
            Assert.AreEqual(1, 1); // keine Execption, mehr kann nicht getestet werden

            // Tear Down
            Library.close();
        }
        [TestMethod]
        public void Camera_Tilt_WithoutWindow()
        {
            // Setup

            // Test
            Library.tiltCamera(90);
            Assert.AreEqual(1, 1); // keine Execption, mehr kann nicht getestet werden

            // Tear Down
            Library.close();
        }

        [TestMethod]
        public void Camera_ChangeCameraSpeed()
        {
            // Setup
            Utility.InitWindow();

            // Test
            Library.changeCameraSpeed(2);
            Assert.AreEqual(1, 1); // keine Execption, mehr kann nicht getestet werden

            // Tear Down
            Library.close();
        }
        [TestMethod]
        public void Camera_ChangeCameraSpeed_WithoutWindow()
        {
            // Setup

            // Test
            Library.changeCameraSpeed(2);
            Assert.AreEqual(1, 1); // keine Execption, mehr kann nicht getestet werden

            // Tear Down
            Library.close();
        }
    }
}