using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Controller
{
    /// <summary>
    /// Überprüft, ob eine durch die Körperbewegung durchgeführte Auswahl getätigt wird.
    /// </summary>
    class Click
    {
        /// <summary>Speichert die Z Koordinate der Rechten Hand zur Startzeit zwischen</summary>
        private float HandRightStart = 0;
        /// <summary>Speichert die Z Koordinate der Linken Hand zur Startzeit zwischen</summary>
        private float HandLeftStart = 0;
        /// <summary>Speichert die Startzeit der Geste</summary>
        private DateTime StartTime = DateTime.Now;

        /// <summary>
        /// Überprüft, ob die Hand innerhalb einer halben Sekunde nach vorne bewegt wurde
        /// </summary>
        /// <returns>Auswahl wurde getätigt</returns>
        public bool IsClicked(){
            // Geste Starten
            if (Math.Abs(StartTime.Subtract(DateTime.Now).TotalSeconds) >= 0.5)
            {
                HandRightStart = Body.Instance.HandRight.Z;
                HandLeftStart = Body.Instance.HandLeft.Z;
                StartTime = DateTime.Now;
            }

            // Hand wurde nach vorne bewegt
            if ((HandRightStart - Body.Instance.HandRight.Z) >= 0.02)
            {
                return true;
            }
            if ((HandLeftStart - Body.Instance.HandLeft.Z) >= 0.02)
            {
                return true;
            }
            // Hand wurde nicht nach vorne bewegt
            return false;
        }
    }
}
