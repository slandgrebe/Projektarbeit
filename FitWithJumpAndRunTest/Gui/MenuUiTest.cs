using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JumpAndRun;

namespace JumpAndRun.Gui
{
    [TestClass]
    public class MenuUiTest
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
        public void MenuUi_Show()
        {
            MenuUi ui = new MenuUi();
            ui.Show();
            Assert.AreEqual(true, ui.IsShown, "Gui wird angezeigt.");
        }

        [TestMethod]
        public void MenuUi_Hide()
        {
            MenuUi ui = new MenuUi();
            Assert.AreEqual(false, ui.IsShown, "Gui wird nach Initialisierung nicht angezeigt.");
            ui.Show();
            ui.Hide();
            Assert.AreEqual(false, ui.IsShown, "Gui wird nicht angezeigt.");
        }

        [TestMethod]
        public void MenuUi_HoverButtonTrue()
        {
            MenuUi ui = new MenuUi();
            Assert.AreEqual(true, ui.HoverButton(0,0), "Hand ist auf dem Button positioniert.");
        }

        [TestMethod]
        public void MenuUi_HoverButtonFalseOver()
        {
            MenuUi ui = new MenuUi();
            Assert.AreEqual(false, ui.HoverButton(0,0.1f), "Hand ist nicht auf dem Button positioniert.");
        }

        [TestMethod]
        public void MenuUi_HoverButtonFalseUnder()
        {
            MenuUi ui = new MenuUi();
            Assert.AreEqual(false, ui.HoverButton(0,-0.1f), "Hand ist nicht auf dem Button positioniert.");
        }

        [TestMethod]
        public void MenuUi_HoverButtonFalseLeft()
        {
            MenuUi ui = new MenuUi();
            Assert.AreEqual(false, ui.HoverButton(0.1f,0), "Hand ist nicht auf dem Button positioniert.");
        }

        [TestMethod]
        public void MenuUi_HoverButtonFalseRight()
        {
            MenuUi ui = new MenuUi();
            Assert.AreEqual(false, ui.HoverButton(-0.1f,0), "Hand ist nicht auf dem Button positioniert.");
        }
    }
}
