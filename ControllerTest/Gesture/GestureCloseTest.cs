using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JumpAndRun;
using MotionDetection;

namespace ControllerTest.Gesture
{
    [TestClass]
    public class GestureCloseTest
    {
        [TestMethod]
        public void GestureClose_IsTrueTrue()
        {
            Body.Instance.Scale(1);
            Data.SetBody();
            Body.Instance.HandLeft.Y = 2;
            Body.Instance.HandRight.Y = 2;
            Body.Instance.HandLeft.X = 2.5f;
            Body.Instance.HandRight.X = -2.5f;
            Assert.AreEqual(true, GestureClose.IsTrue(), "Geste zum schliessen wurde ausgeführt.");
        }

        [TestMethod]
        public void GestureClose_IsTrueFalse()
        {
            Body.Instance.Scale(1);
            Data.SetBody();
            Assert.AreEqual(false, GestureClose.IsTrue(), "Body wurde erst in Grundstellung gebracht.");
        }


        [TestMethod]
        public void GestureClose_IsTrueFalseNotCrossedHands()
        {
            Body.Instance.Scale(1);
            Data.SetBody();
            Body.Instance.HandLeft.Y = 2;
            Body.Instance.HandRight.Y = 2;
            Assert.AreEqual(false, GestureClose.IsTrue(), "Hände sind nicht gekreuzt.");
        }

        [TestMethod]
        public void GestureClose_IsTrueFalseHandsUnderElbows()
        {
            Body.Instance.Scale(1);
            Data.SetBody();
            Body.Instance.HandLeft.X = 2.5f;
            Body.Instance.HandRight.X = -2.5f;
            Assert.AreEqual(false, GestureClose.IsTrue(), "Hände sind unterhalb der Ellbogen.");
        }

        [TestMethod]
        public void GestureClose_IsTrueFalseElbowOverShoulder()
        {
            Data.SetBody();
            Body.Instance.HandLeft.Y = 5;
            Body.Instance.HandRight.Y = 5;
            Body.Instance.ElbowLeft.Y = 4;
            Body.Instance.ElbowRight.Y = 4;
            Assert.AreEqual(false, GestureClose.IsTrue(), "Ellbogen sind oberhalb der Schultern.");
        }
    }
}
