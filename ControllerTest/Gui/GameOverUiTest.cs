using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JumpAndRun;

namespace ControllerTest.Gui
{
    [TestClass]
    public class GameOverUiTest
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
        public void GameOverUi_Show()
        {
            GameOverUi ui = new GameOverUi();
            ui.Show();
            Assert.AreEqual(true, ui.IsShow, "Gui wird angezeigt.");
        }

        [TestMethod]
        public void GameOverUi_Hide()
        {
            GameOverUi ui = new GameOverUi();
            Assert.AreEqual(false, ui.IsShow, "Gui wird nach Initialisierung nicht angezeigt.");
            ui.Show();
            ui.Hide();
            Assert.AreEqual(false, ui.IsShow, "Gui wird nicht angezeigt.");
        }

        [TestMethod]
        public void GameOverUi_HoverButtonTrue()
        {
            GameOverUi ui = new GameOverUi();
            Assert.AreEqual(true, ui.HoverButton(0,0), "Hand ist auf dem Button positioniert.");
        }

        [TestMethod]
        public void GameOverUi_HoverButtonFalseOver()
        {
            GameOverUi ui = new GameOverUi();
            Assert.AreEqual(false, ui.HoverButton(0,0.1f), "Hand ist nicht auf dem Button positioniert.");
        }

        [TestMethod]
        public void GameOverUi_HoverButtonFalseUnder()
        {
            GameOverUi ui = new GameOverUi();
            Assert.AreEqual(false, ui.HoverButton(0,-0.1f), "Hand ist nicht auf dem Button positioniert.");
        }

        [TestMethod]
        public void GameOverUi_HoverButtonFalseLeft()
        {
            GameOverUi ui = new GameOverUi();
            Assert.AreEqual(false, ui.HoverButton(0.1f,0), "Hand ist nicht auf dem Button positioniert.");
        }

        [TestMethod]
        public void GameOverUi_HoverButtonFalseRight()
        {
            GameOverUi ui = new GameOverUi();
            Assert.AreEqual(false, ui.HoverButton(-0.1f,0), "Hand ist nicht auf dem Button positioniert.");
        }
    }
}
