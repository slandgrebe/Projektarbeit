using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MotionDetection;
using JumpAndRun.Item;

namespace JumpAndRun.GameLogic
{
    /// <summary>
    /// Stellt einen Spielcharakter in der Ausgabe dar
    /// </summary>
    public class Player
    {
        /// <summary>Skalierungsgrad der Spielfigur als Ganzes</summary>
        public float Scale { private get; set; }
        /// <summary>Spielfigur der Kamera anhängen</summary>
        public bool Attach { private get; set; }
        /// <summary>Aktuell noch verfügbare Leben der Spielfigur</summary>
        public uint Lives { get; set; }
        /// <summary>Aktueller Punktestand der Spielfigur</summary>
        public uint Score { get; set; }

        /// <summary>Modell des Kopfes</summary>
        public Model Head { get; private set; }
        /// <summary>Modell des Körpers</summary>
        public Model Torso { get; private set; }
        /// <summary>Modell des linken Oberarms</summary>
        public Model UpperarmLeft { get; private set; }
        /// <summary>Modell des rechten Oberarms</summary>
        public Model UpperarmRight { get; private set; }
        /// <summary>Modell des linken Untgerarms</summary>
        public Model ForearmLeft { get; private set; }
        /// <summary>Modell des rechten Unterarms</summary>
        public Model ForearmRight { get; private set; }
        /// <summary>Modell des linken Oberschenkels</summary>
        public Model ThighlegLeft { get; private set; }
        /// <summary>Modell des rechten Oberschenkels</summary>
        public Model ThighlegRight { get; private set; }
        /// <summary>Modell des linken Unterschenkels</summary>
        public Model LowerlegLeft { get; private set; }
        /// <summary>Modell des linken Unterschenkels</summary>
        public Model LowerlegRight { get; private set; }
        
        /// <summary>Modelle mit denen die Spielfigur schon Kollidiert ist</summary>
        public List<uint> Colidet { get; set; }

        /// <summary>
        /// Initialisierung der Spielfigur
        /// </summary>
        public Player()
        {
            Head = new Model("data/models/player/head.3ds", false);
            Torso = new Model("data/models/player/torso.3ds", false);
            UpperarmLeft = new Model("data/models/player/upperarm.3ds", false);
            UpperarmRight = new Model("data/models/player/upperarm.3ds", false);
            ForearmLeft = new Model("data/models/player/forearm.3ds", false);
            ForearmRight = new Model("data/models/player/forearm.3ds", false);
            ThighlegLeft = new Model("data/models/player/thighleg.3ds", false);
            ThighlegRight = new Model("data/models/player/thighleg.3ds", false);
            LowerlegLeft = new Model("data/models/player/lowerleg.3ds", false);
            LowerlegRight = new Model("data/models/player/lowerleg.3ds", false);

            Colidet = new List<uint>();

            Scale = 1;
        }

        /// <summary>
        /// Positionierung der einzelnen Modelle der Spielfigur anhand Personenerkennung
        /// </summary>
        /// <returns>Prüfung ob die Operation durchgeführt werden konnte</returns>
        public bool Update()
        {
            Body.Instance.ZModifikator((Body.Instance.Z*-1) -4.5f); // Spielfigur vor der Kamera positionieren
            Body.Instance.YModifikator(-0.7f);
            if (!ScalePlayer()) return false;
            if (!Alignment()) return false;
            if (!AttachToCamera()) return false;
            return true;
        }

        /// <summary>
        /// Gib die absolute Z Koordinate der Spielfigur in der Welt zurück.
        /// </summary>
        /// <returns>Gib die absolute Z Koordinate der Spielfigur ion der Welt zurück</returns>
        public float GetPosition()
        {
            return View.Model.PositionZ(Torso.Id);
        }

