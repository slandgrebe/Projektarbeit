using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using View;
using MotionDetection;
using System;
using JumpAndRun.GameLogic;
using JumpAndRun.Gesture;
using JumpAndRun.Gui;
using log4net;
using log4net.Config;

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

        /*/// <summary>Klickgeste überprüfen</summary>
        private Click click = new Click();*/
        /// <summary>Aktueller Modus des Spieles</summary>
        private Modus modus;

        private Difficulty difficulty = Difficulty.NotSelected;

        private bool ButtonTutorialCompleted = false;
        private bool ButtonLoadingClicked = false;

        private Sound.Sound backgroundSound = null;
        /// <summary>Logger</summary>
        private static readonly ILog log = LogManager.GetLogger(typeof(Run).Name);

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

        private bool Initialize()
        {
            log.Info("Run Initialize");

            // Fenster öffnen
            bool fullscreen = true;
            uint windowWidth = 0;
            uint windowHeight = 0;

            // immer selbe auflösung
            windowWidth = 1024;
            windowHeight = 768;

            // fenstermodus im debugmodus
            if (Program.state == Program.State.Debug)
            {
                fullscreen = false;
                windowWidth = 1024;
                windowHeight = 768;
            }
            if (!Window.Init("Dschungel Trainer", fullscreen, windowWidth, windowHeight))
            {
                System.Windows.Forms.MessageBox.Show("Leider kann das Spiel auf deinem Computer nicht gestartet werden. Wahrscheinlich ist die Grafikkarte zu alt.");
                return false;
            }

            // sound
            backgroundSound = new Sound.Sound(GetRandomFileFromFolder("data/sound/menu/background", "*.mp3"));
            backgroundSound.Volume = 50;
            backgroundSound.SoundFinished += new Sound.Sound.SoundFinishedEventHandler(SoundFinished);

            // GUI für Initialisierung anzeigen
            modus = Modus.KinectMissing;
            KinectUi.Instance.SetText("Kinect wird gestartet");
            KinectUi.Instance.Show();

            // Spielerskalierung
            Player.Instance.Scale = 1;

            // click events
            ButtonTutorialUi.Instance.ButtonClickedEvent += new ButtonTutorialUi.ButtonClick(TutorialButtonClicked);
            MenuUi.Instance.DifficultySelectedEvent += new MenuUi.DifficultySelected(DifficultySelected);
            LoadingUi.Instance.ButtonClickedEvent += new LoadingUi.ButtonClick(LoadingButtonClicked);
            ScoreUi.Instance.ButtonClickedEvent += new ScoreUi.ButtonClick(ScoreButtonClicked);
            GameOverUi.Instance.ButtonClickedEvent += new GameOverUi.ButtonClick(GameOverButtonClicked);

            // Zufallszahlen initialisieren
            RandomNumberGenerator.SetSeedFromSystemTime();

            return true;
        }

        /// <summary>
        /// Hauptschleife
        /// </summary>
        private Run()
        {
            if (!Initialize())
            {
                return;
            }
            System.Threading.Thread.Sleep(30);

            // Hauptschleife
            while (Window.IsRunning())
            {
                // senkt die CPU Auslastung drastisch
                System.Threading.Thread.Sleep(1);

                // Kinect überprüfen
                if (!CheckKinect()) continue;
                // Überprüfen ob eine Person erkannt wird
                else if (!CheckPersonTracking()) continue;
                // Programm mit Geste beenden
                else if (GestureClose.IsTrue()) break;
                // Button Tutorial
                else if (!CheckButtonTutorial()) continue;
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
                    log.Warn("ungueltiger Zustand.");
                }
            }

            // Fenster schliessen
            Window.Close();
        }



        // Alle GUIs ausblenden
        private void HideAllGuis()
        {
            KinectUi.Instance.Hide();
            NoTrackingUi.Instance.Hide();
            ButtonTutorialUi.Instance.Hide();
            MenuUi.Instance.Hide();
            LoadingUi.Instance.Hide();
            GameUi.Instance.Hide();
            GameOverUi.Instance.Hide();
            ScoreUi.Instance.Hide();
        }

        // prüfen ob Kinect angeschlossen ist
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
            }
            else
            {
                return true;
            }

            return false;
        }

        // prüfen ob eine Person von der Kinect erkannt wird
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

                // Alles zurücksetzen, da keine Person mehr erkannt wird
                ResetEverything();

                // Tutorial wieder anzeigen, da es möglicherweise eine neue Person ist
                ButtonTutorialCompleted = false;

                return false;
            }

            // Kinect ist angeschlossen und Person erkannt: Cursors updaten
            JumpAndRun.Gui.Elements.Cursor.Instance.UpdateCursor();

            return true;
        }

        // Überprüft ob das Button Tutorial abgeschlossen wurde
        private bool CheckButtonTutorial()
        {
            if (ButtonTutorialCompleted == false)
            {
                if (modus != Modus.ButtonTutorial)
                {
                    modus = Modus.ButtonTutorial;
                    Body.Instance.Scale(1f); // warum ist das nötig?
                    HideAllGuis();
                    ButtonTutorialUi.Instance.Show();
                    backgroundSound.Play();
                }

                return false;
            }

            return true;
        }

        // überprüfen ob eine Schwierigkeit ausgewählt wurde
        private bool CheckDifficultySelection()
        {
            // Schwierigkeitsgrad wählen
            if (difficulty == Difficulty.NotSelected)
            {
                if (modus != Modus.Menu) // MenuUi zeigen, falls zuvor in einem anderen Modus gewesen
                {
                    modus = Modus.Menu;
                    Body.Instance.Scale(1f); // warum ist das nötig?
                    HideAllGuis();
                    MenuUi.Instance.Show();
                    backgroundSound.Play();
                }

                return false;
            }

            return true;
        }

        // Überprüfen ob das Spiel geladen ist
        private bool CheckGameLoading()
        {
            // Button auf Lade Bildschirm noch nicht geklickt
            if (!ButtonLoadingClicked)
            {
                // Spiel muss geladen werden
                if (Game.Instance.GameStatus == GameStatus.Start) // GameStatus.Initial???
                {
                    if (modus != Modus.Loading)
                    {
                        modus = Modus.Loading;
                        HideAllGuis();
                        LoadingUi.Instance.Show();
                        backgroundSound.Play();

                        // Spiel laden
                        string path = "data/levels/jungle/level.xml";
                        Game.Instance.Load(path, difficulty);
                    }

                    return false;
                }
                // Level ist geladen
                else if (Game.Instance.GameStatus == GameStatus.LoadingComplete)
                {
                    if (modus != Modus.LoadingComplete)
                    {
                        modus = Modus.LoadingComplete;
                        // Button anzeigen
                        LoadingUi.Instance.LoadingComplete();
                    }

                    return false;
                }
            }

            return true;
        }

        // Überprüfen ob das Spiel am laufen ist
        private bool CheckGaming()
        {
            // Darstellung des Spiels updaten
            Game.Instance.Update();

            // Level ist geladen
            if (Game.Instance.GameStatus == GameStatus.LoadingComplete)
            {
                if (modus != Modus.Play)
                {
                    modus = Modus.Play;
                    HideAllGuis();
                    backgroundSound.Stop();
                    Game.Instance.Start();
                }

                return false;
            }
            // am spielen
            else if (Game.Instance.GameStatus == GameStatus.Playing /*|| Game.Instance.GameStatus == GameStatus.Start*/)
            {
                if (modus != Modus.Play)
                {
                    log.Debug("Seltsamer Zustand: Run > CheckGaming()");
                    modus = Modus.Play;
                    HideAllGuis();
                    backgroundSound.Stop();
                    Game.Instance.Start();
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

        // Überprüfen ob der Spieler noch den Endbildschirm bestaunt
        private bool CheckFinishedGame()
        {
            // Spiel erfolgreich beendet
            if (Game.Instance.GameStatus == GameStatus.Successful)
            {
                if (modus != Modus.Score)
                {
                    modus = Modus.Score;
                    Body.Instance.Scale(1f);
                    ScoreUi.Instance.Gains = Game.Instance.Player.Gains;
                    ScoreUi.Instance.Penalties = Game.Instance.Player.Penalties;
                    HideAllGuis();
                    ScoreUi.Instance.Show();
                    backgroundSound.Play();
                }

                return false;
            }
            // Spiel nicht so erfolgreich beendet
            else if (Game.Instance.GameStatus == GameStatus.GameOver)
            {
                if (modus != Modus.GameOver)
                {
                    modus = Modus.GameOver;
                    Body.Instance.Scale(1f);
                    HideAllGuis();
                    GameOverUi.Instance.Show();
                    backgroundSound.Play();
                }

                return false;
            }

            return true;
        }

        /// <summary>
        /// Event Listener wenn ein Sound zu ende ist
        /// </summary>
        /// <param name="sound">Der beendete Sound</param>
        public void SoundFinished(Sound.Sound sound)
        {
            if (sound.Equals(backgroundSound))
            {
                backgroundSound.FilePath = GetRandomFileFromFolder("data/sound/menu/background", "*.mp3");
                backgroundSound.Play();

                log.Debug("Hintergrundsound durchgelaufen. Naechster Sound wird gestartet.");
            }
        }

        public void TutorialButtonClicked()
        {
            ButtonTutorialCompleted = true;
        }

        /// <summary>
        /// Event Listener wenn die Schwierigkeit ausgewählt wurde
        /// </summary>
        /// <param name="difficulty">ausgewählter Schwierigkeitsgrad</param>
        public void DifficultySelected(Difficulty difficulty)
        {
            this.difficulty = difficulty;
            log.Debug("Schwierigkeitsgrad ausgewaehlt: " + difficulty.ToString());
        }

        /// <summary>
        /// Button auf dem Ladebildschirm wurde geklickt
        /// </summary>
        public void LoadingButtonClicked()
        {
            ButtonLoadingClicked = true;
        }

        /// <summary>
        /// Event Listener wenn auf dem Game Over Bildschirm der Button geklickt wird
        /// </summary>
        public void GameOverButtonClicked()
        {
            ResetEverything();
        }

        /// <summary>
        /// Event Listener wenn auf dem Siegbildschirm der Button geklickt wird
        /// </summary>

        public void ScoreButtonClicked()
        {
            ResetEverything();
        }

        // Alles auf den Anfangszustand zurücksetzen
        private void ResetEverything()
        {
            modus = Modus.NotTracked;
            difficulty = Difficulty.NotSelected;
            ButtonLoadingClicked = false;
            Game.Instance.ResetGame();
        }

        // Zufällige Datei aus Ordner zurückliefern
        private string GetRandomFileFromFolder(string folder, string pattern)
        {
            int seed = (int)DateTime.Now.Ticks;
            var rand = new Random(seed);
            var files = System.IO.Directory.GetFiles(folder, pattern);
            return files[rand.Next(files.Length)];
        }

        /// <summary>
        /// Ausgabe und Sensor sauber Beenden
        /// </summary>
        private void End()
        {
            sensor.Stop();
        }

        /*public GestureClose GestureClose
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }*/
    }
}
