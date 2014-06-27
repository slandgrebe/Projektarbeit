using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace JumpAndRun.GameLogic
{
    /// <summary>
    /// Übernimmt die Darstellung eines Levelsegmentes
    /// </summary>
    public class LevelSegment
    {
        /// <summary>Liste aller Hinternissen</summary>
        public List<JumpAndRun.Item.Object> obstacles;
        /// <summary>Liste aller Punkte</summary>
        public List<JumpAndRun.Item.Object> scores;
        /// <summary>Liste aller neutralen Objekte</summary>
        public List<JumpAndRun.Item.Object> objects;
        /// <summary>Länge des Levelsegmentes (in meter)</summary>
        public float Length { get; set; }
        /// <summary>Schwierigkeitsgrad des Levelsegmentes</summary>
        public int Severity { get; set; }
        /// <summary>Absolute Startposition des Segmentes</summary>
        public float StartPosition { get; set; }
        public string FilePath { get; set; }
        /// <summary>
        /// Sound der beim Betreten dieses Segments abgespielt werden soll
        /// </summary>
        public string SoundOnEnter { get; set; }

        /// <summary>
        /// Delegate für Entered Event
        /// </summary>
        /// <param name="segment">Das auslösende Segment</param>
        public delegate void Entered(LevelSegment segment);
        /// <summary>
        /// Entered Event
        /// </summary>
        public event Entered EnteredEvent;
        /// <summary>
        /// Delegate für Exited Event
        /// </summary>
        /// <param name="segment">Das auslösende Segment</param>
        public delegate void Exited(LevelSegment segment);
        /// <summary>
        /// Exit Event
        /// </summary>
        public event Exited ExitedEvent;
        private enum State { InFront, Inside, Behind };
        private State state = State.InFront;
        private bool Visible = false;

        /// <summary>
        /// Initialisierung des Levelsegments
        /// </summary>
        public LevelSegment()
        {
            obstacles = new List<JumpAndRun.Item.Object>();
            scores = new List<JumpAndRun.Item.Object>();
            objects = new List<JumpAndRun.Item.Object>();

            Player.Instance.MovedEvent += new Player.Moved(PlayerMoved);

            SoundOnEnter = "";
        }
        
        /// <summary>
        /// Hinternis hinzufügen
        /// </summary>
        /// <param name="obj">Hinternisobjekt</param>
        public void AddObstacle(JumpAndRun.Item.Object obj)
        {
            obstacles.Add(obj);
        }

        /// <summary>
        /// Punkte Hinzufügen
        /// </summary>
        /// <param name="obj">Punkteobjekt</param>
        public void AddScore(JumpAndRun.Item.Object obj)
        {
            scores.Add(obj);
        }

        /// <summary>
        /// Neutrales Objekt hinzufügen
        /// </summary>
        /// <param name="obj">Neutrales Objekt</param>
        public void AddObject(JumpAndRun.Item.Object obj)
        {
            objects.Add(obj);
        }

        /// <summary>
        /// Levelsegment in der Anzeige darstellen
        /// </summary>
        /// <param name="z">Relativer 0 Punkt in der Z Koordinate</param>
        /// <returns>Prüfung ob die Operation durchgeführt werden konnte</returns>
        public bool Create(float z)
        {
            StartPosition = z;
            foreach (JumpAndRun.Item.Object o in obstacles)
            {
                if (!o.Create(StartPosition)) return false;
                o.Model.CollisionGroup(2);
            }
            foreach (JumpAndRun.Item.Object s in scores)
            {
                if (!s.Create(StartPosition)) return false;
                 s.Model.CollisionGroup(3);
            }
            foreach (JumpAndRun.Item.Object o in objects)
            {
                if (!o.Create(StartPosition)) return false;
            }

            // alles ausblenden am Anfang
            Visibility(false);

            return true;
        }

        /// <summary>
        /// Objekte dieses Levelsegment anhand der XML erzeugen
        /// </summary>
        public void Deserialize()
        {
            foreach (JumpAndRun.Item.Object o in obstacles)
            {
                o.Deserialize();
                o.Model.CollisionGroup(2);
            }

            foreach (JumpAndRun.Item.Object s in scores)
            {
                s.Deserialize();
                s.Model.CollisionGroup(3);
            }

            foreach (JumpAndRun.Item.Object o in objects)
            {
                o.Deserialize();
            }
        }

        /// <summary>
        /// Objekete eines Segmentes anzeigen oder ausblenden
        /// </summary>
        /// <param name="visible">Sichtbarkeit</param>
        /// <returns>Prüfung ob die Operation durchgeführt werden konnte</returns>
        public bool Visibility(bool visible)
        {
            Visible = visible;
            foreach (JumpAndRun.Item.Object o in obstacles)
            {
                if (!o.Model.Visibility(visible)) return false;
            }
            foreach (JumpAndRun.Item.Object s in scores)
            {
                if (!s.Model.Visibility(visible)) return false; 
            }
            foreach (JumpAndRun.Item.Object o in objects)
            {
                if (!o.Model.Visibility(visible)) return false;
            }
            return true;
        }

        /// <summary>
        /// Levelsegment aus der Anzeige entfernen
        /// </summary>
        public void Dispose()
        {
            foreach (JumpAndRun.Item.Object o in obstacles)
            {
                o.Dispose();
            }
            foreach (JumpAndRun.Item.Object s in scores)
            {
                s.Dispose();
            }
            foreach (JumpAndRun.Item.Object o in objects)
            {
                o.Dispose();
            }
        }

        /// <summary>
        /// Event Listener für das Player Moved Event
        /// </summary>
        /// <param name="z">z Kooridnate des Player</param>
        public void PlayerMoved(float z)
        {
            // Nur relevant, wenn das Segment auch sichtbar ist
            if (!Visible)
            {
                return;
            }
            switch (state)
            {
                // bereits passiert
                case State.Behind:
                    break;
                // testen ob betreten
                case State.InFront:
                    if (IsInside(z))
                    {
                        HandleEntered();
                    }
                    break;
                // testen ob verlassen
                case State.Inside:
                    if (!IsInside(z))
                    {
                        state = State.Behind;
                        if (ExitedEvent != null)
                        {
                            ExitedEvent(this);
                        }
                    }
                    break;
            }
        }

        private void HandleEntered()
        {
            state = State.Inside;
            if (EnteredEvent != null)
            {
                EnteredEvent(this);

                // Sound abspielen
                if (!SoundOnEnter.Equals(""))
                {
                    Sound.Sound sound = new Sound.Sound();
                    sound.FilePath = SoundOnEnter;
                    sound.Play();
                }
            }
        }

        /// <summary>
        /// Findet heraus, ob die z Koordinate innerhalb dieses Segments ist
        /// </summary>
        /// <param name="z">zu prüfende z-Koordinate</param>
        /// <returns>Resultat</returns>
        private bool IsInside(float z)
        {
            if (z >= StartPosition && z < (StartPosition + Length))
            {
                return true;
            }
            return false;
        }
    }
}
