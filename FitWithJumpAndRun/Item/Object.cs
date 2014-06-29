﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace JumpAndRun.Item
{
    /// <summary>
    /// Übernimmt die Darstellung es eines Modelles innerhalb eines Levels.
    /// </summary>
    public class Object
    {
        /// <summary>XML Pfad zum Objekt</summary>
        public string ModelXmlPath {get; set; }

        /// <summary>Rotationsachse: x, y, z</summary>
        public float[] RotationAxis { get; set; }
        /// <summary>Rotationswinkel in Grad</summary>
        public float RotationAngle { get; set; }

        /// <summary>Skalierung des Objekts</summary>
        public float Scale { get; set; }
        /// <summary>Positionierung X Koordinate des Objekts</summary>
        public float PosX { get; set; }
        /// <summary>Positionierung Y Koordinate des Objekts</summary>
        public float PosY { get; set; }
        /// <summary>Positionierung Z Koordinate des Objekts</summary>
        public float PosZ { get; set; }
        /// <summary>An Kamera anhängen</summary>
        public bool AttachToCamera { get; set; }
        /// <summary>Schwiergikeitsgrad des Objektes</summary>
        public int Severity { get; set; }
        /// <summary>Beinhaltet das Modell</summary>
        public Model Model { get; set; }
        /// <summary>Soundobjekt</summary>
        private Sound.Sound SoundCollision = null;

        /// <summary>
        /// Initialisierung des Objektes.
        /// </summary>
        public Object()
        {
            Scale = 1;
            PosX = 0;
            PosY = 0;
            PosZ = 0;
            RotationAxis = new float[] { 1f, 0f, 0f };
            RotationAngle = 0f;
            Severity = 1;
        }

        /// <summary>
        /// Erstellt das Objekt in der Anzeige
        /// </summary>
        /// <returns>Prüfung ob die Operation durchgeführt werden konnte</returns>
        public bool Create()
        {
            if (Model == null) return false;
            if (!Model.Create()) return false;
            if (!Model.Position(PosX, PosY, PosZ * -1)) return false;
            if (!Model.Scale(Scale)) return false;
            if (!Model.AttachToCamera(AttachToCamera)) return false;
            if (!Model.Rotate(RotationAngle, RotationAxis[0], RotationAxis[1], RotationAxis[2])) return false;
            return true;
        }

        /// <summary>
        /// Erstellt das Objekt in der Anzeige
        /// </summary>
        /// <param name="z">Relativer 0 Punkt in der Z Koordinate</param>
        /// <returns>Prüfung ob die Operation durchgeführt werden konnte</returns>
        public bool Create(float z)
        {
            if (Model == null) return false;
            if (!Model.Create()) return false;
            if (!Model.Position(PosX, PosY, (z + PosZ) * -1)) return false;
            if (!Model.Scale(Scale)) return false;
            if (!Model.AttachToCamera(AttachToCamera)) return false;
            if (!Model.Rotate(RotationAngle, RotationAxis[0], RotationAxis[1], RotationAxis[2])) return false;
            return true;
        }
        
        /// <summary>
        /// Erstellt das Objekt in der Anzeige
        /// </summary>
        /// <param name="z">Relativer 0 Punkt in der Z Koordinate</param>
        /// <param name="path">Pfad zur 3D Datei</param>
        /// <returns>Prüfung ob die Operation durchgeführt werden konnte</returns>
        public bool Create(float z, string path)
        {
            if (Model == null)
            {
                Model = new Model(path);
            }
            else
            {
                Model.Path = path;
                if (!Model.Create()) return false;
            }
            if (!Model.Position(PosX, PosY, (z + PosZ) * -1)) return false;
            if (!Model.Scale(Scale)) return false;
            if (!Model.AttachToCamera(AttachToCamera)) return false;
            if (!Model.Rotate(RotationAngle, RotationAxis[0], RotationAxis[1], RotationAxis[2])) return false;
            return true;
        }

        /// <summary>
        /// head anhand des XML erzeugen
        /// </summary>
        public void Deserialize()
        {
            FileStream stream;
            //stream = new FileStream(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + ModelXmlPath, FileMode.Open);
            stream = new FileStream(ModelXmlPath, FileMode.Open);
            XmlSerializer serializer = new XmlSerializer(typeof(Model));
            Model = (Model)serializer.Deserialize(stream);
            stream.Close();
        }

        /// <summary>
        /// Überprüft, ob das Objekt mit der Spielfigur Kollidiert
        /// </summary>
        /// <param name="player">Spielfigur</param>
        /// <param name="dispose">Modell soll nach der Kollision versteckt werden</param>
        /// <returns>Kollidiert Ja/Nein</returns>
        public bool Collision(JumpAndRun.GameLogic.Player player, bool dispose)
        {
            // COLLISION DETECTION
            System.Collections.Generic.Dictionary<uint, System.Collections.Generic.List<uint>> collisionList = new System.Collections.Generic.Dictionary<uint, System.Collections.Generic.List<uint>>();

            uint length = View.Model.CollisionsTextLength();

            System.Text.StringBuilder str = new System.Text.StringBuilder((int)length + 1);
            View.Model.CollisionsText(str, str.Capacity); // Daten aus DLL holen
            // str: 3;2 => 3 und 2 sind kollidiert
            String collisionsString = str.ToString(); // Daten parsen
            string[] models = collisionsString.Split(';');
            foreach (string model in models)
            {
                if (model.Length == 0) return false;
                string[] parts = model.Split(':');

                if (model == Model.Id.ToString())
                {
                    if (!player.Colided.Contains(Model.Id))
                    {
                        player.Colided.Add(Model.Id);
                        PlaySound();
                        if (dispose) this.Dispose();
                        else
                        {
                            // hervorheben
                            this.Model.Highlight(1f, 0, 0, 1f);
                        }

                        Model.CollisionGroup(0);
                        return true;
                    }
                }
            }
            return false;
        }
        
        /// <summary>
        /// Spielt einen Sound ab.
        /// </summary>
        private void PlaySound()
        {
            if (Model.Sound.Length > 0)
            {
                //SoundCollision.FilePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + Model.Sound;
                SoundCollision = new Sound.Sound(Model.Sound);
                if (Model.SoundVolume > 0) SoundCollision.Volume = Model.SoundVolume;
                SoundCollision.Play();
            }
        }

        /// <summary>
        /// head aus der Anzeige entfernen
        /// </summary>
        public void Dispose()
        {
            Model.Dispose();
        }
    }
}
