using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MotionDetection;

namespace ModelTest
{
    [TestClass]
    public class BodyTest
    {
        [TestMethod]
        public void Body_XModifikatorPositivModifikatorPositivValue()
        {
            Body body = Body.Instance;
            body.XModifikator(5);
            body.Scale(1);
            body.Head.X = 10;
            Assert.AreEqual(15, body.Head.X, "Positive XModifikation eines positiven Wertes fehlgeschlagen.");
        }

        [TestMethod]
        public void Body_XModifikatorNegativModifikatorPositivValue()
        {
            Body body = Body.Instance;
            body.XModifikator(-5);
            body.Scale(1);
            body.Head.X = 10;
            Assert.AreEqual(5, body.Head.X, "Negative XModifikation eines positiven Wertes fehlgeschlagen.");
        }

        [TestMethod]
        public void Body_XModifikatorPositivModifikatorNegativValue()
        {
            Body body = Body.Instance;
            body.XModifikator(5);
            body.Scale(1);
            body.Head.X = -10;
            Assert.AreEqual(-5, body.Head.X, "Positive XModifikation eines negativen Wertes fehlgeschlagen.");
        }

        [TestMethod]
        public void Body_XModifikatorNegativModifikatorNegativValue()
        {
            Body body = Body.Instance;
            body.XModifikator(-5);
            body.Scale(1);
            body.Head.X = -10;
            Assert.AreEqual(-15, body.Head.X, "Negative XModifikation eines negativen Wertes fehlgeschlagen.");
        }

        [TestMethod]
        public void Body_XScale()
        {
            Body body = Body.Instance;
            body.XModifikator(0);
            body.Scale(0.5f);
            body.Head.X = 10;
            Assert.AreEqual(5, body.Head.X, "X Skaliering fehlgeschlagen.");
        }

        [TestMethod]
        public void Body_YModifikatorPositivModifikatorPositivValue()
        {
            Body body = Body.Instance;
            body.YModifikator(5);
            body.Scale(1);
            body.Head.Y = 10;
            Assert.AreEqual(15, body.Head.Y, "Positive YModifikation eines positiven Wertes fehlgeschlagen.");
        }

        [TestMethod]
        public void Body_YModifikatorNegativModifikatorPositivValue()
        {
            Body body = Body.Instance;
            body.YModifikator(-5);
            body.Scale(1);
            body.Head.Y = 10;
            Assert.AreEqual(5, body.Head.Y, "Negative YModifikation eines positiven Wertes fehlgeschlagen.");
        }

        [TestMethod]
        public void Body_YModifikatorPositivModifikatorNegativValue()
        {
            Body body = Body.Instance;
            body.YModifikator(5);
            body.Scale(1);
            body.Head.Y = -10;
            Assert.AreEqual(-5, body.Head.Y, "Positive YModifikation eines negativen Wertes fehlgeschlagen.");
        }

        [TestMethod]
        public void Body_YModifikatorNegativModifikatorNegativValue()
        {
            Body body = Body.Instance;
            body.YModifikator(-5);
            body.Scale(1);
            body.Head.Y = -10;
            Assert.AreEqual(-15, body.Head.Y, "Negative YModifikation eines negativen Wertes fehlgeschlagen.");
        }

        [TestMethod]
        public void Body_YScale()
        {
            Body body = Body.Instance;
            body.YModifikator(0);
            body.Scale(0.5f);
            body.Head.Y = 10;
            Assert.AreEqual(5, body.Head.Y, "Y Skaliering fehlgeschlagen.");
        }

        [TestMethod]
        public void Body_ZModifikatorPositivModifikatorPositivValue()
        {
            Body body = Body.Instance;
            body.ZModifikator(5);
            body.Scale(1);
            body.Head.Z = 10;
            Assert.AreEqual(15, body.Head.Z, "Positive ZModifikation eines positiven Wertes fehlgeschlagen.");
        }

        [TestMethod]
        public void Body_ZModifikatorNegativModifikatorPositivValue()
        {
            Body body = Body.Instance;
            body.ZModifikator(-5);
            body.Scale(1);
            body.Head.Z = 10;
            Assert.AreEqual(5, body.Head.Z, "Negative ZModifikation eines positiven Wertes fehlgeschlagen.");
        }

        [TestMethod]
        public void Body_ZModifikatorPositivModifikatorNegativValue()
        {
            Body body = Body.Instance;
            body.ZModifikator(5);
            body.Scale(1);
            body.Head.Z = -10;
            Assert.AreEqual(-5, body.Head.Z, "Positive ZModifikation eines negativen Wertes fehlgeschlagen.");
        }

        [TestMethod]
        public void Body_ZModifikatorNegativModifikatorNegativValue()
        {
            Body body = Body.Instance;
            body.ZModifikator(-5);
            body.Scale(1);
            body.Head.Z = -10;
            Assert.AreEqual(-15, body.Head.Z, "Negative ZModifikation eines negativen Wertes fehlgeschlagen.");
        }

        [TestMethod]
        public void Body_ZScale()
        {
            Body body = Body.Instance;
            body.ZModifikator(0);
            body.Scale(0.5f);
            body.Head.Z = 10;
            Assert.AreEqual(5, body.Head.Z, "Z Skaliering fehlgeschlagen.");
        }

        [TestMethod]
        public void Body_IsTracked()
        {
            Body body = Body.Instance;
            Assert.IsFalse(body.IsTracked, "Tracking Status muss per default auf False sein.");
            body.IsTracked = true;
            Assert.IsTrue(body.IsTracked, "Tracking Status muss nach dem setzen auf True sein.");
            System.Threading.Thread.Sleep(1900);
            Assert.IsTrue(body.IsTracked, "Tracking Status muss für 2 Sekunden auf True bleiben.");
            System.Threading.Thread.Sleep(200);
            Assert.IsFalse(body.IsTracked, "Tracking Status muss nach 2 Sekunden auf False fallen.");
        }
    }
}
