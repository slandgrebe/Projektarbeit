using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
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
        /// <summary>Flag, ob eine Person aktuell erkennt wird</summary>
        private bool _isTracked;
        /// <summary>Flag, ob eine Person aktuell erkennt wird. Erfolgt 2 Sekundenlang kein Signal, so wird das Flag auf false gesetzt.</summary>
        public bool IsTracked
        {
            get { return _isTracked; }
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
        /// Modifiziert alle Körperpunkte entlang der Z-Achse um den Modifikator.
        /// </summary>
        /// <param name="mod">Modifikator</param>
        public void ZModifikator(float mod){
            if (mod <= 0)
            {
                // Negativer Modifikator
                AnkleLeft.Z += mod;
                AnkleRight.Z += mod;
                ElbowLeft.Z += mod;
                ElbowRight.Z += mod;
                FootLeft.Z += mod;
                FootRight.Z += mod;
                HandLeft.Z += mod;
                HandRight.Z += mod;
                Head.Z += mod;
                HipCenter.Z += mod;
                HipLeft.Z += mod;
                HipRight.Z += mod;
                KneeLeft.Z += mod;
                KneeRight.Z += mod;
                ShoulderCenter.Z += mod;
                ShoulderLeft.Z += mod;
                ShoulderRight.Z += mod;
                Spine.Z += mod;
                WristLeft.Z += mod;
                WristRight.Z += mod;
            }
            else
            {
                // Positiver Modifikator
                AnkleLeft.Z -= mod;
                AnkleRight.Z -= mod;
                ElbowLeft.Z -= mod;
                ElbowRight.Z -= mod;
                FootLeft.Z -= mod;
                FootRight.Z -= mod;
                HandLeft.Z -= mod;
                HandRight.Z -= mod;
                Head.Z -= mod;
                HipCenter.Z -= mod;
                HipLeft.Z -= mod;
                HipRight.Z -= mod;
                KneeLeft.Z -= mod;
                KneeRight.Z -= mod;
                ShoulderCenter.Z -= mod;
                ShoulderLeft.Z -= mod;
                ShoulderRight.Z -= mod;
                Spine.Z -= mod;
                WristLeft.Z -= mod;
                WristRight.Z -= mod;
            }
        }

        /// <summary>
        /// Modifiziert alle Körperpunkte entlang der Y-Achse um den Modifikator.
        /// </summary>
        /// <param name="mod">Modifikator</param>
        public void YModifikator(float mod)
        {
            if (mod <= 0)
            {
                // Negativer Modifikator
                AnkleLeft.Y += mod;
                AnkleRight.Y += mod;
                ElbowLeft.Y += mod;
                ElbowRight.Y += mod;
                FootLeft.Y += mod;
                FootRight.Y += mod;
                HandLeft.Y += mod;
                HandRight.Y += mod;
                Head.Y += mod;
                HipCenter.Y += mod;
                HipLeft.Y += mod;
                HipRight.Y += mod;
                KneeLeft.Y += mod;
                KneeRight.Y += mod;
                ShoulderCenter.Y += mod;
                ShoulderLeft.Y += mod;
                ShoulderRight.Y += mod;
                Spine.Y += mod;
                WristLeft.Y += mod;
                WristRight.Y += mod;
            }
            else
            {
                // Positiver Modifikator
                AnkleLeft.Y -= mod;
                AnkleRight.Y -= mod;
                ElbowLeft.Y -= mod;
                ElbowRight.Y -= mod;
                FootLeft.Y -= mod;
                FootRight.Y -= mod;
                HandLeft.Y -= mod;
                HandRight.Y -= mod;
                Head.Y -= mod;
                HipCenter.Y -= mod;
                HipLeft.Y -= mod;
                HipRight.Y -= mod;
                KneeLeft.Y -= mod;
                KneeRight.Y -= mod;
                ShoulderCenter.Y -= mod;
                ShoulderLeft.Y -= mod;
                ShoulderRight.Y -= mod;
                Spine.Y -= mod;
                WristLeft.Y -= mod;
                WristRight.Y -= mod;
            }
        }

        /// <summary>
        /// Skalliert den Getrackten Körper um den Skalierwert.
        /// </summary>
        /// <param name="scale">Skalierwert</param>
        public void Scale(float scale)
        {
            // Skallieren der X-Achse
            AnkleLeft.X *= scale;
            AnkleRight.X *= scale;
            ElbowLeft.X *= scale;
            ElbowRight.X *= scale;
            FootLeft.X *= scale;
            FootRight.X *= scale;
            HandLeft.X *= scale;
            HandRight.X *= scale;
            Head.X *= scale;
            HipCenter.X *= scale;
            HipLeft.X *= scale;
            HipRight.X *= scale;
            KneeLeft.X *= scale;
            KneeRight.X *= scale;
            ShoulderCenter.X *= scale;
            ShoulderLeft.X *= scale;
            ShoulderRight.X *= scale;
            Spine.X *= scale;
            WristLeft.X *= scale;
            WristRight.X *= scale;

            // Skallieren der Y-Achse
            AnkleLeft.Y *= scale;
            AnkleRight.Y *= scale;
            ElbowLeft.Y *= scale;
            ElbowRight.Y *= scale;
            FootLeft.Y *= scale;
            FootRight.Y *= scale;
            HandLeft.Y *= scale;
            HandRight.Y *= scale;
            Head.Y *= scale;
            HipCenter.Y *= scale;
            HipLeft.Y *= scale;
            HipRight.Y *= scale;
            KneeLeft.Y *= scale;
            KneeRight.Y *= scale;
            ShoulderCenter.Y *= scale;
            ShoulderLeft.Y *= scale;
            ShoulderRight.Y *= scale;
            Spine.Y *= scale;
            WristLeft.Y *= scale;
            WristRight.Y *= scale;

            // Skallieren der Z-Achse
            AnkleLeft.Z *= scale;
            AnkleRight.Z *= scale;
            ElbowLeft.Z *= scale;
            ElbowRight.Z *= scale;
            FootLeft.Z *= scale;
            FootRight.Z *= scale;
            HandLeft.Z *= scale;
            HandRight.Z *= scale;
            Head.Z *= scale;
            HipCenter.Z *= scale;
            HipLeft.Z *= scale;
            HipRight.Z *= scale;
            KneeLeft.Z *= scale;
            KneeRight.Z *= scale;
            ShoulderCenter.Z *= scale;
            ShoulderLeft.Z *= scale;
            ShoulderRight.Z *= scale;
            Spine.Z *= scale;
            WristLeft.Z *= scale;
            WristRight.Z *= scale;
        }
    }
}
