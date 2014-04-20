using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace Controller
{
    public class Object
    {
        public string ModelXmlPath {get; set; }
        public float Scale { get; set; }
        public float PosX { get; set; }
        public float PosY { get; set; }
        public float PosZ { get; set; }
        public bool AttachToCamera { get; set; }
        public float RotateHorizontal { get; set; }
        private Model model = null;

        public void Create(float z)
        {
            model.Create();
            model.Position(PosX, PosY, z + PosZ);
            model.Scale(Scale);
            model.AttachToCamera(AttachToCamera);
            model.Rotate(RotateHorizontal, 0, 1, 0);
        }

        public void Deserialize()
        {
            FileStream stream;
            stream = new FileStream(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + ModelXmlPath, FileMode.Open);
            XmlSerializer serializer = new XmlSerializer(typeof(Model));
            model = (Model)serializer.Deserialize(stream);
            stream.Close();
        }
    }
}
