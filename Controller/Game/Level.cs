using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace Controller
{
    public class Level
    {
        public string Name { get; set; }
        public string Difficulty { get; set; }
        public List<LevelSegment> segments;
        public List<string> segmentsXmlPath;
        public float LevelLength = 0;

        public void Load()
        {
            foreach (LevelSegment segment in segments)
            {
                segment.Create(LevelLength);
                LevelLength -= segment.Length;
            }
        }

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

        public Level(){
            segments = new List<LevelSegment>();
            segmentsXmlPath = new List<string>();
        }

        public void AddSegment(LevelSegment segment)
        {
            segments.Add(segment);
        }

        public void AddXmlPath(string path)
        {
            segmentsXmlPath.Add(path);
        }    
    }
}
