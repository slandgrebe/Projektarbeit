using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotionDetection
{
    /// <summary>
    /// Bildet die Körperpunkte einer getrackten Person ab.
    /// </summary>
    public class Body
    {
        /// <summary>Instanz des Positionobjektes</summary>
        private static Body instance;
        /// <summary>Koordinatenobjekt des Punktes: Fussgelenk links</summary>
        public Position AnkleLeft { get; set; }
        /// <summary>Koordinatenobjekt des Punktes: Fussgelenk rechts</summary>
        public Position AnkleRight { get; set; }
        /// <summary>Koordinatenobjekt des Punktes: Ellenbogen links</summary>
        public Position ElbowLeft { get; set; }
        /// <summary>Koordinatenobjekt des Punktes: Ellenbogen rechts</summary>
        public Position ElbowRight { get; set; }
        /// <summary>Koordinatenobjekt des Punktes: Fuss links</summary>
        public Position FootLeft { get; set; }
        /// <summary>Koordinatenobjekt des Punktes: Fuss rechts</summary>
        public Position FootRight { get; set; }
        /// <summary>Koordinatenobjekt des Punktes: Hand links</summary>
        public Position HandLeft { get; set; }
        /// <summary>Koordinatenobjekt des Punktes: Hand rechts</summary>
        public Position HandRight { get; set; }
        /// <summary>Koordinatenobjekt des Punktes: Kopf</summary>
        public Position Head { get; set; }
        /// <summary>Koordinatenobjekt des Punktes: Hüfte mitte</summary>
        public Position HipCenter { get; set; }
        /// <summary>Koordinatenobjekt des Punktes: Hüfte links</summary>
        public Position HipLeft { get; set; }
        /// <summary>Koordinatenobjekt des Punktes: Hüfte rechts</summary>
        public Position HipRight { get; set; }
        /// <summary>Koordinatenobjekt des Punktes: Knie links</summary>
        public Position KneeLeft { get; set; }
        /// <summary>Koordinatenobjekt des Punktes: Knie rechts</summary>
        public Position KneeRight { get; set; }
        /// <summary>Koordinatenobjekt des Punktes: Schulter mitte</summary>
        public Position ShoulderCenter { get; set; }
        /// <summary>Koordinatenobjekt des Punktes: Schulter links</summary>
        public Position ShoulderLeft { get; set; }
        /// <summary>Koordinatenobjekt des Punktes: Schulter rechts</summary>
        public Position ShoulderRight { get; set; }
        /// <summary>Koordinatenobjekt des Punktes: Rückgrat</summary>
        public Position Spine { get; set; }
        /// <summary>Koordinatenobjekt des Punktes: Handgelenk links</summary>
        public Position WristLeft { get; set; }
        /// <summary>Koordinatenobjekt des Punktes: Handgelenk rechts</summary>
        public Position WristRight { get; set; }
        /// <summary>X Koordinate des Körpers</summary>
        public float X { get; set; }
        /// <summary>Y Koordinate des Körpers</summary>
        public float Y { get; set; }
        /// <summary>Z Koordinate des Körpers</summary>
        public float Z { get; set; }
        /// <summary>Flag, ob eine Person aktuell erkennt wird</summary>
        private bool _isTracked;
        /// <summary>Flag, ob eine Person aktuell erkennt wird. Erfolgt 2 Sekundenlang kein Signal, so wird das Flag auf false gesetzt.</summary>
        public bool IsTracked
        {
            get
            {
                if (Math.Abs(trackedStartTime.Subtract(DateTime.Now).TotalSeconds) >= 2)
                {
                    _isTracked = false;
                }
                return _isTracked;
            }
            set
            {
                if (value)
                {
                    trackedStartTime = DateTime.Now;
                    _isTracked = value;
                }
                if (Math.Abs(trackedStartTime.Subtract(DateTime.Now).TotalSeconds) >= 2)
                {
                    _isTracked = value;
                }
            }
        }
        /// <summary>Zeitpunkt der letzten erfolgreichen erkennung einer Person</summary>
        private DateTime trackedStartTime = DateTime.Now;

        /// <summary>
        /// Konstruktor, Initialisiert die Körperpunkte
        /// </summary>
        private Body()
        {
            IsTracked = false;
            AnkleLeft = new Position();
            AnkleRight = new Position();
            ElbowLeft = new Position();
            ElbowRight = new Position();
            FootLeft = new Position();
            FootRight = new Position();
            HandLeft = new Position();
            HandRight = new Position();
            Head = new Position();
            HipCenter = new Position();
            HipLeft = new Position();
            HipRight = new Position();
            KneeLeft = new Position();
            KneeRight = new Position();
            ShoulderCenter = new Position();
            ShoulderLeft = new Position();
            ShoulderRight = new Position();
            Spine = new Position();
            WristLeft = new Position();
            WristRight = new Position();
        }

        /// <summary>
        /// Stellt sicher, dass diese Klasse nur einmal Instanziert wird.
        /// </summary>
        /// <returns>instance der Klasse Position</returns>
        public static Body Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Body();
                }
                return instance;
            }
        }

        /// <summary>
        /// Modifiziert alle Körperpunkte entlang der X-Achse um den Modifikator.
        /// </summary>
        /// <param name="mod">Modifikator</param>
        /// 
        public void XModifikator(float mod)
        {
            AnkleLeft.XModifikator = mod;
            AnkleRight.XModifikator = mod;
            ElbowLeft.XModifikator = mod;
            ElbowRight.XModifikator = mod;
            FootLeft.XModifikator = mod;
            FootRight.XModifikator = mod;
            HandLeft.XModifikator = mod;
            HandRight.XModifikator = mod;
            Head.XModifikator = mod;
            HipCenter.XModifikator = mod;
            HipLeft.XModifikator = mod;
            HipRight.XModifikator = mod;
            KneeLeft.XModifikator = mod;
            KneeRight.XModifikator = mod;
            ShoulderCenter.XModifikator = mod;
            ShoulderLeft.XModifikator = mod;
            ShoulderRight.XModifikator = mod;
            Spine.XModifikator = mod;
            WristLeft.XModifikator = mod;
            WristRight.XModifikator = mod;
        }

        /// <summary>
        /// Modifiziert alle Körperpunkte entlang der Y-Achse um den Modifikator.
        /// </summary>
        /// <param name="mod">Modifikator</param>
        public void YModifikator(float mod)
        {
            AnkleLeft.YModifikator = mod;
            AnkleRight.YModifikator = mod;
            ElbowLeft.YModifikator = mod;
            ElbowRight.YModifikator = mod;
            FootLeft.YModifikator = mod;
            FootRight.YModifikator = mod;
            HandLeft.YModifikator = mod;
            HandRight.YModifikator = mod;
            Head.YModifikator = mod;
            HipCenter.YModifikator = mod;
            HipLeft.YModifikator = mod;
            HipRight.YModifikator = mod;
            KneeLeft.YModifikator = mod;
            KneeRight.YModifikator = mod;
            ShoulderCenter.YModifikator = mod;
            ShoulderLeft.YModifikator = mod;
            ShoulderRight.YModifikator = mod;
            Spine.YModifikator = mod;
            WristLeft.YModifikator = mod;
            WristRight.YModifikator = mod;
        }

        /// <summary>
        /// Modifiziert alle Körperpunkte entlang der Z-Achse um den Modifikator.
        /// </summary>
        /// <param name="mod">Modifikator</param>
        /// 
        public void ZModifikator(float mod)
        {
            AnkleLeft.ZModifikator = mod;
            AnkleRight.ZModifikator = mod;
            ElbowLeft.ZModifikator = mod;
            ElbowRight.ZModifikator = mod;
            FootLeft.ZModifikator = mod;
            FootRight.ZModifikator = mod;
            HandLeft.ZModifikator = mod;
            HandRight.ZModifikator = mod;
            Head.ZModifikator = mod;
            HipCenter.ZModifikator = mod;
            HipLeft.ZModifikator = mod;
            HipRight.ZModifikator = mod;
            KneeLeft.ZModifikator = mod;
            KneeRight.ZModifikator = mod;
            ShoulderCenter.ZModifikator = mod;
            ShoulderLeft.ZModifikator = mod;
            ShoulderRight.ZModifikator = mod;
            Spine.ZModifikator = mod;
            WristLeft.ZModifikator = mod;
            WristRight.ZModifikator = mod;
        }

        /// <summary>
        /// Skalliert den Getrackten Körper um den Skalierwert.
        /// </summary>
        /// <param name="scale">Skalierwert</param>
        public void Scale(float scale)
        {
            AnkleLeft.Scale = scale;
            AnkleRight.Scale = scale;
            ElbowLeft.Scale = scale;
            ElbowRight.Scale = scale;
            FootLeft.Scale = scale;
            FootRight.Scale = scale;
            HandLeft.Scale = scale;
            HandRight.Scale = scale;
            Head.Scale = scale;
            HipCenter.Scale = scale;
            HipLeft.Scale = scale;
            HipRight.Scale = scale;
            KneeLeft.Scale = scale;
            KneeRight.Scale = scale;
            ShoulderCenter.Scale = scale;
            ShoulderLeft.Scale = scale;
            ShoulderRight.Scale = scale;
            Spine.Scale = scale;
            WristLeft.Scale = scale;
            WristRight.Scale = scale;
        }
    }
}
