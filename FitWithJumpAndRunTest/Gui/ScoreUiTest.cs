using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JumpAndRun;

namespace JumpAndRun.Gui
{
    [TestClass]
    public class ScoreUiTest
    {
        [TestInitialize]
        public void MyClassInitialize()
        {
            View.Window.Init("test", false, 100, 100);
        }

        [TestCleanup]
        public void MyClassCleanup()
        {
            View.Window.Close();
        }

        [TestMethod]
        public void ScoreUi_Show()
        {
            ScoreUi ui = new ScoreUi();
            ui.Show(5);
            Assert.AreEqual(true, ui.IsShow, "Gui wird angezeigt.");
        }

        [TestMethod]
        public void ScoreUi_Hide()
        {
            ScoreUi ui = new ScoreUi();
            Assert.AreEqual(false, ui.IsShow, "Gui wird nach Initialisierung nicht angezeigt.");
            ui.Show(5);
            ui.Hide();
            Assert.AreEqual(false, ui.IsShow, "Gui wird nicht angezeigt.");
        }

        [TestMethod]
        public void ScoreUi_HoverButtonTrue()
        {
            ScoreUi ui = new ScoreUi();
            Assert.AreEqual(true, ui.HoverButton(0, 0), "Hand ist auf dem Button positioniert.");
        }

        [TestMethod]
        public void ScoreUi_HoverButtonFalseOver()
        {
            ScoreUi ui = new ScoreUi();
            Assert.AreEqual(false, ui.HoverButton(0, 0.1f), "Hand ist nicht auf dem Button positioniert.");
        }

        [TestMethod]
        public void ScoreUi_HoverButtonFalseUnder()
        {
            ScoreUi ui = new ScoreUi();
            Assert.AreEqual(false, ui.HoverButton(0, -0.1f), "Hand ist nicht auf dem Button positioniert.");
        }

        [TestMethod]
        public void ScoreUi_HoverButtonFalseLeft()
        {
            ScoreUi ui = new ScoreUi();
            Assert.AreEqual(false, ui.HoverButton(0.1f, 0), "Hand ist nicht auf dem Button positioniert.");
        }

        [TestMethod]
        public void ScoreUi_HoverButtonFalseRight()
        {
            ScoreUi ui = new ScoreUi();
            Assert.AreEqual(false, ui.HoverButton(-0.1f, 0), "Hand ist nicht auf dem Button positioniert.");
        }
    }
}
