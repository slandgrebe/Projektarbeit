using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JumpAndRun;

namespace ControllerTest.Gui
{
    [TestClass]
    public class GameUiTest
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
        public void GameUi_Show()
        {
            GameUi ui = new GameUi();
            ui.Show();
            Assert.AreEqual(true, ui.IsShow, "Gui wird angezeigt.");
        }

        [TestMethod]
        public void GameUi_Hide()
        {
            GameUi ui = new GameUi();
            Assert.AreEqual(false, ui.IsShow, "Gui wird nach Initialisierung nicht angezeigt.");
            ui.Show();
            ui.Hide();
            Assert.AreEqual(false, ui.IsShow, "Gui wird nicht angezeigt.");
        }
    }
}
