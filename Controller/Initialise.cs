using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using View;

namespace Controller
{
    class Initialise
    {

        private static Initialise instance;
        private uint backgroundId = 0;
        private uint textId = 0;

        public Initialise(){
            backgroundId = Visualization.addPoint("Resource Files/Background/white.jpg");
            while (backgroundId != 0 && !Visualization.isCreated(backgroundId)) { }
            Visualization.scale(backgroundId, 10, 10, 1);
            Visualization.position(backgroundId, 1000f, 0f, -0.3f);
            
            textId = Visualization.addText("data/fonts/arial.ttf");
            while (!Visualization.isCreated(textId)) { }
            Visualization.text(textId, "Keine Person erkannt!");
            Visualization.textSize(textId, 50);
            Visualization.textColor(textId, 0f, 0f, 0f, 1.0f);
            
        }

        /// <summary>
        /// stellt sicher, dass diese Klasse nur einmal Instanziert wird.
        /// </summary>
        /// <returns>instance der Klasse Position</returns>
        public static Initialise Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Initialise();
                }
                return instance;
            }
        }

        public void NotTracked(){
            Visualization.position(textId, 0f, 0f, 1.0f);
            Visualization.positionCamera(1000, 0, 0);
        }

        public void Tracked(){
            Visualization.position(textId, 1000f, 0f, 1.0f);
        }        
    }
}
