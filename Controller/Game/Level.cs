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
    /// Übernimmt die Darstellung eines ganzen Levels
    /// </summary>
    public class Level
    {
        /// <summary>Name des Levels</summary>
        public string Name { get; set; }
        /// <summary>Schwierigkeitsgrad des Levels</summary>
        public string Difficulty { get; set; }
        /// <summary>Liste aller Levelsegmentes dieses Levels</summary>
        public List<LevelSegment> segments;
        /// <summary>Liste aller XML Pfade der Levelsegmente dieses Levels</summary>
        public List<string> segmentsXmlPath;
        /// <summary>Gesammtlänge des Levels</summary>
        public float LevelLength = 0;
        /// <summary>Anzahl zur verfügung stehendes Leben für dieses Level</summary>
        public uint Lives { get; set; }

        /// <summary>
        /// Level Initialisieren
        /// </summary>
        public Level()
        {
            segments = new List<LevelSegment>();
            segmentsXmlPath = new List<string>();
        }

        /// <summary>
        /// Level in der Anzeige erzeugen/laden
        /// </summary>
        /// <returns>Prüfung ob die Operation durchgeführt werden konnte</returns>
        public bool Load()
        {
            foreach (LevelSegment segment in segments)
            {
                if (!segment.Create(LevelLength)) return false;
                LevelLength += segment.Length;
            }
            return true;
        }

        /// <summary>
        /// Levelsegmente aus der XML erzeugen
        /// </summary>
        public void Deserialize()
        {
            FileStream stream;
            foreach (string segmentXmlPath in segmentsXmlPath)
            {
                stream = new FileStream(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + segmentXmlPath, FileMode.Open);
                XmlSerializer serializer = new XmlSerializer(typeof(LevelSegment));
                AddSegment((LevelSegment)serializer.Deserialize(stream));
                stream.Close();
            }
            
            foreach (LevelSegment segment in segments)
            {
                segment.Deserialize();
            }
        }

        /// <summary>
        /// Levelsegment hinzufügen
        /// </summary>
        /// <param name="segment"></param>
        public void AddSegment(LevelSegment segment)
        {
            segments.Add(segment);
        }

        /// <summary>
        /// Hinzufügen eines XML Datei eines Levelsegmentes
        /// </summary>
        /// <param name="path"></param>
        public void AddXmlPath(string path)
        {
            segmentsXmlPath.Add(path);
        }

        /// <summary>
        /// Komplettes Level aus der Anzeige entfernen
        /// </summary>
        public void Dispose()
        {
            foreach (LevelSegment segment in segments)
            {
                segment.Dispose();
            }
        }
    }
}
