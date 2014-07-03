using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JumpAndRun;
using JumpAndRun.GameLogic;

namespace JumpAndRun
{
    [TestClass]
    public class GameTest
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
        public void Game_InitWithoutPath()
        {
            Game game = Game.Instance;
            Assert.AreEqual(false, game.Load(""), "Ohne Pfad darf nicht initialisiert werden.");
            Assert.AreNotEqual(GameStatus.LoadingComplete, game.GameStatus, "Game Status darf nicht 'loadet' sein.");
        }

        [TestMethod]
        public void Game_StartFalseStatus()
        {
            Game game = Game.Instance;
            game.Load("data/levels/jungle/level.xml");
            game.GameStatus = GameStatus.Loading;
            game.Start();
            Assert.AreNotEqual(GameStatus.Playing, game.GameStatus, "Game Status darf nicht 'started' sein.");
        }

        [TestMethod]
        public void Game_Start()
        {
            Game game = Game.Instance;
            game.Load("data/levels/jungle/level.xml");
            game.GameStatus = GameStatus.LoadingComplete;
            game.Start();
            Assert.AreEqual(GameStatus.Playing, game.GameStatus, "Game Status muss 'started' sein.");
        }

        [TestMethod]
        public void Game_ResetGame()
        {
            Game game = Game.Instance;
            game.Load("data/levels/jungle/level.xml");
            game.ResetGame();
            Assert.AreEqual(GameStatus.Start, game.GameStatus, "Game Status muss 'start' sein.");
        }

        // hier müssen neue testdaten ersetellt werden
        /*[TestMethod]
        public void Game_CheckLevelEndTrue()
        {
            Game game = Game.Instance;
            game.Load("data/levels/jungle/level.xml");
            game.Start();
            Data.SetBody();
            View.Camera.PositionCamera(0, 1.5f, -50);
            game.Update();
            Assert.AreEqual(GameStatus.Successful, game.GameStatus, "Game Status muss 'successful' sein.");
        }*/

        [TestMethod]
        public void Game_CheckLevelEndFalse()
        {
            Game game = Game.Instance;
            game.Load("data/levels/jungle/level.xml");
            game.Start();
            Data.SetBody();
            game.Update();
            Assert.AreNotEqual(GameStatus.Successful, game.GameStatus, "Game Status darf nicht 'successful' sein.");
        }
    }
}
