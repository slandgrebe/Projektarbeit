using System;
using System.Runtime.InteropServices;

namespace View
{
    /// <summary>
    /// Einbinden der Visualization.dll in C#
    /// </summary>
    public static class Camera
    {
        /// <summary>
        /// Positioniert die Kamera
        /// </summary>
        /// <param name="x">x Koordinate</param>
        /// <param name="y">y Koordinate</param>
        /// <param name="z">z Koordinate</param>
        [DllImport("Visualization.dll", EntryPoint = "positionCamera")]
        public extern static void PositionCamera(float x, float y, float z);
        /// <summary>
        /// Rotiert die Kamera um die Y-Achse.
        /// </summary>
        /// <param name="degrees">Rotationswinkel in Grad</param>
        [DllImport("Visualization.dll", EntryPoint = "rotateCamera")]
        public extern static void RotateCamera(float degrees);
        /// <summary>
        /// Rotiert die Kamera um die x-Achse
        /// </summary>
        /// <param name="degrees">Rotationswinkel in Grad</param>
        [DllImport("Visualization.dll", EntryPoint = "tiltCamera")]
        public extern static void TiltCamera(float degrees);
        /// <summary>
        /// Ändert die Geschwindikeit der Kamera
        /// </summary>
        /// <param name="speed">Geschwindigkeit in m/s (bzw. Einheiten/s)</param>
        [DllImport("Visualization.dll", EntryPoint = "changeCameraSpeed")]
        public extern static void ChangeCameraSpeed(float speed);
    }
}
