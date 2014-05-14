using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace Controller
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
        /// <summary>Beinhaltet das Model</summary>
        public Model Model { get; set; }

        /// <summary>
        /// Erstellt das Objekt in der Anzeige
        /// </summary>
        /// <param name="z">Relativer 0 Punkt in der Z Koordinate</param>
        /// <returns>Prüfung ob die Operation durchgeführt werden konnte</returns>
        public bool Create(float z)
        {
            if (!Model.Create()) return false;
            if (!Model.Position(PosX, PosY, z + PosZ *-1)) return false;
            if (!Model.Scale(Scale)) return false;
            if (!Model.AttachToCamera(AttachToCamera)) return false;
            if (!Model.Rotate(RotateHorizontal, 0, 1, 0)) return false;
            return true;
        }

        /// <summary>
        /// Model anhand des XML erzeugen
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
        public bool handleCollisions(Player player, bool dispose)
        {
            // COLLISION DETECTION
            System.Collections.Generic.Dictionary<uint, System.Collections.Generic.List<uint>> collisionList = new System.Collections.Generic.Dictionary<uint, System.Collections.Generic.List<uint>>();

            uint length = View.Model.CollisionsTextLength();
            System.Text.StringBuilder str = new System.Text.StringBuilder((int)length + 1);
            View.Model.CollisionsText(str, str.Capacity); // Daten aus DLL holen
            // str: 1:2,3;2:1,2 => 1 kollidiert mit 2 und 3. 2 kollidiert mit 1 und 2.
            String collisionsString = str.ToString(); // Daten parsen
            string[] models = collisionsString.Split(';');
            foreach (string model in models)
            {
                // model: 1:2,3
                string[] parts = model.Split(':');
                if (parts.Length != 2) continue; // Fehlerbehandlung: tritt beim schliessen des Fensters auf

                string aModelId = parts[0].ToString(); // modelId

                // wer kollidierte alles mit diesem Model?
                System.Collections.Generic.List<uint> collideeList = new System.Collections.Generic.List<uint>();

                if (parts[1] != "")
                {
                    string[] collidees = parts[1].Split(',');

                    foreach (string collidee in collidees)
                    {
                        if (collidee != "")
                        {
                            collideeList.Add(Convert.ToUInt32(collidee));
                        }
                    }
                }
                collisionList.Add(Convert.ToUInt32(aModelId), collideeList);
            }

            // Liste abarbeiten
            if (collisionList.ContainsKey(Model.Id))
            {
                System.Collections.Generic.List<uint> myCollisions = collisionList[Model.Id];
                if (myCollisions.Contains(player.Head.Id) ||
                    myCollisions.Contains(player.Torso.Id) ||
                    myCollisions.Contains(player.UpperarmLeft.Id) ||
                    myCollisions.Contains(player.UpperarmRight.Id) ||
                    myCollisions.Contains(player.ForearmLeft.Id) ||
                    myCollisions.Contains(player.ForearmRight.Id) ||
                    myCollisions.Contains(player.ThighlegLeft.Id) ||
                    myCollisions.Contains(player.ThighlegRight.Id) ||
                    myCollisions.Contains(player.LowerlegLeft.Id) ||
                    myCollisions.Contains(player.LowerlegRight.Id)) // es gibt kollisionen => das Objekt selber behandeln
                {
                    if (!player.Colidet.Contains(Model.Id))
                    {
                        player.Colidet.Add(Model.Id);
                        if (dispose)
                        {
                            View.Model.Dispose(Model.Id);
                        }
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Model aus der Anzeige entfernen
        /// </summary>
        public void Dispose()
        {
            Model = null;
        }
    }
}
