﻿using System.Collections.Generic;
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
<<<<<<< HEAD
            // Fenster öffnen
            Window.Init("Fit with Jump and Run", false, 1280, 800);

            // GUI für Initialisierung anzeigen
            modus = Modus.KinectMissing;
            KinectUi.Instance.SetText("Kinect wird gestartet");
            KinectUi.Instance.Show();

            // click events
            MenuUi.Instance.DifficultySelectedEvent += new MenuUi.DifficultySelected(DifficultySelected);
            ScoreUi.Instance.ButtonClickedEvent += new ScoreUi.ButtonClick(ScoreButtonClicked);
            GameOverUi.Instance.ButtonClickedEvent += new GameOverUi.ButtonClick(GameOverButtonClicked);
=======
            modus = Modus.NotTracked;
            
            // Fenster im Fullscreen öffnen
            Window.Init("Fit with Jump and Run",false,1600,900);

            // Gui Element für Keine Person erkannt initialisieren
            noTrackingUi = new NoTrackingUi();
            noTrackingUi.Position = 100;
            noTrackingUi.Show(); 

            // Gui Element für das Hauptmenu initialisieren
            menuUi = new MenuUi();
            menuUi.Position = 200;

            // Gui Element für den Ladebildschirm des Levels initialisieren
            loadingUi = new LoadingUi();
            loadingUi.Position = 300;

            // Gui Element für das erfolgreiche Beenden eines Levels initialisieren
            scoreUi = new ScoreUi();
            scoreUi.Position = 400;

            // Gui Element für das nicht erfolgreiche Beenden eines Levels initialisieren
            gameOverUi = new GameOverUi();
            gameOverUi.Position = 500;
>>>>>>> master
        }

        /// <summary>
        /// Abhandeln eines Frames.
        /// </summary>
        public Run()
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
                    modus = Modus.NotTracked;
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
                }

                //Console.WriteLine("jetzt passiert's");
                //Game test = new Game();
                Game.Instance.ResetGame();

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
                }

                UpdateCursor();

                return false;
            }

            return true;
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
            Console.WriteLine("difficulty selected: " + difficulty);
            this.difficulty = difficulty;
            modus = Modus.Play;
        }
        public void GameOverButtonClicked()
        {
            GameFinished();
        }
        public void ScoreButtonClicked()
        {
            GameFinished();
        }

        private void GameFinished()
        {
            Console.WriteLine("game finished");
            modus = Modus.KinectMissing;
            difficulty = Difficulty.NotSelected;
            Game.Instance.ResetGame();
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
