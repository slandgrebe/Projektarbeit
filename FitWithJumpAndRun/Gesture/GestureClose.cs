using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MotionDetection;

namespace JumpAndRun.Gesture
{
    /// <summary>
    /// Überprüft, ob eine durch den Körper getätigte Geste zum Schliessen für 1 Sekunde getätigt wird.
    /// </summary>
    public class GestureClose
    {
        /// <summary>Speichert die Startzeit der Geste</summary>
        private static DateTime StartTime = DateTime.Now.AddSeconds(-2);
        /// <summary>
        /// Gibt True zurück, wenn die Gestig erkannt wurde
        /// </summary>
        /// <returns></returns>
        public static bool IsTrue()
        {
            System.Console.WriteLine(Math.Abs(StartTime.Subtract(DateTime.Now).TotalSeconds));
            if (Body.Instance.ElbowLeft.Y < Body.Instance.ShoulderLeft.Y && Body.Instance.ElbowRight.Y < Body.Instance.ShoulderRight.Y) // Ellebogen befinden sich unterhalb der Schultern
            {
                if (Body.Instance.HandLeft.X > Body.Instance.HandRight.X) // Linke Hand ist rechts von der rechten Hand
                {
                    if (Body.Instance.HandLeft.Y > Body.Instance.ElbowLeft.Y && Body.Instance.HandRight.Y > Body.Instance.ElbowRight.Y) // Hände sind oberhalb der Ellebogen
                    {
                        if (Math.Abs(StartTime.Subtract(DateTime.Now).TotalSeconds) >= 1)
                        {
                            return true;
                        }
                        return false;
                    }
                }
            }
            StartTime = DateTime.Now;
            return false;
        }
    }
}
