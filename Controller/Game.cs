using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Xml.Serialization;
using View;

namespace Controller
{
    class Game
    {
        private static Game instance;
        public Player Player { get; set; }
        public Level level = null;
        public GameStatus GameStatus { get; set; }
        private GameUi gameUi;

        public Game()
        {
            gameUi = new GameUi();
            Init();
        }

        public void Init()
        {
            GameStatus = GameStatus.Initial;

            string dir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            dir = dir + @"\Resource Files\Levels\Jungle\Level.xml";

            FileStream stream;
            stream = new FileStream(dir, FileMode.Open);
            XmlSerializer serializer = new XmlSerializer(typeof(Level));
            level = (Level)serializer.Deserialize(stream);
            level.Deserialize();
            level.Load();

            Player = new Player();
            Player.Scale = 0.7f;
            Player.Attach = true;
            Player.Score = 0;
            Player.Lives =level.Lives;
            
            gameUi.Show();
            GameStatus = GameStatus.Loadet;
        }

        public void Start()
        {
            if (GameStatus == GameStatus.Loadet)
            {
                View.Visualization.PositionCamera(0, 1.5f, 0);
                View.Visualization.ChangeCameraSpeed(5f);
                GameStatus = GameStatus.Started;
            }
        }

        /// <summary>
        /// stellt sicher, dass diese Klasse nur einmal Instanziert wird.
        /// </summary>
        /// <returns>instance der Klasse Position</returns>
        public static Game Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Game();
                }
                return instance;
            }
        }

        public void Update()
        {
            if(GameStatus != GameStatus.Started){
                View.Visualization.ChangeCameraSpeed(0);
            }
            if (GameStatus == GameStatus.Started)
            {
                if (level != null)
                {
                    Player.Update();
                    gameUi.Lives = Player.Lives;
                    gameUi.Score = Player.Score;
                    gameUi.Update();
                    foreach (LevelSegment segment in level.segments)
                    {
                        foreach (Object score in segment.scores)
                        {
                            if (score.handleCollisions(Player, true))
                            {
                                Player.Score++;
                            }
                        }
                    }

                    foreach (LevelSegment segment in level.segments)
                    {
                        foreach (Object obstacle in segment.obstacles)
                        {
                            if (obstacle.handleCollisions(Player, false))
                            {
                                Player.Lives--;
                            }
                        }
                    }
                    gameUi.Update();
                    ChechLevelEnd();
                    CheckGameOver();
                }
            } 
        }

        public void DisposeLevel()
        {
            gameUi.Hide();
            level.Dispose();
            GameStatus = GameStatus.Start;
        }

        private void CheckGameOver()
        {
            if (Player.Lives == 0)
            {
                GameStatus = GameStatus.GameOver;
            }
        }

        private void ChechLevelEnd()
        {
            if (level.LevelLength + level.segments.Last().Length - 2 > Player.GetPosition())
            {
                GameStatus = GameStatus.Successful;
            }
        }
    }
}
