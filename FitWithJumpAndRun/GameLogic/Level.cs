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
        /// <summary>Pfad zum Ordner der Segmente dieses Levels</summary>
        public string SegmentsXmlPath;
        /// <summary>Gesammtlänge des Levels in Meter</summary>
        public float Length = 0;
        /// <summary>Anzahl zur verfügung stehendes Leben für dieses Level</summary>
        public uint Lifes { get; set; }
        /// <summary>Mindestdauer des Levels in Sekunden</summary>
        public int LevelDuration { get; set; }
        /// <summary>
        /// Geschwindigkeit in m/s
        /// </summary>
        public double Speed { get; set; }
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
            //SegmentsXmlPath = new List<string>();
            Speed = 5;
        }

        /// <summary>
        /// Level in der Anzeige erzeugen/laden
        /// </summary>
        /// <param name="severity">Schwierigkeitsgrad</param>
        /// <returns>Prüfung ob die Operation durchgeführt werden konnte</returns>
        public bool Load()
        {
            // min. 1 Segment muss vorhanden sein
            if (Segments.Count <= 0)
            {
                return false;
            }

            List<LevelSegment> list = new List<LevelSegment>();

            // Zufällig Segmente anhand der Schwierigkeit auswählen
            list = ChooseRandomSegments(Difficulty.Normal, LevelDuration, Speed);

            // ausgewählte Segemente zufällig aneinanderreihen
            list = ShuffleSegments(SegmentsStartEnd[0], SegmentsStartEnd[1], list);

            foreach (LevelSegment segment in list)
            {
                LoadNextSegment(segment);
            }

            return true;
        }

        /// <summary>
        /// Ladet das nächste Segment
        /// </summary>
        /// <returns>Prüfung ob die Operation durchgeführt werden konnte</returns>
        public bool LoadNextSegment(LevelSegment segment)
        {
            /*//if (!Segments[LoadetSegments].Create(Length)) return false
            Segments[LoadetSegments].Create(Length);
            Length += Segments[LoadetSegments].Length;
            LoadetSegments++;
            return true;*/

            segment.Create(Length);
            Length += segment.Length;

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

            // alle xml Dateien im Ordner auslesen
            //string path = "data/levels/jungle/segments";
            var files = System.IO.Directory.GetFiles(SegmentsXmlPath, "*.xml");
            foreach (string file in files)
            {
                Console.WriteLine(file);
                Segments.Add(DeserializeSegment(file));
            }
            /*foreach (string segmentXmlPath in SegmentsXmlPath)
            {
                Segments.Add(DeserializeSegment(segmentXmlPath));
            }*/

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

            //stream = new FileStream(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + filePath, FileMode.Open);
            stream = new FileStream(filePath, FileMode.Open);
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
        private List<LevelSegment> ShuffleSegments(LevelSegment startSegment, LevelSegment endSegment, List<LevelSegment> segments)
        {
            List<LevelSegment> randomList = new List<LevelSegment>();

            Random r = new Random();
            int randomIndex = 0;

            randomList.Add(startSegment);

            while (segments.Count > 0)
            {
                randomIndex = r.Next(0, segments.Count);
                randomList.Add(segments[randomIndex]);
                segments.RemoveAt(randomIndex);
            }

            randomList.Add(endSegment);

            return randomList;
        }

        /// <summary>
        /// Wählt aus allen zur Verfügung stehenden Segmenten zufällige aus. Dabei wird der Schwierigkeitsgrad sowie die gewünschte Mindestlänge berücksichtigt.
        /// </summary>
        /// <param name="difficulty">Schwierigkeitsgrad</param>
        /// <param name="lengthInSeconds">Mindestlänge in Sekunden</param>
        /// <param name="speed">Geschwindigkeit in m/s</param>
        /// <returns>Liste der ausgewählten Segemente</returns>
        private List<LevelSegment> ChooseRandomSegments(JumpAndRun.Difficulty difficulty, int lengthInSeconds, double speed)
        {
            Console.WriteLine("new Random Distribution");
            double currentLength = 0; // in meter
            RandomNumberGenerator.SetSeedFromSystemTime();

            int minDifficulty = Segments.Min(e => e.Severity); // einfachste vorhandene Schwierigkeit
            int maxDifficulty = Segments.Max(e => e.Severity); // schwerste vorhandene Schwierigkeit


            double nDifficulty = 0; // schwierigkeit auf einer Skala von min - max
            switch (difficulty)
            {
                case Difficulty.Easy: nDifficulty = Math.Round((double)(maxDifficulty - minDifficulty) / 4) + minDifficulty; break; // 25%
                case Difficulty.Normal: nDifficulty = Math.Round((double)(maxDifficulty - minDifficulty) / 2) + minDifficulty; break; // 50%
                case Difficulty.Difficult: nDifficulty = Math.Round((double)(maxDifficulty - minDifficulty) / 4 * 3) + minDifficulty; break; // 75%
                default: nDifficulty = Math.Round((double)(maxDifficulty - minDifficulty) / 2) + minDifficulty; break;
            }

            double standardDeviation = (maxDifficulty - minDifficulty) / 4; // Standardabweichung
            
            // bei nur einem segment...
            if (standardDeviation <= 0)
            {
                standardDeviation = 1;
            }

            List<LevelSegment> segmentList = new List<LevelSegment>();
            while(true)
            {
                // Zufallszahl nach Gaussscher Normalverteilung mit nDifficulty als Durchschnitt
                double d = (int)Math.Round(RandomNumberGenerator.GetNormal(nDifficulty, standardDeviation));
                
                // min 1, max 10
                if (d < 1.0) d = 1;
                else if (d > 10) d = 10;

                // Segment welches dieser Schwierigkeit am ehesten entspricht
                LevelSegment segment = GetSegmentWithDifficulty(d);

                segmentList.Add(segment);

                // so lange Segmente hinzufügen, bis die gewünschte Länge erreicht wird.
                if ((currentLength + segment.Length) / speed < lengthInSeconds)
                {
                    currentLength += segment.Length;
                }
                else
                {
                    break;
                }     
            }

            return segmentList;
        }
        private LevelSegment GetSegmentWithDifficulty(double difficulty)
        {
            double smallesDifference = 100000;
            List<LevelSegment> list = new List<LevelSegment>();

            foreach (LevelSegment segment in Segments)
            {
                double currentDifference = Math.Abs(difficulty - (double)segment.Severity);
                if (currentDifference < smallesDifference)
                {
                    smallesDifference = currentDifference;
                    list.Add(segment);
                }
                else if (currentDifference == smallesDifference)
                {
                    list.Add(segment);
                }
            }

            double rnd = RandomNumberGenerator.GetUniform(); // zufallszahl 0-1
            double choice = Math.Round(rnd * (list.Count - 1)); // zufallszahl * Anzahl gefundene Segemente -1 (Index fängt bei 0 an)
            return list[(int)choice]; // Segement zurück liefern
        }

        /// <summary>
        /// Hinzufügen eines XML Datei eines Levelsegmentes
        /// </summary>
        /// <param name="path"></param>
        /*public void AddXmlPath(string path)
        {
            SegmentsXmlPath.Add(path);
        }*/

        public void playBackgroundMusic()
        {
            switch (Severity.ToString())
            {
                case "1":
                    //BgSound.FilePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + BackgroundMusicEasy;
                    BgSound.FilePath = BackgroundMusicEasy;
                    BgSound.Volume = BackgroundMusicEasyVolume;
                    break;
                case "2":
                    //BgSound.FilePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + BackgroundMusicMedium;
                    BgSound.FilePath = BackgroundMusicMedium;
                    BgSound.Volume = BackgroundMusicMediumVolume;
                    break;
                case "3":
                    //BgSound.FilePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + BackgroundMusicHard;
                    BgSound.FilePath = BackgroundMusicHard;
                    BgSound.Volume = BackgroundMusicHardVolume;
                    break;
                default:
                    //BgSound.FilePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + BackgroundMusicHard;
                    BgSound.FilePath = BackgroundMusicHard;
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
        /// Objekete eines Levels anzeigen oder ausblenden
        /// </summary>
        /// <param name="visible">Sichtbarkeit</param>
        /// <returns>Prüfung ob die Operation durchgeführt werden konnte</returns>
        public bool Visibility(bool visible)
        {
            foreach (LevelSegment segment in Segments)
            {
                if (!segment.Visibility(visible)) return false;
            }
            return true;
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
