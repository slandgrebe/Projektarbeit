using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using View;
using MotionDetection;
using System;
using JumpAndRun.GameLogic;
using JumpAndRun.Gesture;
using JumpAndRun.Gui;

namespace JumpAndRun
{
    public enum Difficulty { NotSelected = 0, Easy, Normal, Difficult, Endless };

    /// <summary>
    /// Hauptklasse des Programmes. Abhandeln Ausgabenelemente und sicherstellen der Anzeige zum richtigen Zeitpunkt.
    /// </summary>
    class Run
    {
        /// <summary>Instanz des Personenerkennungssensor</summary>
        private SkeletonTracker sensor = null;
        /// <summary>Eigene Instanz</summary>
        private static Run instance;

        /// <summary>Klickgeste überprüfen</summary>
        private Click click = new Click();
        /// <summary>Aktueller Modus des Spieles</summary>
        private Modus modus;

        private Difficulty difficulty = Difficulty.NotSelected;

        private Sound.Sound backgroundSound = null;

        /// <summary>
        /// Stellt sicher, dass diese Klasse nur einmal Instanziert werden kann.
        /// </summary>
        /// <returns>Instance der Klasse Position</returns>
        public static Run Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Run();
                }
                return instance;
            }
        }

        /// <summary>
        /// GUI Elemente Initialisieren.
        /// </summary>
        private void Initialize()
        {
            // Fenster öffnen
            Window.Init("Fit with Jump and Run", false, 1280, 800);

            // sound
            backgroundSound = new Sound.Sound();
            backgroundSound.FilePath = GetRandomFileFromFolder("data/sound/menu/background", "*.mp3");
            backgroundSound.Volume = 50;
            backgroundSound.SoundStopped += new Sound.Sound.SoundStoppedEventHandler(SoundFinished);

            // GUI für Initialisierung anzeigen
            modus = Modus.KinectMissing;
            KinectUi.Instance.SetText("Kinect wird gestartet");
            KinectUi.Instance.Show();

            // click events
            MenuUi.Instance.DifficultySelectedEvent += new MenuUi.DifficultySelected(DifficultySelected);
            ScoreUi.Instance.ButtonClickedEvent += new ScoreUi.ButtonClick(ScoreButtonClicked);
            GameOverUi.Instance.ButtonClickedEvent += new GameOverUi.ButtonClick(GameOverButtonClicked);
        }

        /// <summary>
        /// Abhandeln eines Frames.
        /// </summary>
        private Run()
        {
            Initialize();
            System.Threading.Thread.Sleep(30);

            while(Window.IsRunning())
            {
                // senkt die CPU Auslastung drastisch
                System.Threading.Thread.Sleep(1);

                // Kinect überprüfen
                if (!CheckKinect()) continue;
                // Überprüfen ob eine Person erkannt wird
                else if (!CheckPersonTracking()) continue;
                // Schwierigkeitsgrad wählen
                else if (!CheckDifficultySelection()) continue;
                // spiel laden
                else if (!CheckGameLoading()) continue;
                // spielen
                else if (!CheckGaming()) continue;
                // spiel beendet
                else if (!CheckFinishedGame()) continue;
                // da stimmt etwas nicht
                else 
                {
                    Console.WriteLine("ungueltiger Zustand.");
                }

                // Programm mit Geste beenden
                if (GestureClose.IsTrue())
                {
                    Window.Close();
                    break;
                }
            }
        }


        private void HideAllGuis()
        {
            KinectUi.Instance.Hide();
            NoTrackingUi.Instance.Hide();
            MenuUi.Instance.Hide();
            LoadingUi.Instance.Hide();
            GameUi.Instance.Hide();
            GameOverUi.Instance.Hide();
            ScoreUi.Instance.Hide();
        }
        private bool CheckKinect()
        {
            if (sensor == null)
            {
                modus = Modus.KinectMissing;

                // Personenerkennung starten
                try
                {
                    // Programm Starten
                    sensor = new SkeletonTracker();
                    sensor.Start();
                    KinectUi.Instance.Hide();

                    return true;
                }
                catch (Exception e)
                {
                    KinectUi.Instance.SetText("Die Kinect ist nicht angeschlossen");
                    KinectUi.Instance.Show();
                    modus = Modus.KinectMissing;
                    sensor = null;
                }

                return false;
            }
            else
            {
                return true;
            }

            return false;
        }
        private bool CheckPersonTracking()
        {
            // Überprüfen ob eine Person erkannt wird
            if (!Body.Instance.IsTracked)
            {
                if (modus != Modus.NotTracked) // NoTrackingUi zeigen, falls zuvor in einem anderen Modus gewesen
                {
                    modus = Modus.NotTracked;
                    HideAllGuis();
                    NoTrackingUi.Instance.Show();
                    backgroundSound.Play();
                }

                //Game.Instance.ResetGame();
                ResetEverything();

                return false;
            }

            return true;
        }
        private bool CheckDifficultySelection()
        {
            // Schwierigkeitsgrad wählen
            if (difficulty == Difficulty.NotSelected) 
            {
                if (modus != Modus.Menu) // MenuUi zeigen, falls zuvor in einem anderen Modus gewesen
                {
                    modus = Modus.Menu;
                    Body.Instance.Scale(0.1f); // warum ist das nötig?
                    HideAllGuis();
                    MenuUi.Instance.Show();
                    backgroundSound.Play();
                }

                UpdateCursor();

                return false;
            }

            return true;
        }
        private bool CheckGameLoading()
        {
            if (Game.Instance.GameStatus == GameStatus.Start) // GameStatus.Initial???
            {
                if (modus != Modus.Loading)
                {
                    modus = Modus.Loading;
                    HideAllGuis();
                    LoadingUi.Instance.Show();
                    backgroundSound.Play();
                }

                Game.Instance.LevelXmlPath = "/data/levels/jungle/level.xml";
                Game.Instance.Init();

                return false;
            }

            return true;
        }

        private bool CheckGaming() 
        {
            // Darstellung des Spiels updaten
            Game.Instance.Update();

            // Level ist geladen
            if (Game.Instance.GameStatus == GameStatus.Loadet)
            {
                if (modus != Modus.Play)
                {
                    modus = Modus.Play;
                    HideAllGuis();
                    backgroundSound.Stop();
                }

                Game.Instance.Start();

                return false;
            }
            // am spielen
            else if (Game.Instance.GameStatus == GameStatus.Started || Game.Instance.GameStatus == GameStatus.Start)
            {
                if (modus != Modus.Play)
                {
                    modus = Modus.Play;
                    backgroundSound.Stop();
                }

                return false;
            }
            // Spiel erfolgreich beendet
            else if (Game.Instance.GameStatus == GameStatus.Successful)
            {                
                return true;
            }
            else if (Game.Instance.GameStatus == GameStatus.GameOver)
            {
                return true;
            }

            return false;
        }
        private bool CheckFinishedGame()
        {
            // Spiel erfolgreich beendet
            if (Game.Instance.GameStatus == GameStatus.Successful)
            {
                if (modus != Modus.Score)
                {
                    modus = Modus.Score;
                    Body.Instance.Scale(0.1f);
                    ScoreUi.Instance.Score = Game.Instance.Player.Score;
                    HideAllGuis();
                    ScoreUi.Instance.Show();
                    backgroundSound.Play();
                }

                UpdateCursor();

                return false;
            }
            else if (Game.Instance.GameStatus == GameStatus.GameOver)
            {
                if (modus != Modus.GameOver)
                {
                    modus = Modus.GameOver;
                    Body.Instance.Scale(0.1f);
                    HideAllGuis();
                    GameOverUi.Instance.Show();
                    backgroundSound.Play();
                }

                UpdateCursor();

                return false;
            }

            return true;
        }

        public void SoundFinished(Sound.Sound sound)
        {
            if (sound.Equals(backgroundSound))
            {
                backgroundSound.FilePath = GetRandomFileFromFolder("data/sound/menu/background", "*.mp3");
                backgroundSound.Play();
            }
        }

        private void UpdateCursor()
        {
            // Position des Cursors updaten und events auslösen
            View.Cursor.Instance.UpdateCursor(MotionDetection.Body.Instance.HandRight.X,
                MotionDetection.Body.Instance.HandRight.Y,
                MotionDetection.Body.Instance.HandRight.Z,
                MotionDetection.Body.Instance.Head.X,
                MotionDetection.Body.Instance.Head.Y,
                MotionDetection.Body.Instance.Head.Z,
                MotionDetection.Body.Instance.ShoulderRight.X,
                MotionDetection.Body.Instance.ShoulderRight.Y);
        }

        public void DifficultySelected(Difficulty difficulty)
        {
            this.difficulty = difficulty;
            modus = Modus.Play;
        }
        public void GameOverButtonClicked()
        {
            ResetEverything();
        }
        public void ScoreButtonClicked()
        {
            ResetEverything();
        }

        private void ResetEverything()
        {
            modus = Modus.NotTracked;
            difficulty = Difficulty.NotSelected;
            Game.Instance.ResetGame();
        }

        private string GetRandomFileFromFolder(string folder, string pattern)
        {
            int seed = (int)DateTime.Now.Ticks;
            var rand = new Random(seed);
            var files = System.IO.Directory.GetFiles("data/sound/menu/background", "*.mp3");
            return files[rand.Next(files.Length)];
        }

        /// <summary>
        /// Ausgabe und Sensor sauber Beenden
        /// </summary>
        private void End()
        {
            sensor.Stop();
        }

        public GestureClose GestureClose
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }
    }
}
