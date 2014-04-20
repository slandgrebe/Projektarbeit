using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Xml.Serialization;

namespace Controller
{
    class Game
    {
        private static Game instance;
        public Player Player { get; set; }
        public Level level = null;
        private bool loadet = false;

        public Game()
        {
            Player = new Player();
            Player.Scale = 0.5f;
            Player.Attach = true;
            /*
            Model model = new Model();
            model.Path = "Resource Files/Models/Rail/Rail.3ds";
            model.ScalingNormalized = false;
            
            Object o1 = new Object();
            o1.ModelXmlPath = ".xml";
            o1.Scale = 0.5f;
            o1.PosX = 0;
            o1.PosY = 0;
            o1.PosZ = 11.5f*-1;
            o1.AttachToCamera = false;
            
            LevelSegment seg = new LevelSegment();
            seg.AddObject(o1);
            
            */
            /*
            level = new Level();
            level.Name = "Jungle";
            level.Difficulty = "Easy";
            level.AddXmlPath("Resource Files/Levels/Jungle/Models/Rail.xml");

            FileStream stream;
            stream = new FileStream(@"C:\Users\tobia_000\Documents\Level.xml", FileMode.Create);
            XmlSerializer serializer = new XmlSerializer(typeof(Level));
            serializer.Serialize(stream, level);
            stream.Close();
            */
            
            string dir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            dir = dir + @"\Resource Files\Levels\Jungle\Level.xml";

            FileStream stream;
            stream = new FileStream(dir, FileMode.Open);
            XmlSerializer serializer = new XmlSerializer(typeof(Level));
            level = (Level)serializer.Deserialize(stream);
            level.Deserialize();
            level.Load();
            loadet = true;
            View.Visualization.changeCameraSpeed(5f);
        }

        /// <summary>
        /// stellt sicher, dass diese Klasse nur einmal Instanziert wird.
        /// </summary>
        /// <returns>instance der Klasse Position</returns>
        public static Game Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Game();
                }
                return instance;
            }
        }

        public void Update()
        {
            if (loadet)
            {
                Player.Update();
            }
        }
    }


}
