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
        private Model[] Rails = new Model[200];

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
            float distance = 0;
            Visualization.positionCamera(0, 1.5f, 0);
            for (int i = 0; i < 20; i++)
            {
                Rails[i] = new Model("Resource Files/Models/Rail/Rail.3ds");
                Rails[i].Scale(20f);
                distance = (23f/3.333f*i)*-1;
                Rails[i].Position(0, 0, distance);
            }

            
            Wagon.Position(0, -1.3f, -3.5f);
            Wagon.Scale(1);
            Visualization.attachToCamera(Wagon.Id, true);


            Visualization.changeCameraSpeed(5f);

            //WagonCam.Position(0, 0.5f, -1f);
            //WagonCam.Scale(0.5f);
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
