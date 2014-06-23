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
    /// <summary>
    /// Hauptklasse des Programmes. Abhandeln Ausgabenelemente und sicherstellen der Anzeige zum richtigen Zeitpunkt.
    /// </summary>
    class Run
    {
        /// <summary>Instanz des Personenerkennungssensor</summary>
        private SkeletonTracker sensor = null;
        /// <summary>Eigene Instanz</summary>
        private static Run instance;
        /// <summary>Instanz des eigentlichen Spieles</summary>
        private Game game = null;
        /// <summary>Gui Element, wenn keine Person erkannt wird</summary>
        private NoTrackingUi noTrackingUi = null;
        /// <summary>Gui Element für das Hauptmenu</summary>
        private MenuUi menuUi = null;
        /// <summary>Gui Element während des Levelladens</summary>
        private LoadingUi loadingUi = null;
        /// <summary>Gui Element für das erfolgreiche Beenden eines Levels</summary>
        private ScoreUi scoreUi = null;
        /// <summary>Gui Element wenn das Level wenn man das Level nicht beenden kann</summary>
        private GameOverUi gameOverUi = null;
        /// <summary>Klickgeste überprüfen</summary>
        private Click click = new Click();
        /// <summary>Aktueller Modus des Spieles</summary>
        private Modus modus;

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
            modus = Modus.NotTracked;
            
            // Fenster im Fullscreen öffnen
            Window.Init("Fit with Jump and Run",true,0,0);

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

                // Sensor nicht angeschlossen / nicht gestartet
                if (sensor == null)
                {
                    // Personenerkennung starten
                    try
                    {
                        // Programm Starten
                        sensor = new SkeletonTracker();
                        sensor.Start();
                    }
                    catch (Exception e)
                    {
                        modus = Modus.NotTracked;
                        sensor = null;
                    }
                }
                else
                {
                    // Überprüfen ob eine Person erkannt wird
                    if (Body.Instance.IsTracked)
                    {
                        if (modus == Modus.NotTracked)
                        {
                            modus = Modus.Menu;
                        }
                    }
                    else
                    {
                        modus = Modus.NotTracked;
                        //modus = Modus.Menu;
                    }

                    // Programm mit Geste beenden
                    if (GestureClose.IsTrue())
                    {
                        Window.Close();
                        break;
                    }

                    // Weiche was aktuell am Screen angezeigt werden soll
                    switch (modus)
                    {
                        case Modus.NotTracked:
                            noTrackingUi.Show();
                            if (game != null)
                            {
                                game.ResetGame();
                            }
                            menuUi.Hide();
                            loadingUi.Hide();
                            scoreUi.Hide();
                            gameOverUi.Hide();
                            break;

                        case Modus.Menu:
                            Body.Instance.Scale(0.1f);
                            noTrackingUi.Hide();
                            menuUi.Show();
                            menuUi.PositionCursor(Body.Instance.HandRight.X, Body.Instance.HandRight.Y, Body.Instance.Head.X, Body.Instance.Head.Y, Body.Instance.ShoulderRight.X, Body.Instance.ShoulderRight.Y);
                            // Klickgeste auf Button
                            if (menuUi.CursorPosition(Body.Instance.HandRight.X, Body.Instance.HandRight.Y))
                            {
                                if (click.IsClicked())
                                {
                                    modus = Modus.Play;
                                }
                            }
                            break;

                        case Modus.Play:
                            menuUi.Hide();
                            scoreUi.Hide();
                            gameOverUi.Hide();
                            if (game != null)
                            {
                                // Spiel/Level wird geladen
                                if (game.GameStatus == GameStatus.Start)
                                {
                                    loadingUi.Show();
                                    game.LevelXmlPath = "/data/levels/jungle/level.xml";
                                    game.Init();
                                }
                                // Level ist geladen
                                if (game.GameStatus == GameStatus.Loadet)
                                {
                                    game.Start();
                                    loadingUi.Hide();
                                }
                                // Darstellung des Spiels updaten
                                game.Update();
                                // Spiel erfolgreich beendet
                                if (game.GameStatus == GameStatus.Successful)
                                {
                                    game.ResetGame();
                                    modus = Modus.Score;
                                }
                                // Spiel nicht erfolgreich beendet
                                if (game.GameStatus == GameStatus.GameOver)
                                {
                                    game.ResetGame();
                                    modus = Modus.GameOver;
                                }
                            }
                            else
                            {
                                // Spiel beim ersten durchgang initialisieren
                                loadingUi.Show();
                                game = Game.Instance;
                            }
                            break;

                        case Modus.Score:
                            Body.Instance.Scale(0.1f);
                            scoreUi.Show(game.Player.Score);
                            scoreUi.PositionCursor(Body.Instance.HandRight.X, Body.Instance.HandRight.Y);
                            // Klickgeste auf Button
                            if (scoreUi.HoverButton(Body.Instance.HandRight.X, Body.Instance.HandRight.Y))
                            {
                                if (click.IsClicked())
                                {
                                    modus = Modus.Play;
                                    game.GameStatus = GameStatus.Start;
                                }
                            }
                            break;

                        case Modus.GameOver:
                            Body.Instance.Scale(0.1f);
                            gameOverUi.Show();
                            gameOverUi.PositionCursor(Body.Instance.HandRight.X, Body.Instance.HandRight.Y);
                            // Klickgeste auf Button
                            if (gameOverUi.HoverButton(Body.Instance.HandRight.X, Body.Instance.HandRight.Y))
                            {
                                if (click.IsClicked())
                                {
                                    modus = Modus.Play;
                                    game.GameStatus = GameStatus.Start;
                                }
                            }
                            break;

                        default:
                            break;
                    }
                }
            }
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
