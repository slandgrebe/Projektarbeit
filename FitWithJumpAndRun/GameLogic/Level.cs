using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace JumpAndRun.GameLogic
{
    /// <summary>
    /// Übernimmt die Darstellung eines ganzen Levels
    /// </summary>
    public class Level
    {
        /// <summary>Name des Levels</summary>
        public string Name { get; set; }
        /// <summary>Liste aller Levelsegmentes dieses Levels</summary>
        public List<LevelSegment> segments;
        /// <summary>Start und Endsegment dieses Levels</summary>
        private List<LevelSegment> segmentsStartEnd;
        /// <summary>Liste aller einfachen Levelsegmenten dieses Levels</summary>
        private List<LevelSegment> segmentsEasy;
        /// <summary>Liste aller mittleren Levelsegmenten dieses Levels</summary>
        private List<LevelSegment> segmentsMedium;
        /// <summary>Liste aller schweren Levelsegmenten dieses Levels</summary>
        private List<LevelSegment> segmentsHard;
        /// <summary>XML Pfad zum Startsegment</summary>
        public string StartSegmentXml { get; set; }
        /// <summary>XML Pfad zum Endsegment</summary>
        public string EndSegmentXml {get; set;}
        /// <summary>Liste aller XML Pfade der Levelsegmente dieses Levels</summary>
        public List<string> segmentsXmlPath;
        /// <summary>Gesammtlänge des Levels</summary>
        public float Length = 0;
        /// <summary>Anzahl zur verfügung stehendes Leben für dieses Level</summary>
        public uint Lifes { get; set; }
        /// <summary>Anzahl zur verfügung stehendes Leben für dieses Level</summary>
        public int SegmentNumber { get; set; }
        /// <summary>Hintergrundmusik für einfaches Level</summary>
        public string BackgroundMusicEasy { get; set; }
        /// <summary>Hintergrundmusik für mittelers Level</summary>
        public string BackgroundMusicMedium { get; set; }
        /// <summary>Hintergrundmusik für schweres Level</summary>
        public string BackgroundMusicHard { get; set; }

        /// <summary>
        /// Level Initialisieren
        /// </summary>
        public Level()
        {
            segments = new List<LevelSegment>();
            segmentsStartEnd = new List<LevelSegment>();
            segmentsEasy = new List<LevelSegment>();
            segmentsHard = new List<LevelSegment>();
            segmentsXmlPath = new List<string>();
        }

        /// <summary>
        /// Level in der Anzeige erzeugen/laden
        /// </summary>
        /// <param name="severity">Schwierigkeitsindikator</param>
        /// <returns>Prüfung ob die Operation durchgeführt werden konnte</returns>
        public bool Load(int severity)
        {
            int severityInt = 0;
            Random rnd = new Random();

            // Berechnen der noch zur verfügung stehender schwierigkeitsgrad
            for (int i = SegmentNumber; i == 0; i--)
            {
                severityInt = (int)severity / i;
                if (severityInt > 3) severityInt = 3;
                segments.Add(GetRandomSegment(rnd.Next(1, severityInt)));
            }

            ShuffleSegments();

            if (!segmentsStartEnd[0].Create(Length)) return false;
            Length += segmentsStartEnd[0].Length;

            foreach (LevelSegment segment in segments)
            {
                if (!segment.Create(Length)) return false;
                Length += segment.Length;
            }

            if (!segmentsStartEnd[1].Create(Length)) return false;
            Length += segmentsStartEnd[1].Length;

            return true;
        }

        /// <summary>
        /// Level aus der XML erzeugen
        /// </summary>
        public void Deserialize()
        {
            LevelSegment ls;
            
            segmentsStartEnd.Add(DeserializeSegment(StartSegmentXml));
            segmentsStartEnd.Add(DeserializeSegment(EndSegmentXml));
            foreach (string segmentXmlPath in segmentsXmlPath)
            {
                ls = DeserializeSegment(segmentXmlPath);
                switch (ls.Severity.ToString())
                {
                    case "1":
                        segmentsEasy.Add(ls);
                        break;
                    case "2":
                        segmentsMedium.Add(ls);
                        break;
                    case "3":
                        segmentsHard.Add(ls);
                        break;
                    default:
                        ls.Severity = 3;
                        segmentsHard.Add(ls);
                        break;
                }
            }
            
            foreach (LevelSegment segment in segments)
            {
                segment.Deserialize();
            }
        }

        /// <summary>
        /// Levelsegmente aus XML erzeugen
        /// </summary>
        /// <param name="filePath">Pfad zum XML</param>
        /// <retunrs>Levelsegment</retunrs>
        private LevelSegment DeserializeSegment(string filePath)
        {
            FileStream stream;
            LevelSegment ls;

            stream = new FileStream(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + filePath, FileMode.Open);
            XmlSerializer serializer = new XmlSerializer(typeof(LevelSegment));
            ls = (LevelSegment)serializer.Deserialize(stream);
            stream.Close();
            return ls;
        }

        /// <summary>
        /// Gibt ein zufälliges Segment zurück
        /// </summary>
        /// <param name="severity">Schwierigkeitsgrad</param>
        /// <returns>Levelsegment</returns>
        private LevelSegment GetRandomSegment(int severity)
        {
            Random rnd = new Random();
            int i = 0;

            switch (severity.ToString())
            {
                case "1":
                    i = rnd.Next(0, segmentsEasy.Count);
                    return segmentsEasy[i];
                case "2":
                    i = rnd.Next(0, segmentsMedium.Count);
                    return segmentsMedium[i];
                case "3":
                    i = rnd.Next(0, segmentsHard.Count);
                    return segmentsHard[i];
                default:
                    i = rnd.Next(0, segmentsHard.Count);
                    return segmentsHard[i];
            }
        }

        /// <summary>
        /// Mischelt die Segmente
        /// </summary>
        private void ShuffleSegments()
        {
            List<LevelSegment> randomList = new List<LevelSegment>();

            Random r = new Random();
            int randomIndex = 0;

            while (segments.Count > 0)
            {
                randomIndex = r.Next(0, segments.Count);
                randomList.Add(segments[randomIndex]);
                segments.RemoveAt(randomIndex);
            }

            segments = randomList;
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
