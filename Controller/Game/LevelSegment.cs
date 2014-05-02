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
    /// Übernimmt die Darstellung eines Levelsegmentes
    /// </summary>
    public class LevelSegment
    {
        /// <summary>Liste aller Hinternissen</summary>
        public List<Object> obstacles;
        /// <summary>Liste aller Punkte</summary>
        public List<Object> scores;
        /// <summary>Liste aller neutralen Objekte</summary>
        public List<Object> objects;
        /// <summary>Länge des Levelsegmentes</summary>
        public float Length { get; set; }

        /// <summary>
        /// Initialisierung des Levelsegments
        /// </summary>
        public LevelSegment()
        {
            obstacles = new List<Object>();
            scores = new List<Object>();
            objects = new List<Object>();
        }
        
        /// <summary>
        /// Hinternis hinzufügen
        /// </summary>
        /// <param name="obj">Hinternisobjekt</param>
        public void AddObstacle(Object obj)
        {
            obstacles.Add(obj);
        }

        /// <summary>
        /// Punkte Hinzufügen
        /// </summary>
        /// <param name="obj">Punkteobjekt</param>
        public void AddScore(Object obj)
        {
            scores.Add(obj);
        }

        /// <summary>
        /// Neutrales Objekt hinzufügen
        /// </summary>
        /// <param name="obj">Neutrales Objekt</param>
        public void AddObject(Object obj)
        {
            objects.Add(obj);
        }

        /// <summary>
        /// Levelsegment in der Anzeige darstellen
        /// </summary>
        /// <param name="z"></param>
        public void Create(float z)
        {
            foreach (Object o in obstacles)
            {
                o.Create(z);
            }
            foreach (Object s in scores)
            {
                s.Create(z);
            }
            foreach (Object o in objects)
            {
                o.Create(z);
            }
        }

        /// <summary>
        /// Objekte dieses Levelsegment anhand der XML erzeugen
        /// </summary>
        public void Deserialize()
        {
            foreach (Object obstacle in obstacles)
            {
                obstacle.Deserialize();
            }

            foreach (Object score in scores)
            {
                score.Deserialize();
            }

            foreach (Object obj in objects)
            {
                obj.Deserialize();
            }
        }

        /// <summary>
        /// Levelsegment aus der Anzeige entfernen
        /// </summary>
        public void Dispose()
        {
            foreach (Object o in obstacles)
            {
                o.Dispose();
            }
            foreach (Object s in scores)
            {
                s.Dispose();
            }
            foreach (Object o in objects)
            {
                o.Dispose();
            }
        }
    }
}
