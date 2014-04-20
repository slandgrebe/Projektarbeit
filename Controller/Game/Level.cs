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

        public void Load()
        {
            float startPos = 0;

            foreach (LevelSegment segment in segments)
            {
                segment.Create(startPos);
                startPos -= segment.Length;
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












        //public Model Wagon = new Model("Resource Files/Models/Wagon/Wagon.3ds", false);
        //public Model[] Rails = new Model[10];

        public Level(){
            segments = new List<LevelSegment>();
            segmentsXmlPath = new List<string>();

            View.Visualization.positionCamera(0, 1.5f, 0);
            /*for (int i = 0; i < 20; i++)
            {
                Rails[i] = new Model("Resource Files/Models/Rail/Rail.3ds", false);
                Rails[i].Scale(0.5f);
                Rails[i].Position(0, 0, 11.5f*i*-1);
            }*/

            /*Wagon.Position(0, -1.3f, -3.5f);
            Wagon.Scale(0.5f);
            View.Visualization.attachToCamera(Wagon.Id, true);
            View.Visualization.changeCameraSpeed(5f);
              segmentsXmlPath.Add(".xml");*/
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
