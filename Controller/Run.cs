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
            
            Sensor = new SkeletonTracker();
            Sensor.Start();
            Sensor.SkeletonEvent += new SkeletonTrackerEvent(GetEvent);
            game = Game.Instance;
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
            if(game != null){
                game.Update();
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
