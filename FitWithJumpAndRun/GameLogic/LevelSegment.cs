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
        /// <summary>Länge des Levelsegmentes</summary>
        public float Length { get; set; }
        /// <summary>Schwierigkeitsgrad des Levelsegmentes</summary>
        public int Severity { get; set; }
        /// <summary>Absolute Startposition des Segmentes</summary>
        public float StartPosition { get; set; }

        /// <summary>
        /// Initialisierung des Levelsegments
        /// </summary>
        public LevelSegment()
        {
            obstacles = new List<JumpAndRun.Item.Object>();
            scores = new List<JumpAndRun.Item.Object>();
            objects = new List<JumpAndRun.Item.Object>();
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
    }
}
