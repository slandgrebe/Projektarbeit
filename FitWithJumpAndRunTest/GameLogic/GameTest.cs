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
        
        /*[TestMethod]
        public void Game_InitWithoutPath()
        {
            Game game = Game.Instance;
            Assert.AreEqual(false, game.Load(""), "Ohne Pfad darf nicht initialisiert werden.");
            Assert.AreNotEqual(GameStatus.LoadingComplete, game.GameStatus, "Game Status darf nicht 'loadet' sein.");
        }

        [TestMethod]
        public void Game_InitWithPath()
        {
            Game game = Game.Instance;
            game.LevelXmlPath = "/data/levels/test/level.xml";
            Assert.AreEqual(true, game.Load(), "Das Level konnte nicht initialisiert werden.");
            Assert.IsNotNull(game.Player, "Spieler wurde nicht erstellt.");
            Assert.AreEqual(3, game.level.AllAvailableSegments.Count, "Level wurde nicht deserialisiert.");
            Assert.AreEqual(GameStatus.LoadingComplete, game.GameStatus, "Game Status ist nicht 'loadet'");
        }

        [TestMethod]
        public void Game_StartFalseStatus()
        {
            Game game = Game.Instance;
            game.LevelXmlPath = "/data/levels/test/level.xml";
            game.GameStatus = GameStatus.Loading;
            game.Start();
            Assert.AreNotEqual(GameStatus.Playing, game.GameStatus, "Game Status darf nicht 'started' sein.");
        }

        [TestMethod]
        public void Game_Start()
        {
            Game game = Game.Instance;
            game.LevelXmlPath = "/data/levels/test/level.xml";
            game.GameStatus = GameStatus.LoadingComplete;
            game.Start();
            Assert.AreEqual(GameStatus.Playing, game.GameStatus, "Game Status muss 'started' sein.");
        }

        [TestMethod]
        public void Game_ResetGame()
        {
            Game game = Game.Instance;
            game.LevelXmlPath = "/data/levels/test/level.xml";
            game.Load();
            game.ResetGame();
            Assert.AreEqual((System.UInt32)0, game.level.AllAvailableSegments[0].objects[0].Model.Id, "Objekte wurden nicht entfernt.");
            Assert.AreEqual(GameStatus.Start, game.GameStatus, "Game Status muss 'start' sein.");
        }

        [TestMethod]
        public void Game_CheckGameOverTrue()
        {
            Game game = Game.Instance;
            game.LevelXmlPath = "/data/levels/test/level.xml";
            game.Load();
            game.Player.Lifes = 0;
            game.Start();
            Data.SetBody();
            game.Update();
            Assert.AreEqual(GameStatus.GameOver, game.GameStatus, "Game Status muss 'gameOver' sein.");
        }

        [TestMethod]
        public void Game_CheckGameOverFalse()
        {
            Game game = Game.Instance;
            game.LevelXmlPath = "/data/levels/test/level.xml";
            game.Load();
            game.Player.Lifes = 5;
            game.Start();
            Data.SetBody();
            game.Update();
            Assert.AreNotEqual(GameStatus.GameOver, game.GameStatus, "Game Status darf nicht 'gameOver' sein.");
        }

        [TestMethod]
        public void Game_CheckLevelEndTrue()
        {
            Game game = Game.Instance;
            game.LevelXmlPath = "/data/levels/test/level.xml";
            game.Load();
            game.Start();
            Data.SetBody();
            View.Camera.PositionCamera(0, 1.5f, -50);
            game.Update();
            Assert.AreEqual(GameStatus.Successful, game.GameStatus, "Game Status muss 'successful' sein.");
        }

        [TestMethod]
        public void Game_CheckLevelEndFalse()
        {
            Game game = Game.Instance;
            game.LevelXmlPath = "/data/levels/test/level.xml";
            game.Load();
            game.Start();
            Data.SetBody();
            game.Update();
            Assert.AreNotEqual(GameStatus.Successful, game.GameStatus, "Game Status darf nicht 'successful' sein.");
        }*/
    }
}
