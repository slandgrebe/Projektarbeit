using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JumpAndRun;

namespace ControllerTest.Gesture
{
    [TestClass]
    public class ClickTest
    {

        [TestMethod]
        public void Click_IsClickedTrue()
        {
            Click click = new Click();
            MotionDetection.Body.Instance.Scale(1);
            MotionDetection.Body.Instance.XModifikator(0);
            MotionDetection.Body.Instance.YModifikator(0);
            MotionDetection.Body.Instance.ZModifikator(0);
            Data.SetBody();
            MotionDetection.Body.Instance.HandRight.Z = -1f;
            Assert.AreEqual(false, click.IsClicked(), "Click wurde erst initialisiert.");
            MotionDetection.Body.Instance.HandRight.Z -= 0.01f;
            Assert.AreEqual(false, click.IsClicked(), "Hand wurde noch nicht genügend nach vorne bewegt." + click.HandRightStart.ToString());
            MotionDetection.Body.Instance.HandRight.Z -= 0.02f;
            Assert.AreEqual(true, click.IsClicked(), "Click wurde ausgeführt.");
        }

        [TestMethod]
        public void Click_IsClickedFalse()
        {
            Click click = new Click();
            MotionDetection.Body.Instance.Scale(1);
            MotionDetection.Body.Instance.XModifikator(0);
            MotionDetection.Body.Instance.YModifikator(0);
            MotionDetection.Body.Instance.ZModifikator(0);
            Data.SetBody();
            MotionDetection.Body.Instance.HandRight.Z = -1f;
            Assert.AreEqual(false, click.IsClicked(), "Click wurde erst initialisiert.");
            MotionDetection.Body.Instance.HandRight.Z -= 0.01f;
            Assert.AreEqual(false, click.IsClicked(), "Hand wurde noch nicht genügend nach vorne bewegt.");
            System.Threading.Thread.Sleep(510);
            MotionDetection.Body.Instance.HandRight.Z -= 0.02f;
            Assert.AreEqual(false, click.IsClicked(), "Zu langsam. Click muss wieder neu initialisiert werden.");
        }

        [TestMethod]
        public void Click_IsClickedFalseNegative()
        {
            Click click = new Click();
            MotionDetection.Body.Instance.Scale(1);
            MotionDetection.Body.Instance.XModifikator(0);
            MotionDetection.Body.Instance.YModifikator(0);
            MotionDetection.Body.Instance.ZModifikator(0);
            Data.SetBody();
            MotionDetection.Body.Instance.HandRight.Z = -1f;
            Assert.AreEqual(false, click.IsClicked(), "Click wurde erst initialisiert.");
            MotionDetection.Body.Instance.HandRight.Z += 0.02f;
            Assert.AreEqual(false, click.IsClicked(), "Hand wurde nicht nach vorne bewegt.");
        }
    }
}
