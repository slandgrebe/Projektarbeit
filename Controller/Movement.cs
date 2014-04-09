using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using View;
using Model;

namespace Controller
{
    class Movement
    {
        private SkeletonTracker sensor = null;
        public Player Player { get; private set; }

        private int init = 0;

        /// <summary>
        /// Startet Kinect und registriert die Punkte und Linien des Körper in der Ausgabe
        /// </summary>
        public Movement()
        {
            // Sensor starten
            Start();
        }

        /// <summary>
        /// Sensor starten
        /// </summary>
        public void Start()
        {
            sensor = new SkeletonTracker();
            sensor.Start();
            sensor.SkeletonEvent += new SkeletonTrackerEvent(GetEvent);
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
            Player.Instance.Update();
        }

        /// <summary>
        /// Ausgabe und Sensor sauber Beenden
        /// </summary>
        public void End()
        {
            //Visualization.close();
            sensor.Stop();
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
