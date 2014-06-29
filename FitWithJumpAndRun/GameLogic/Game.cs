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
using log4net;
using log4net.Config;

namespace JumpAndRun.GameLogic
{
    /// <summary>
    /// Beinhaltet das Spiel
    /// </summary>
    public class Game
    {
        /// <summary>Instanz des Gameobjektes.</summary>
        private static Game instance;
        /// <summary>Beinhaltet die Spielfigur.</summary>
        public Player Player { get; set; }
        /// <summary>Beinhaltet das level.</summary>
        public Level level = null;
        /// <summary>Beinhaltet den aktuellen Spielstatus.</summary>
        public GameStatus GameStatus { get; set; }
        /// <summary>Beinhaltet das GUI während des Spiels.</summary>
        private GameUi gameUi;
        /*/// <summary>Beinhaltet die Segmentnummer, in welchem sich der Spieler gerade befindet</summary>
        private int CurrentSegment;*/
        /// <summary>Kamerageschwindigkeit  in m/s</summary>
        private double speed = 5;
        /// <summary>Startzeit bei Levelbeginn</summary>
        private DateTime startTime = DateTime.Now;
        /// <summary>Logger</summary>
        private static readonly ILog log = LogManager.GetLogger(typeof(Game).Name);

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
        /// Initialisiert das Spiel
        /// </summary>
        private Game()
        {
            gameUi = GameUi.Instance;
            Player = Player.Instance;
            GameStatus = GameStatus.Start;
        }

        /// <summary>
        /// Ladet das Level, erstellt die Spielfigur
        /// </summary>
        /// <param name="levelXmlPath">XML Pfad des gewünschten Levels</param>
        /// <param name="difficulty">Schwierigkeitsgrad</param>
        /// <returns>Prüfung ob das Level geladen werden konnte</returns>
        public bool Load(string levelXmlPath = "data/levels/jungle/level.xml", JumpAndRun.Difficulty difficulty = JumpAndRun.Difficulty.Normal)
        {
            /*if (String.IsNullOrEmpty(LevelXmlPath))
            {
                return false;
            }*/

            GameStatus = GameStatus.Loading;

            //string dir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            //dir = dir + LevelXmlPath;

            // Level Objekt deserialisieren und laden
            FileStream stream;
            //stream = new FileStream(dir, FileMode.Open);
            stream = new FileStream(levelXmlPath, FileMode.Open);
            XmlSerializer serializer = new XmlSerializer(typeof(Level));
            level = (Level)serializer.Deserialize(stream);
            stream.Close();

            level.Deserialize();
            if (!level.Load(difficulty))
            {
                return false;
            }
            level.Visibility(false);

            // Spieler
            Player.Scale = 0.7f;
            Player.Attach = true;
            Player.Gains = 0;
            Player.Penalties = 0;

            Sound.Sound sound = new Sound.Sound("data/sound/menu/Bing.mp3");
            sound.Play();

            // Levelende abfangen
            level.LevelFinishedEvent += new Level.LevelFinished(LevelHasFinished);

            GameStatus = GameStatus.LoadingComplete;

            return true;
        }

        /// <summary>
        /// Startet das Spiel, sofern das Level fertig geladen ist.
        /// </summary>
        public void Start()
        {
            if (GameStatus == GameStatus.LoadingComplete)
            {
                //Camera.PositionCamera(0, 1.5f, -40 + 4.5f -0.5f);
                //Camera.ChangeCameraSpeed(0f);
                //CurrentSegment = -1;
                level.Visibility(true);
                Player.Visibility(true);
                Camera.PositionCamera(0, 1.5f, 0);
                Camera.ChangeCameraSpeed((float)speed);
                level.playBackgroundMusic();
                GameStatus = GameStatus.Playing;

                GameUi.Instance.Show();

                startTime = DateTime.Now;
            }
        }


        /// <summary>
        /// Spiellogik während eines durchlaufs.
        /// </summary>
        public void Update()
        {
            // Wenn das Level beendet ist, die Kamera stoppen
            /*if(GameStatus != GameStatus.Playing){
                Camera.ChangeCameraSpeed(0);
            }

            // Prüfen ob der Spieler Game Over ist
            CheckGameOver();*/

            if (GameStatus == GameStatus.Playing)
            {
                if (level != null)
                {
                    // Spielfigur bewegen
                    Player.Update();
                    
                    // Score um 1 erhöhen, wenn ein Punkt gesammelt wird
                    foreach (LevelSegment segment in level.RandomlyChosenSegments)
                    {
                        foreach (JumpAndRun.Item.Object score in segment.scores)
                        {
                            if (score.Collision(Player, true))
                            {
                                Player.Gains += (uint)score.Severity;
                            }
                        }
                    }

                    // Leben um 1 verringern, wenn ein Hindernis getroffen wird
                    foreach (LevelSegment segment in level.RandomlyChosenSegments)
                    {
                        foreach (JumpAndRun.Item.Object obstacle in segment.obstacles)
                        {
                            if (obstacle.Collision(Player, false))
                            {
                                Player.Penalties += (uint)obstacle.Severity;
                            }
                        }
                    }

                    // Position herausfinden
                    double timePlaying = DateTime.Now.Subtract(startTime).TotalSeconds;
                    double distance = speed * timePlaying;

                    Player.ZPosition = (float)distance;

                    // Neuer Score, Lebensvorrat im GUI anzeigen
                    gameUi.Penalty = Player.Lifes;
                    gameUi.Advantage = Player.Gains;
                    gameUi.Update();
                }
            }
            else if (GameStatus == GameStatus.GameOver || GameStatus == GameStatus.Successful)
            {
                level.Visibility(false);
                Player.Visibility(false);
                Camera.ChangeCameraSpeed(0);
                level.stopBackgroundMusic();
            }
        }

        /// <summary>
        /// verbirgt das GUI und löscht das Level
        /// </summary>
        public void ResetGame()
        {
            GameUi.Instance.Hide();
            if (GameStatus == GameStatus.LoadingComplete
                || GameStatus == GameStatus.Playing
                || GameStatus == GameStatus.GameOver
                || GameStatus == GameStatus.Successful)
            {
                level.Dispose();
            }
            GameStatus = GameStatus.Start;
        }

        /// <summary>
        /// Fängt das Level Finished Event ab
        /// </summary>
        public void LevelHasFinished()
        {
            GameStatus = GameStatus.Successful;
        }

        /*/// <summary>
        /// Überprüft ob der Spieler Game Over ist
        /// </summary>
        private void CheckGameOver()
        {
            if (Player.Lifes == 0)
            {
                log.Info("GAME OVER!");
                GameStatus = GameStatus.GameOver;
                Camera.ChangeCameraSpeed(0);
                
            }
        }

        /// <summary>
        /// Überprüft ob der Spieler im Ziel ist.
        /// </summary>
        private void CheckLevelEnd()
        {
            if (level.Length - level.RandomlyChosenSegments.Last().Length + 2 < Player.GetPosition() * -1)
            {
                log.Info("Spiel beendet");
                GameStatus = GameStatus.Successful;
                level.Visibility(false);
                Player.Visibility(false);
            }
        }*/
    }
}
