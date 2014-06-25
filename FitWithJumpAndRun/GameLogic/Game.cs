using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Xml.Serialization;
using View;
using JumpAndRun.Gui;

namespace JumpAndRun.GameLogic
{
    /// <summary>
    /// Beinhaltet das Spiel
    /// </summary>
    public class Game
    {
        /// <summary>Instanz des Positionobjektes.</summary>
        private static Game instance;
        /// <summary>Beinhaltet die Spielfigur.</summary>
        public Player Player { get; set; }
        /// <summary>Beinhaltet das level.</summary>
        public Level level = null;
        /// <summary>Beinhaltet den aktuellen Spielstatus.</summary>
        public GameStatus GameStatus { get; set; }
        /// <summary>XML Pfad zum level</summary>
        public string LevelXmlPath { get; set; }
        /// <summary>Beinhaltet das GUI während des Spiels.</summary>
        private GameUi gameUi;

        /// <summary>
        /// Initialisiert das Spiel
        /// </summary>
        public Game()
        {
            gameUi = GameUi.Instance;
            Player = new Player();
            GameStatus = GameStatus.Start;
        }

        /// <summary>
        /// Ladet das Level, erstellt die Spielfigur
        /// </summary>
        public bool Init()
        {
            if (String.IsNullOrEmpty(LevelXmlPath))
            {
                return false;
            }

            GameStatus = GameStatus.Initial;

            string dir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            dir = dir + LevelXmlPath;

            FileStream stream;
            stream = new FileStream(dir, FileMode.Open);
            XmlSerializer serializer = new XmlSerializer(typeof(Level));
            level = (Level)serializer.Deserialize(stream);
            stream.Close();
            level.Deserialize();
            level.Load();

            Player.Scale = 0.7f;
            Player.Attach = true;
            Player.Score = 0;
            Player.Lifes = level.Lifes;
            
            GameStatus = GameStatus.Loadet;

            return true;
        }

        /// <summary>
        /// Startet das Spiel, sofern das Level fertig geladen ist.
        /// </summary>
        public void Start()
        {
            if (GameStatus == GameStatus.Loadet)
            {
                //Camera.PositionCamera(0, 1.5f, -40 + 4.5f -0.5f);
                //Camera.ChangeCameraSpeed(0f);
                level.Visibility(true);
                Player.Visibility(true);
                Camera.PositionCamera(0, 1.5f, 0);
                Camera.ChangeCameraSpeed(5f);
                level.playBackgroundMusic();
                GameStatus = GameStatus.Started;

                GameUi.Instance.Show();
            }
        }

        /// <summary>
        /// Stellt sicher, dass diese Klasse nur einmal Instanziert wird.
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

        /// <summary>
        /// Spiellogik während eines durchlaufs.
        /// </summary>
        public void Update()
        {
            // Wenn das Lvel beendet ist, die Kamera stoppen
            if(GameStatus != GameStatus.Started){
                Camera.ChangeCameraSpeed(0);
            }
            if (GameStatus == GameStatus.Started)
            {
                if (level != null)
                {
                    // Spielfigur bewegen
                    Player.Update();
                    
                    // Score um 1 erhöhen, wenn ein Punkt gesammelt wird
                    foreach (LevelSegment segment in level.Segments)
                    {
                        foreach (JumpAndRun.Item.Object score in segment.scores)
                        {
                            if (score.Collision(Player, true))
                            {
                                Player.Score++;
                            }
                        }
                    }

                    // Leben um 1 verringern, wenn ein Hinternis getroffen wird
                    foreach (LevelSegment segment in level.Segments)
                    {
                        foreach (JumpAndRun.Item.Object obstacle in segment.obstacles)
                        {
                            if (obstacle.Collision(Player, false))
                            {
                                Player.Lifes--;
                            }
                        }
                    }

                    // Neuer Score, Lebensvorrat im GUI anzeigen
                    //////gameUi.Lives = Player.Lives;
                    gameUi.Score = Player.Score;
                    gameUi.Update();

                    // Prüfen ob das Ziel erreicht wurde
                    CheckLevelEnd();

                    // Prüfen ob der Spieler Game Over ist
                    CheckGameOver();

                    if (GameStatus == GameStatus.GameOver || GameStatus == GameStatus.Successful)
                    {
                        level.stopBackgroundMusic();
                    }
                }
            } 
        }

        /// <summary>
        ///// verbirgt das GUI und löscht das Level
        ///// </summary>
        public void ResetGame()
        {
            GameUi.Instance.Hide();
            if (GameStatus == GameStatus.Loadet
                || GameStatus == GameStatus.Started
                || GameStatus == GameStatus.GameOver
                || GameStatus == GameStatus.Successful)
            {
            level.Dispose();
            }
            GameStatus = GameStatus.Start;
        }

        /// <summary>
        /// Überprüft ob der Spieler Game Over ist
        /// </summary>
        private void CheckGameOver()
        {
            if (Player.Lifes == 0)
            {
                GameStatus = GameStatus.GameOver;
                Camera.ChangeCameraSpeed(0);
                level.Visibility(false);
                Player.Visibility(false);
            }
        }

        /// <summary>
        /// Überprüft ob der Spieler im Ziel ist.
        /// </summary>
        private void CheckLevelEnd()
        {
            if (level.Length - level.Segments.Last().Length + 2 < Player.GetPosition() * -1)
            {
                GameStatus = GameStatus.Successful;
                level.Visibility(false);
                Player.Visibility(false);
            }
        }
    }
}
