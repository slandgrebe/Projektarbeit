using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Controller
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
            Head = new Model("Resource Files/Models/Player/Head.3ds", false);
            Torso = new Model("Resource Files/Models/Player/Torso.3ds", false);
            UpperarmLeft = new Model("Resource Files/Models/Player/UpperArm.3ds", false);
            UpperarmRight = new Model("Resource Files/Models/Player/UpperArm.3ds", false);
            ForearmLeft = new Model("Resource Files/Models/Player/ForeArm.3ds", false);
            ForearmRight = new Model("Resource Files/Models/Player/ForeArm.3ds", false);
            ThighlegLeft = new Model("Resource Files/Models/Player/ThighLeg.3ds", false);
            ThighlegRight = new Model("Resource Files/Models/Player/ThighLeg.3ds", false);
            LowerlegLeft = new Model("Resource Files/Models/Player/LowerLeg.3ds", false);
            LowerlegRight = new Model("Resource Files/Models/Player/LowerLeg.3ds", false);

            Colidet = new List<uint>();
        }

        /// <summary>
        /// Positionierung der einzelnen Modelle der Spielfigur anhand Personenerkennung
        /// </summary>
        public void Update()
        {
            Body.Instance.ZModifikator(Body.Instance.Z + 4.5f);
            Body.Instance.YModifikator(-0.7f);
            ScalePlayer();
            Alignment();
            AttachToCamera();
        }

        /// <summary>
        /// Gib die absolute Z Koordinate der Spielfigur zurück.
        /// </summary>
        /// <returns></returns>
        public float GetPosition()
        {
            return View.Visualization.PositionZ(Torso.Id);
        }

        /// <summary>
        /// Die einzelnen Körperteile neu ausrichten
        /// </summary>
        private void Alignment()
        {
            Head.Position(Body.Instance.Head.X, Body.Instance.Head.Y, Body.Instance.Head.Z);
            Torso.Alignment(Body.Instance.HipCenter.X, Body.Instance.HipCenter.Y, Body.Instance.HipCenter.Z,
                Body.Instance.ShoulderCenter.X, Body.Instance.ShoulderCenter.Y, Body.Instance.ShoulderCenter.Z);
            UpperarmLeft.Alignment(Body.Instance.ShoulderLeft.X, Body.Instance.ShoulderLeft.Y, Body.Instance.ShoulderLeft.Z,
                Body.Instance.ElbowLeft.X, Body.Instance.ElbowLeft.Y, Body.Instance.ElbowLeft.Z);
            UpperarmRight.Alignment(Body.Instance.ShoulderRight.X, Body.Instance.ShoulderRight.Y, Body.Instance.ShoulderRight.Z,
                Body.Instance.ElbowRight.X, Body.Instance.ElbowRight.Y, Body.Instance.ElbowRight.Z);
            ForearmLeft.Alignment(Body.Instance.ElbowLeft.X, Body.Instance.ElbowLeft.Y, Body.Instance.ElbowLeft.Z,
                Body.Instance.WristLeft.X, Body.Instance.WristLeft.Y, Body.Instance.WristLeft.Z);
            ForearmRight.Alignment(Body.Instance.ElbowRight.X, Body.Instance.ElbowRight.Y, Body.Instance.ElbowRight.Z,
                Body.Instance.WristRight.X, Body.Instance.WristRight.Y, Body.Instance.WristRight.Z);
            ThighlegLeft.Alignment(Body.Instance.HipLeft.X, Body.Instance.HipLeft.Y, Body.Instance.HipLeft.Z,
                Body.Instance.KneeLeft.X, Body.Instance.KneeLeft.Y, Body.Instance.KneeLeft.Z);
            ThighlegRight.Alignment(Body.Instance.HipRight.X, Body.Instance.HipRight.Y, Body.Instance.HipRight.Z,
                Body.Instance.KneeRight.X, Body.Instance.KneeRight.Y, Body.Instance.KneeRight.Z);
            LowerlegLeft.Alignment(Body.Instance.KneeLeft.X, Body.Instance.KneeLeft.Y, Body.Instance.KneeLeft.Z,
                Body.Instance.AnkleLeft.X, Body.Instance.AnkleLeft.Y, Body.Instance.AnkleLeft.Z);
            LowerlegRight.Alignment(Body.Instance.KneeRight.X, Body.Instance.KneeRight.Y, Body.Instance.KneeRight.Z,
                Body.Instance.AnkleRight.X, Body.Instance.AnkleRight.Y, Body.Instance.AnkleRight.Z);
        }

        /// <summary>
        /// Spielfigur Skallieren
        /// </summary>
        private void ScalePlayer()
        {
            Body.Instance.Scale(Scale);
            
            Head.Scale(Scale);
            Torso.Scale(Scale);
            UpperarmLeft.Scale(Scale);
            UpperarmRight.Scale(Scale);
            ForearmLeft.Scale(Scale);
            ForearmRight.Scale(Scale);
            ThighlegLeft.Scale(Scale);
            ThighlegRight.Scale(Scale);
            LowerlegLeft.Scale(Scale);
            LowerlegRight.Scale(Scale);
        }

        // Spielfigur der Kamera anhängen
        private void AttachToCamera()
        {
            Head.AttachToCamera(Attach);
            UpperarmLeft.AttachToCamera(Attach);
            UpperarmRight.AttachToCamera(Attach);
            ForearmLeft.AttachToCamera(Attach);
            ForearmRight.AttachToCamera(Attach);
            Torso.AttachToCamera(Attach);
            ThighlegLeft.AttachToCamera(Attach);
            ThighlegRight.AttachToCamera(Attach);
            LowerlegLeft.AttachToCamera(Attach);
            LowerlegRight.AttachToCamera(Attach);
        }
    }
}
