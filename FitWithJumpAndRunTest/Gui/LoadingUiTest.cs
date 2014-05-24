using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JumpAndRun;

namespace JumpAndRun.Gui
{
    [TestClass]
    public class LoadingUiTest
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
        public void LoadingUi_Show()
        {
            LoadingUi ui = new LoadingUi();
            ui.Show();
            Assert.AreEqual(true, ui.IsShow, "Gui wird angezeigt.");
        }

        [TestMethod]
        public void LoadingUi_Hide()
        {
            LoadingUi ui = new LoadingUi();
            Assert.AreEqual(false, ui.IsShow, "Gui wird nach Initialisierung nicht angezeigt.");
            ui.Show();
            ui.Hide();
            Assert.AreEqual(false, ui.IsShow, "Gui wird nicht angezeigt.");
        }
    }
}