        /// <summary>
        /// Die einzelnen Körperteile neu ausrichten
        /// </summary>
        /// <returns>Prüfung ob die Operation durchgeführt werden konnte</returns>
        private bool Alignment()
        {
            if (!Head.Position(Body.Instance.Head.X, Body.Instance.Head.Y, Body.Instance.Head.Z)) return false;
            if (!Torso.Alignment(Body.Instance.HipCenter.X, Body.Instance.HipCenter.Y, Body.Instance.HipCenter.Z,
                Body.Instance.ShoulderCenter.X, Body.Instance.ShoulderCenter.Y, Body.Instance.ShoulderCenter.Z)) return false;
            if (!UpperarmLeft.Alignment(Body.Instance.ShoulderLeft.X, Body.Instance.ShoulderLeft.Y, Body.Instance.ShoulderLeft.Z,
                Body.Instance.ElbowLeft.X, Body.Instance.ElbowLeft.Y, Body.Instance.ElbowLeft.Z)) return false;
            if (!UpperarmRight.Alignment(Body.Instance.ShoulderRight.X, Body.Instance.ShoulderRight.Y, Body.Instance.ShoulderRight.Z,
                Body.Instance.ElbowRight.X, Body.Instance.ElbowRight.Y, Body.Instance.ElbowRight.Z)) return false;
            if (!ForearmLeft.Alignment(Body.Instance.ElbowLeft.X, Body.Instance.ElbowLeft.Y, Body.Instance.ElbowLeft.Z,
                Body.Instance.WristLeft.X, Body.Instance.WristLeft.Y, Body.Instance.WristLeft.Z)) return false;
            if (!ForearmRight.Alignment(Body.Instance.ElbowRight.X, Body.Instance.ElbowRight.Y, Body.Instance.ElbowRight.Z,
                Body.Instance.WristRight.X, Body.Instance.WristRight.Y, Body.Instance.WristRight.Z)) return false;
            if (!ThighlegLeft.Alignment(Body.Instance.HipLeft.X, Body.Instance.HipLeft.Y, Body.Instance.HipLeft.Z,
                Body.Instance.KneeLeft.X, Body.Instance.KneeLeft.Y, Body.Instance.KneeLeft.Z)) return false;
            if (!ThighlegRight.Alignment(Body.Instance.HipRight.X, Body.Instance.HipRight.Y, Body.Instance.HipRight.Z,
                Body.Instance.KneeRight.X, Body.Instance.KneeRight.Y, Body.Instance.KneeRight.Z)) return false;
            if (!LowerlegLeft.Alignment(Body.Instance.KneeLeft.X, Body.Instance.KneeLeft.Y, Body.Instance.KneeLeft.Z,
                Body.Instance.AnkleLeft.X, Body.Instance.AnkleLeft.Y, Body.Instance.AnkleLeft.Z)) return false;
            if (!LowerlegRight.Alignment(Body.Instance.KneeRight.X, Body.Instance.KneeRight.Y, Body.Instance.KneeRight.Z,
                Body.Instance.AnkleRight.X, Body.Instance.AnkleRight.Y, Body.Instance.AnkleRight.Z)) return false;
            return true;
        }

        /// <summary>
        /// Spielfigur Skallieren
        /// </summary>
        /// <returns>Prüfung ob die Operation durchgeführt werden konnte</returns>
        private bool ScalePlayer()
        {
            Body.Instance.Scale(Scale);
            
            if (!Head.Scale(Scale)) return false;
            if (!Torso.Scale(Scale)) return false;
            if (!UpperarmLeft.Scale(Scale)) return false;
            if (!UpperarmRight.Scale(Scale)) return false;
            if (!ForearmLeft.Scale(Scale)) return false;
            if (!ForearmRight.Scale(Scale)) return false;
            if (!ThighlegLeft.Scale(Scale)) return false;
            if (!ThighlegRight.Scale(Scale)) return false;
            if (!LowerlegLeft.Scale(Scale)) return false;
            if (!LowerlegRight.Scale(Scale)) return false;
            return true;
        }

        /// <summary>
        /// Spielfigur der Kamera anhängen.
        /// </summary>
        /// <returns>Prüfung ob die Operation durchgeführt werden konnte</returns>
        private bool AttachToCamera()
        {
            if (!Head.AttachToCamera(Attach)) return false;
            if (!UpperarmLeft.AttachToCamera(Attach)) return false;
            if (!UpperarmRight.AttachToCamera(Attach)) return false;
            if (!ForearmLeft.AttachToCamera(Attach)) return false;
            if (!ForearmRight.AttachToCamera(Attach)) return false;
            if (!Torso.AttachToCamera(Attach)) return false;
            if (!ThighlegLeft.AttachToCamera(Attach)) return false;
            if (!ThighlegRight.AttachToCamera(Attach)) return false;
            if (!LowerlegLeft.AttachToCamera(Attach)) return false;
            if (!LowerlegRight.AttachToCamera(Attach)) return false;
            return true;
        }
    }
}
