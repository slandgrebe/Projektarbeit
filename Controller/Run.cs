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
        public Player Player { get; private set; }
        /// <summary>Instanz des Positionobjektes</summary>
        private static Run instance;
        private int init = 0;

        private Model Wagon = new Model("Resource Files/Models/Wagon/Wagon.3ds");
        private Model WagonCam = new Model("Resource Files/Models/Wagon/Wagon.3ds");
        private Model Rail = new Model("Resource Files/Models/Rail/Rail.3ds");

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
            Visualization.positionCamera(0, 0, 0);
            Wagon.Position(0, -1f, -3.5f);
            Wagon.Scale(0.5f);
            Visualization.attachToCamera(WagonCam.Id, true);
            WagonCam.Position(0, 0.5f, -1f);
            WagonCam.Scale(0.5f);
            Rail.Position(0, -1.4f, -3f);
            Rail.Scale(0.5f);
            Sensor = new SkeletonTracker();
            Sensor.Start();
            Sensor.SkeletonEvent += new SkeletonTrackerEvent(GetEvent);
        }

        /// <summary>
        /// Punkte und Linien für einen Körpder erstellen
        /// </summary>
        private void Initialize()
        {
            
           
            
            Player = new Player();
            Player.Scale = 0.5f;
        }

        /// <summary>
        /// Punkte und Linien neu setzen und ausgabe neu Zeichnen
        /// </summary>
        public void Update()
        {
            Player.Update();
            
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
