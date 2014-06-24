using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JumpAndRun;

namespace JumpAndRun.Gui
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
            GameUi ui = GameUi.Instance;
            ui.Show();
            ui.
            Assert.AreEqual()
            //Assert.AreEqual(true, ui.IsShow, "Gui wird angezeigt.");
        }

        [TestMethod]
        public void GameUi_Hide()
        {
            GameUi ui = GameUi.Instance;
            Assert.AreEqual(false, ui.IsShow, "Gui wird nach Initialisierung nicht angezeigt.");
            ui.Show();
            ui.Hide();
            Assert.AreEqual(false, ui.IsShow, "Gui wird nicht angezeigt.");
        }
    }
}
