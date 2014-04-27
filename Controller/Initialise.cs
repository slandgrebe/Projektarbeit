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
        

        public Initialise(){
            
            
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
            //Visualization.position(textId, 0f, 0f, 1.0f);
            Visualization.PositionCamera(1000, 0, 0);
        }

        public void Tracked(){
            //Visualization.position(textId, 1000f, 0f, 1.0f);
        }        
    }
}
