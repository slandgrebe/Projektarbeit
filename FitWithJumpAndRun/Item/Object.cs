using System;
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
        /// <summary>Skalierung des Objekts</summary>
        public float Scale { get; set; }
        /// <summary>Positionierung X Koordinate des Objekts</summary>
        public float PosX { get; set; }
        /// <summary>Positionierung Y Koordinate des Objekts</summary>
        public float PosY { get; set; }
        /// <summary>Positionierung Z Koordinate des Objekts</summary>
        public float PosZ { get; set; }
        /// <summary>Objekt der Kamera anhängen</summary>
        public bool AttachToCamera { get; set; }
        /// <summary>Winkel in Grad, um welches das Objekt Horizontal gedreht werden soll</summary>
        public float RotateHorizontal { get; set; }
        /// <summary>Winkel in Grad, um welches das Objekt Vertical gedreht werden soll</summary>
        public float RotateVertical { get; set; }
        /// <summary>Schwiergikeitsgrad des Objektes</summary>
        public int Severity { get; set; }
        /// <summary>Beinhaltet das head</summary>
        public Model Model { get; set; }
        /// <summary>Soundobjekt</summary>
        private Sound.Sound SoundCollision = new Sound.Sound();

        /// <summary>
        /// Initialisierung des Objektes.
        /// </summary>
        public Object()
        {
            Scale = 1;
            PosX = 0;
            PosY = 0;
            PosZ = 0;
            AttachToCamera = false;
            RotateHorizontal = 0;
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
            if (!Model.Rotate(RotateHorizontal, 0, 1, 0)) return false;
            //if (!Model.Rotate(RotateVertical, 1, 0, 0)) return false;
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
            if (!Model.Rotate(RotateHorizontal, 0, 1, 0)) return false;
            //if (!Model.Rotate(RotateVertical, 1, 0, 0)) return false;
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
            if (!Model.Rotate(RotateHorizontal, 0, 1, 0)) return false;
            //if (!Model.Rotate(RotateVertical, 1, 0, 0)) return false;
            return true;
        }

        /// <summary>
        /// head anhand des XML erzeugen
        /// </summary>
        public void Deserialize()
        {
            FileStream stream;
            stream = new FileStream(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + ModelXmlPath, FileMode.Open);
            XmlSerializer serializer = new XmlSerializer(typeof(Model));
            Model = (Model)serializer.Deserialize(stream);
            stream.Close();
        }

        /// <summary>
        /// Überprüft, ob das Objekt mit der Spielfigur Kollidiert
        /// </summary>
        /// <param name="player">Spielfigur</param>
        /// <param name="dispose">Modell soll nach der Kollision gelöscht werden</param>
        /// <returns></returns>
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
                    if (!player.Colidet.Contains(Model.Id))
                    {
                        player.Colidet.Add(Model.Id);
                        PlaySound();
                        if (dispose) Dispose();
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
                SoundCollision.FilePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + Model.Sound;
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
