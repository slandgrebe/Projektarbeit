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
        public List<LevelSegment> Segments;
        /// <summary>Start und Endsegment dieses Levels</summary>
        private List<LevelSegment> SegmentsStartEnd;
        /// <summary>XML Pfad zum Startsegment</summary>
        public string StartSegmentXml { get; set; }
        /// <summary>XML Pfad zum Endsegment</summary>
        public string EndSegmentXml {get; set;}
        /// <summary>Liste aller XML Pfade der Levelsegmente dieses Levels</summary>
        public List<string> SegmentsXmlPath;
        /// <summary>Gesammtlänge des Levels</summary>
        public float Length = 0;
        /// <summary>Anzahl zur verfügung stehendes Leben für dieses Level</summary>
        public uint Lifes { get; set; }
        /// <summary>Anzahl zur verfügung stehendes Leben für dieses Level</summary>
        public int LevelDuration { get; set; }
        /// <summary>Hintergrundmusik für einfaches Level</summary>
        public string BackgroundMusicEasy { get; set; }
        /// <summary>Hintergrundmusik für mittelers Level</summary>
        public string BackgroundMusicMedium { get; set; }
        /// <summary>Hintergrundmusik für schweres Level</summary>
        public string BackgroundMusicHard { get; set; }
        /// <summary>Hintergrundmusiklautstärke für einfaches Level</summary>
        public int BackgroundMusicEasyVolume { get; set; }
        /// <summary>Hintergrundmusiklautstärke für mittelers Level</summary>
        public int BackgroundMusicMediumVolume { get; set; }
        /// <summary>Hintergrundmusiklautstärke für schweres Level</summary>
        public int BackgroundMusicHardVolume { get; set; }
        /// <summary>Anzahl geladener Segmente</summary>
        private int LoadetSegments = 0;
        /// <summary>Anzahl gelöschter</summary>
        private int DisposedSegments = 0;
        /// <summary>Schwierigkeitsgrad</summary>
        public int Severity { get; set; }
        /// <summary>Hintergrundmusik Soundobjekt</summary>
        private Sound.Sound BgSound = new Sound.Sound();

        /// <summary>
        /// Level Initialisieren
        /// </summary>
        public Level()
        {
            Segments = new List<LevelSegment>();
            SegmentsStartEnd = new List<LevelSegment>();
            SegmentsXmlPath = new List<string>();
        }

        /// <summary>
        /// Level in der Anzeige erzeugen/laden
        /// </summary>
        /// <param name="severity">Schwierigkeitsgrad</param>
        /// <returns>Prüfung ob die Operation durchgeführt werden konnte</returns>
        public bool Load()
        {
            int severityPoints = 0;
            Random rnd = new Random();
            int min = 1;
            int max = 3;
            int segmentSeverity;

            // Berechnen der noch zur verfügung stehender schwierigkeitsgrade
            /*for (int i = LevelDuration; i == 0; i--)
            {
                segmentSeverity = rnd.Next(min, max);
                severityPoints -= segmentSeverity;
                Segments.Add(GetRandomSegment());
            }*/

            ShuffleSegments(SegmentsStartEnd[0], SegmentsStartEnd[1]);

            foreach (LevelSegment segment in Segments)
            {
                LoadNextSegment();
            }

            return true;
        }

        /// <summary>
        /// Ladet das nächste Segment
        /// </summary>
        /// <returns>Prüfung ob die Operation durchgeführt werden konnte</returns>
        public bool LoadNextSegment()
        {
            //if (!Segments[LoadetSegments].Create(Length)) return false
            Segments[LoadetSegments].Create(Length);
            Length += Segments[LoadetSegments].Length;
            LoadetSegments++;
            return true;
        }

        /// <summary>
        /// Entfernt das nächste Segment
        /// </summary>
        public void DisposeNextSegment()
        {
            Segments[DisposedSegments].Dispose();
            DisposedSegments++;
        }

        /// <summary>
        /// Level aus der XML erzeugen
        /// </summary>
        public void Deserialize()
        {
            SegmentsStartEnd.Add(DeserializeSegment(StartSegmentXml));
            SegmentsStartEnd.Add(DeserializeSegment(EndSegmentXml));
            foreach (string segmentXmlPath in SegmentsXmlPath)
            {
                Segments.Add(DeserializeSegment(segmentXmlPath));
            }

            foreach (LevelSegment segment in SegmentsStartEnd)
            {
                segment.Deserialize();
            }

            foreach (LevelSegment segment in Segments)
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
        /// <returns>Levelsegment</returns>
        private LevelSegment GetRandomSegment()
        {
            Random rnd = new Random();
            int i = 0;

            i = rnd.Next(0, Segments.Count);
            return Segments[i];
        }

        /// <summary>
        /// Mischelt die Segmente
        /// </summary>
        /// <param name="startSegment">Fügt das Startsegment am Anfang ein</param>
        /// <param name="endSegment">Fügt das Endsegment am Ende an</param>
        private void ShuffleSegments(LevelSegment startSegment, LevelSegment endSegment)
        {
            List<LevelSegment> randomList = new List<LevelSegment>();

            Random r = new Random();
            int randomIndex = 0;

            randomList.Add(startSegment);

            while (Segments.Count > 0)
            {
                randomIndex = r.Next(0, Segments.Count);
                randomList.Add(Segments[randomIndex]);
                Segments.RemoveAt(randomIndex);
            }

            randomList.Add(endSegment);

            Segments = randomList;
        }

        /// <summary>
        /// Hinzufügen eines XML Datei eines Levelsegmentes
        /// </summary>
        /// <param name="path"></param>
        public void AddXmlPath(string path)
        {
            SegmentsXmlPath.Add(path);
        }

        public void playBackgroundMusic()
        {
            switch (Severity.ToString())
            {
                case "1":
                    BgSound.FilePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + BackgroundMusicEasy;
                    BgSound.Volume = BackgroundMusicEasyVolume;
                    break;
                case "2":
                    BgSound.FilePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + BackgroundMusicMedium;
                    BgSound.Volume = BackgroundMusicMediumVolume;
                    break;
                case "3":
                    BgSound.FilePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + BackgroundMusicHard;
                    BgSound.Volume = BackgroundMusicHardVolume;
                    break;
                default:
                    BgSound.FilePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + BackgroundMusicHard;
                    BgSound.Volume = BackgroundMusicHardVolume;
                    break;
            }
            BgSound.Loop = true;
            BgSound.Play();
        }

        public void stopBackgroundMusic()
        {
            BgSound.FadeOut(1);
        }

        /// <summary>
        /// Komplettes Level aus der Anzeige entfernen
        /// </summary>
        public void Dispose()
        {
            foreach (LevelSegment segment in Segments)
            {
                segment.Dispose();
            }
        }
    }
}
