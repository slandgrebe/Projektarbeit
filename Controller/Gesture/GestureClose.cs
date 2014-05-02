using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Controller
{
    /// <summary>
    /// Überprüft, ob eine durch den Körper getätigte Geste zum Schliessen getätigt wird.
    /// </summary>
    class GestureClose
    {
        /// <summary>
        /// Gibt True zurück, wenn die Gestig erkannt wurde
        /// </summary>
        /// <returns></returns>
        public static bool IsTrue()
        {
            if (Body.Instance.ElbowLeft.Y < Body.Instance.ShoulderLeft.Y && Body.Instance.ElbowRight.Y < Body.Instance.ShoulderRight.Y) // Ellebogen befinden sich unterhalb der Schultern
            {
                if (Body.Instance.HandLeft.X > Body.Instance.HandRight.X) // Linke Hand ist rechts von der rechten Hand
                {
                    if (Body.Instance.HandLeft.Y > Body.Instance.ElbowLeft.Y && Body.Instance.HandRight.Y > Body.Instance.ElbowRight.Y) // Hände sind oberhalb der Ellebogen
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
