using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using View;
using Model;

namespace Controller
{
    class Run
    {
        private SkeletonTracker Sensor = null;
        
        /// <summary>Instanz des Positionobjektes</summary>
        private static Run instance;
        private int init = 0;
        private Game game = null;
        private NoTrackingUi noTrackingUi = null;
        private MenuUi menuUi = null;
        private LoadingUi loadingUi = null;
        private ScoreUi scoreUi = null;
        private GameOverUi gameOverUi = null;
        private Click click = new Click();
        private Modus modus;
        /// <summary>
        /// stellt sicher, dass diese Klasse nur einmal Instanziert wird.
        /// </summary>
        /// <returns>instance der Klasse Position</returns>
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
        /// Sensor starten
        /// </summary>
        public void Start()
        {
            modus = Modus.NotTracked;
            noTrackingUi = new NoTrackingUi();
            noTrackingUi.Position = 100;
            noTrackingUi.Show(); 

            menuUi = new MenuUi();
            menuUi.Position = 200;

            loadingUi = new LoadingUi();
            loadingUi.Position = 300;

            scoreUi = new ScoreUi();
            scoreUi.Position = 400;

            gameOverUi = new GameOverUi();
            gameOverUi.Position = 500;

            Sensor = new SkeletonTracker();
            Sensor.Start();
            Sensor.SkeletonEvent += new SkeletonTrackerEvent(GetEvent);

        }

        /// <summary>
        /// Punkte und Linien für einen Körpder erstellen
        /// </summary>
        private void Initialize()
        { 
        }

        /// <summary>
        /// Punkte und Linien neu setzen und ausgabe neu Zeichnen
        /// </summary>
        public void Update()
        {
            
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
            }

            switch (modus)
            {
                case Modus.NotTracked:
                    noTrackingUi.Show();
                    if (game != null)
                    {
                        game.DisposeLevel();
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
                    menuUi.PositionCursor(Body.Instance.HandRight.X, Body.Instance.HandRight.Y);
                    if (menuUi.HoverButton(Body.Instance.HandRight.X, Body.Instance.HandRight.Y))
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
                        if (game.GameStatus == GameStatus.Started)
                        {
                            loadingUi.Hide();
                        }
                        if (game.GameStatus == GameStatus.Start)
                        {
                            loadingUi.Show();
                            game.Init();
                        }
                        if (game.GameStatus == GameStatus.Loadet)
                        {
                            game.Start();
                        }
                        game.Update();
                        if (game.GameStatus == GameStatus.Successful)
                        {
                            game.DisposeLevel();
                            modus = Modus.Score;
                        }
                        if (game.GameStatus == GameStatus.GameOver)
                        {
                            game.DisposeLevel();
                            modus = Modus.GameOver;
                        }
                    }
                    else
                    {
                        loadingUi.Show();
                        game = Game.Instance;
                    }
                    break;

                case Modus.Score:
                    Body.Instance.Scale(0.1f);
                    scoreUi.Show(game.Player.Score);
                    scoreUi.PositionCursor(Body.Instance.HandRight.X, Body.Instance.HandRight.Y);
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

                /*while (View.Visualization.IsRunning())
                {
                System.Threading.Thread.Sleep(1); // senkt die CPU Auslastung drastisch
            }*/
        }

        /// <summary>
        /// Ausgabe und Sensor sauber Beenden
        /// </summary>
        public void End()
        {
            Sensor.Stop();
        }

        public void GetEvent()
        {
            if (init == 0)
            {
                Initialize();
                init = 1;
            }
            Update();
        }
    }
}
