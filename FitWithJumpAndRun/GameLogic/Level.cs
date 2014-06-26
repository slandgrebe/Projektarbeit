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
        private List<LevelSegment> AllAvailableSegments;
        /// <summary>
        /// Alle zufällig ausgewählten Segmente für diese Runde
        /// </summary>
        public List<LevelSegment> RandomlyChosenSegments;
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
        private JumpAndRun.Difficulty difficulty = JumpAndRun.Difficulty.NotSelected;
        /// <summary>Hintergrundmusik Soundobjekt</summary>
        private Sound.Sound BgSound = new Sound.Sound();
        private List<LevelSegment> list;

        /// <summary>
        /// Level Initialisieren
        /// </summary>
        public Level()
        {
            AllAvailableSegments = new List<LevelSegment>();
            RandomlyChosenSegments = new List<LevelSegment>();
            SegmentsStartEnd = new List<LevelSegment>();
            list = new List<LevelSegment>();
            //SegmentsXmlPath = new List<string>();
            Speed = 5;
        }

        /// <summary>
        /// Level in der Anzeige erzeugen/laden
        /// </summary>
        /// <param name="severity">Schwierigkeitsgrad</param>
        /// <returns>Prüfung ob die Operation durchgeführt werden konnte</returns>
        public bool Load(JumpAndRun.Difficulty difficulty)
        {
            // min. 1 Segment muss vorhanden sein
            if (AllAvailableSegments.Count < 1 || SegmentsStartEnd.Count < 2)
            {
                return false;
            }

            this.difficulty = difficulty;

            //List<LevelSegment> list = new List<LevelSegment>();

            // Zufällig Segmente anhand der Schwierigkeit auswählen
            RandomlyChosenSegments = ChooseRandomSegments(difficulty, LevelDuration, Speed);

            // ausgewählte Segemente zufällig aneinanderreihen
            RandomlyChosenSegments = ShuffleSegments(SegmentsStartEnd[0], SegmentsStartEnd[1], RandomlyChosenSegments);

            // Segmente ins Spiel laden
            foreach (LevelSegment segment in RandomlyChosenSegments)
            {
                if (!LoadNextSegment(segment))
                {
                    return false;
            }
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

            if (!segment.Create(Length))
            {
                return false;
            }

            Length += segment.Length;

            return true;
        }

        /*/// <summary>
        /// Entfernt das nächste Segment
        /// </summary>
        public void DisposeNextSegment()
        {
            AllAvailableSegments[DisposedSegments].Dispose();
            DisposedSegments++;
        }*/

        /// <summary>
        /// Level aus der XML erzeugen
        /// </summary>
        public void Deserialize()
        {
            // Start und Ende Segmente
            SegmentsStartEnd.Add(DeserializeSegment(StartSegmentXml));
            SegmentsStartEnd.Add(DeserializeSegment(EndSegmentXml));

            foreach (LevelSegment segment in SegmentsStartEnd)
            {
                segment.Deserialize();
            }

            // Segmente: alle xml Dateien im Ordner auslesen
            var files = System.IO.Directory.GetFiles(SegmentsXmlPath, "*.xml");
            foreach (string file in files)
            {
                Console.WriteLine(file);
                AllAvailableSegments.Add(DeserializeSegment(file));
            }

            // Segmente erstellen
            foreach (LevelSegment segment in AllAvailableSegments)
            {
                segment.Deserialize();
            }
            }

        /// <summary>
        /// Levelsegmente aus XML erzeugen
        /// </summary>
        /// <param name="filePath">Pfad zum XML des Segments</param>
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
            ls.FilePath = filePath;
            return ls;
        }

        /*/// <summary>
        /// Gibt ein zufälliges Segment zurück
        /// </summary>
        /// <returns>Levelsegment</returns>
        private LevelSegment GetRandomSegment()
        {
            Random rnd = new Random();
            int i = 0;

            i = rnd.Next(0, AllAvailableSegments.Count);
            return AllAvailableSegments[i];
        }*/

        /// <summary>
        /// Mischelt die Segmente
        /// </summary>
        /// <param name="startSegment">Fügt das Startsegment am Anfang ein</param>
        /// <param name="endSegment">Fügt das Endsegment am Ende an</param>
        private List<LevelSegment> ShuffleSegments(LevelSegment startSegment, LevelSegment endSegment, List<LevelSegment> segments)
        {
            List<LevelSegment> randomList = new List<LevelSegment>();

            // Zufallszahl generieren
            Random r = new Random();
            int randomIndex = 0;

            // Start hinzufügen
            randomList.Add(startSegment);

            // Segmente aus der übergebenen Liste zufällig auswählen
            while (segments.Count > 0)
            {
                randomIndex = r.Next(0, segments.Count);
                randomList.Add(segments[randomIndex]);
                segments.RemoveAt(randomIndex);
            }

            // Ende hinzufügen
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

            int minDifficulty = AllAvailableSegments.Min(e => e.Severity); // einfachste vorhandene Schwierigkeit
            int maxDifficulty = AllAvailableSegments.Max(e => e.Severity); // schwerste vorhandene Schwierigkeit

            double nDifficulty = 0; // schwierigkeit auf einer Skala von min - max
            switch (difficulty)
            {
                case JumpAndRun.Difficulty.Easy: nDifficulty = Math.Round((double)(maxDifficulty - minDifficulty) / 4) + minDifficulty; break; // 25%
                case JumpAndRun.Difficulty.Normal: nDifficulty = Math.Round((double)(maxDifficulty - minDifficulty) / 2) + minDifficulty; break; // 50%
                case JumpAndRun.Difficulty.Difficult: nDifficulty = Math.Round((double)(maxDifficulty - minDifficulty) / 4 * 3) + minDifficulty; break; // 75%
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
                
                // min und max kontrollieren
                if (d < minDifficulty) d = minDifficulty;
                else if (d > maxDifficulty) d = maxDifficulty;

                // Segment welches dieser Schwierigkeit am ehesten entspricht
                LevelSegment segment = DeserializeSegment(GetSegmentWithDifficulty(d).FilePath);
                segment.Deserialize();

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

        /// <summary>
        /// Wählt aus allen zur Verfügung stehenden Segmenten ein Segement aus, welches der angegebenen Schwierigkeit am ehesten entspricht
        /// </summary>
        /// <param name="difficulty">Gewünschte Schwierigkeit</param>
        /// <returns>ausgewähltes Segment</returns>
        private LevelSegment GetSegmentWithDifficulty(double difficulty)
        {
            double smallestDifference = 100000;
            List<LevelSegment> list = new List<LevelSegment>();

            foreach (LevelSegment segment in AllAvailableSegments)
            {
                double currentDifference = Math.Abs(difficulty - (double)segment.Severity);
                if (currentDifference < smallestDifference)
                {
                    smallestDifference = currentDifference;
                    list.Add(segment);
                }
                else if (currentDifference == smallestDifference)
                {
                    list.Add(segment);
                }
            }

            double rnd = RandomNumberGenerator.GetUniform(); // zufallszahl 0-1
            double choice = Math.Round(rnd * (list.Count - 1)); // zufallszahl * Anzahl gefundene Segemente -1 (Index fängt bei 0 an)
            return list[(int)choice]; // Segement zurück liefern
        }

        /*/// <summary>
        /// Hinzufügen eines XML Datei eines Levelsegmentes
        /// </summary>
        /// <param name="path"></param>
        public void AddXmlPath(string path)
        {
            SegmentsXmlPath.Add(path);
        }*/

        /// <summary>
        /// Hintergrundmusik abspielen
        /// </summary>
        public void playBackgroundMusic()
        {
            switch (difficulty)
            {
                case JumpAndRun.Difficulty.Easy:
                    //BgSound.FilePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + BackgroundMusicEasy;
                    BgSound.FilePath = BackgroundMusicEasy;
                    BgSound.Volume = BackgroundMusicEasyVolume;
                    break;
                case JumpAndRun.Difficulty.Normal:
                    //BgSound.FilePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + BackgroundMusicMedium;
                    BgSound.FilePath = BackgroundMusicMedium;
                    BgSound.Volume = BackgroundMusicMediumVolume;
                    break;
                case JumpAndRun.Difficulty.Difficult:
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

        /// <summary>
        /// Hintergrundmusik stoppen
        /// </summary>
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
            foreach (LevelSegment segment in list)
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
            foreach (LevelSegment segment in AllAvailableSegments)
            {
                segment.Dispose();
            }
        }
    }
}
