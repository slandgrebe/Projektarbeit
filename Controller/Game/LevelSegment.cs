using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace Controller
{
    public class LevelSegment
    {
        public List<Object> obstacles;
        public List<Object> scores;
        public List<Object> objects;
        public float Length { get; set; }

        public LevelSegment()
        {
            obstacles = new List<Object>();
            scores = new List<Object>();
            objects = new List<Object>();
        }
        
        public void AddObstacle(Object obj)
        {
            obstacles.Add(obj);
        }

        public void AddScore(Object obj)
        {
            scores.Add(obj);
        }

        public void AddObject(Object obj)
        {
            objects.Add(obj);
        }

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

        /*
        public void Deserialize()
        {
            FileStream stream;
            foreach (string objectXmlPath in objectsXmlPath)
            {
                stream = new FileStream(@objectXmlPath, FileMode.Open);
                XmlSerializer serializer = new XmlSerializer(typeof(Object));
                AddObject((Object)serializer.Deserialize(stream));
            }
        }*/
    }
}
