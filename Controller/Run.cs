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
        private Click click = new Click();
        private Modus modus;
        private uint trackId = 0;
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
            trackId = Visualization.addText("data/fonts/arial.ttf");
            while (!Visualization.isCreated(trackId)) { }
            Visualization.position(trackId, -0.7f, -0.7f, 1.0f);
            Visualization.textSize(trackId, 36);
            Visualization.textColor(trackId, 1f, 0f, 0f, 1f);
            Visualization.text(trackId, "No");
            /*noTrackingUi = new NoTrackingUi();
            noTrackingUi.Position = 1000;
            noTrackingUi.Show();
            */

            modus = Modus.Menu;

            menuUi = new MenuUi();
            menuUi.Position = 0;
            menuUi.Show();
            

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
            switch (modus)
            {
                case Modus.Menu:
                    Body.Instance.Scale(0.1f);
                    menuUi.Show();
                    menuUi.PositionCursor(Body.Instance.HandRight.X, Body.Instance.HandRight.Y);
                    if(menuUi.HoverButton(Body.Instance.HandRight.X, Body.Instance.HandRight.Y))
                    {
                        if(click.IsClicked()){
                            modus = Modus.Play;
                        }
                    }
                    break;

                case Modus.Play:
                    menuUi.Hide();
                    if (game != null)
                    {
                        game.Update();
                    }
                    else
                    {
                        game = Game.Instance;
                    }
                    break;

                default:
                    break;
            }

            //if (Body.Instance.IsTracked)
            if (Body.Instance.Spine.X + Body.Instance.Spine.Y + Body.Instance.Spine.Z != 0 )
            {
                Visualization.text(trackId, "Yes");
            }
            else
            {
                Visualization.text(trackId, "No");
            }
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
