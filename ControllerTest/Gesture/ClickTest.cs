using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Controller;

namespace ControllerTest.Gesture
{
    [TestClass]
    public class ClickTest
    {

        [TestMethod]
        public void Click_IsClickedTrue()
        {
            Click click = new Click();
            Model.Body.Instance.Scale(1);
            Model.Body.Instance.HandRight.Z = -1f;
            Assert.AreEqual(false, click.IsClicked(), "Click wurde erst initialisiert.");
            Model.Body.Instance.HandRight.Z -= 0.01f;
            Assert.AreEqual(false, click.IsClicked(), "Hand wurde noch nicht genügend nach vorne bewegt.");
            Model.Body.Instance.HandRight.Z -= 0.02f;
            Assert.AreEqual(true, click.IsClicked(), "Click wurde ausgeführt.");
        }

        [TestMethod]
        public void Click_IsClickedFalse()
        {
            Click click = new Click();
            Model.Body.Instance.Scale(1);
            Model.Body.Instance.HandRight.Z = -1f;
            Assert.AreEqual(false, click.IsClicked(), "Click wurde erst initialisiert.");
            Model.Body.Instance.HandRight.Z -= 0.01f;
            Assert.AreEqual(false, click.IsClicked(), "Hand wurde noch nicht genügend nach vorne bewegt.");
            System.Threading.Thread.Sleep(510);
            Model.Body.Instance.HandRight.Z -= 0.02f;
            Assert.AreEqual(false, click.IsClicked(), "Zu langsam. Click muss wieder neu initialisiert werden.");
        }

        [TestMethod]
        public void Click_IsClickedFalseNegative()
        {
            Click click = new Click();
            Model.Body.Instance.Scale(1);
            Model.Body.Instance.HandRight.Z = -1f;
            Assert.AreEqual(false, click.IsClicked(), "Click wurde erst initialisiert.");
            Model.Body.Instance.HandRight.Z += 0.02f;
            Assert.AreEqual(false, click.IsClicked(), "Hand wurde nicht nach vorne bewegt.");
        }
    }
}
