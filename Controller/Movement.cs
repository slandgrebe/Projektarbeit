using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using View;
using Model;

namespace Controller
{
    class Movement
    {
        /// <summary>Punkt ID Fuss links</summary>
        private uint pointFootLeft = 0;
        /// <summary>Punkt ID Fuss rechts</summary>
        private uint pointFootRight = 0;
        /// <summary>Punkt ID Hand links</summary>
        private uint pointHandLeft = 0;
        /// <summary>Punkt ID Hand rechts</summary>
        private uint pointHandRight = 0;
        /// <summary>Punkt ID Kopf</summary>
        private uint pointHead = 0;
        
        /// <summary>Linien ID Unterarm links</summary>
        //private int lineForearmLeft = 0;
        /// <summary>Linien ID Unterarm ¨rechts</summary>
        private uint lineForearmRight = 0;
        /// <summary>Linien ID Oberarm links</summary>
        //private int lineUpperarmLeft = 0;
        /// <summary>Linien ID Oberarm rechts</summary>
        private uint lineUpperarmRight = 0;
        /// <summary>Linien ID Oberschenkel links</summary>
        /*private int lineThighLeft = 0;
        /// <summary>Linien ID Oberschenkel rechts</summary>
        private int lineThighRight = 0;
        /// <summary>Linien ID Unterschenkel links</summary>
        private int lineLowerlegLeft = 0;
        /// <summary>Linien ID Unterschenkel rechts</summary>
        private int lineLowerlegRight = 0;
        /// <summary>Linien ID Hüfte links</summary>
        private int lineHipLeft = 0;
        /// <summary>Linien ID Hüfte rechts</summary>
        private int lineHipRight = 0;
        /// <summary>Linien ID Hüfte</summary>
        private int lineHipCenter = 0;
        /// <summary>Linien ID Brust links</summary>
        private int lineBreastLeft = 0;
        /// <summary>Linien ID Brust rechts</summary>
        private int lineBreastRight = 0;
        /// <summary>Linien ID Brust</summary>
        private int lineBreastCenter = 0;
        */

        private uint hand = 0;
        private uint start = 0;
        /// <summary>Objekt mit den Koordinaten der getrackten Körperpunkte</summary>
        public Body body { get; set; }
        /// <summary>Objekt des Sensors</summary>
        private SkeletonTracker sensor = null;

        private int init = 0;

        /// <summary>
        /// Startet Kinect und registriert die Punkte und Linien des Körper in der Ausgabe
        /// </summary>
        public Movement()
        {
            // Sensor starten
            Start();
        }

        /// <summary>
        /// Sensor starten
        /// </summary>
        public void Start()
        {
            sensor = new SkeletonTracker();
            sensor.Start();
            sensor.SkeletonEvent += new SkeletonTrackerEvent(GetEvent);
            body = Body.Instance;
        }

        /// <summary>
        /// Punkte und Linien für einen Körpder erstellen
        /// </summary>
        private void Initialize()
        {
            pointFootLeft = Visualization.addModel("data/models/cube.obj");
            pointFootRight = Visualization.addModel("data/models/cube.obj");
            pointHandLeft = Visualization.addModel("data/models/cube.obj");
            pointHandRight = Visualization.addModel("data/models/cube.obj");
            pointHead = Visualization.addModel("data/models/cube.obj");
            

            //lineForearmLeft = Visualization.addLine();
            lineForearmRight = Visualization.addModel("data/models/cube.obj");
            while (!Visualization.isModelCreated(lineForearmRight)) { }
            //lineUpperarmLeft = Visualization.addLine();
            lineUpperarmRight = Visualization.addModel("data/models/cube.obj");
            while (!Visualization.isModelCreated(lineUpperarmRight)) { }
            /*lineThighLeft = Visualization.addLine();
            lineThighRight = Visualization.addLine();
            lineLowerlegLeft = Visualization.addLine();
            lineLowerlegRight = Visualization.addLine();
            lineHipLeft = Visualization.addLine();
            lineHipRight = Visualization.addLine();
            lineHipCenter = Visualization.addLine();
            lineBreastLeft = Visualization.addLine();
            lineBreastRight = Visualization.addLine();
            lineBreastCenter = Visualization.addLine();*/
            //hand = Wrapper.addPoint("data/textures/hand.jpg");
            //start = Wrapper.addPoint("data/textures/start.png");
            Wrapper.positionModel(start, 0, 0, 0);
            
        }

        /// <summary>
        /// Punkte und Linien neu setzen und ausgabe neu Zeichnen
        /// </summary>
        public void Update()
        {
            body.ZModifikator(body.Spine.Z *-1);
            //body.Scale(3);

            Visualization.positionModel(pointFootLeft, body.FootLeft.X, body.FootLeft.Y, body.FootLeft.Z * -1);
            Visualization.positionModel(pointFootRight, body.FootRight.X, body.FootRight.Y, body.FootRight.Z * -1);
            Visualization.positionModel(pointHandLeft, body.HandLeft.X, body.HandLeft.Y, body.HandLeft.Z * -1);
            Visualization.positionModel(pointHandRight, body.HandRight.X, body.HandRight.Y, body.HandRight.Z * -1);
            Visualization.positionModel(pointHead, body.Head.X, body.Head.Y, body.Head.Z * -1);
            Visualization.scaleModel(pointFootLeft, 0.05f, 0.05f, 0.05f);
            Visualization.scaleModel(pointFootRight, 0.05f, 0.05f, 0.05f);
            Visualization.scaleModel(pointHandLeft, 0.05f, 0.05f, 0.05f);
            Visualization.scaleModel(pointHandRight, 0.05f, 0.05f, 0.05f);
            Visualization.scaleModel(pointHead, 0.1f, 0.1f, 0.1f);
            
            //Visualization.updateLine(lineForearmLeft, body.WristLeft.X, body.WristLeft.Y, body.WristLeft.Z * -1, body.ElbowLeft.X, body.ElbowLeft.Y, body.ElbowLeft.Z * -1);
            Visualization.drawLine(lineForearmRight, body.WristRight.X, body.WristRight.Y, body.WristRight.Z * -1, body.ElbowRight.X, body.ElbowRight.Y, body.ElbowRight.Z * -1);
            //Visualization.updateLine(lineUpperarmLeft, body.ElbowLeft.X, body.ElbowLeft.Y, body.ElbowLeft.Z * -1, body.ShoulderLeft.X, body.ShoulderLeft.Y, body.ShoulderLeft.Z * -1);
            Visualization.drawLine(lineUpperarmRight, body.ElbowRight.X, body.ElbowRight.Y, body.ElbowRight.Z * -1, body.ShoulderRight.X, body.ShoulderRight.Y, body.ShoulderRight.Z * -1);
            /*Visualization.updateLine(lineLowerlegLeft, body.AnkleLeft.X, body.AnkleLeft.Y, body.AnkleLeft.Z * -1, body.KneeLeft.X, body.KneeLeft.Y, body.KneeLeft.Z * -1);
            Visualization.updateLine(lineLowerlegRight, body.AnkleRight.X, body.AnkleRight.Y, body.AnkleRight.Z * -1, body.KneeRight.X, body.KneeRight.Y, body.KneeRight.Z * -1);
            Visualization.updateLine(lineThighLeft, body.KneeLeft.X, body.KneeLeft.Y, body.KneeLeft.Z * -1, body.HipLeft.X, body.HipLeft.Y, body.HipLeft.Z * -1);
            Visualization.updateLine(lineThighRight, body.KneeRight.X, body.KneeRight.Y, body.KneeRight.Z * -1, body.HipRight.X, body.HipRight.Y, body.HipRight.Z * -1);
            Visualization.updateLine(lineHipLeft, body.HipLeft.X, body.HipLeft.Y, body.HipLeft.Z * -1, body.Spine.X, body.Spine.Y, body.Spine.Z * -1);
            Visualization.updateLine(lineHipRight, body.HipRight.X, body.HipRight.Y, body.HipRight.Z * -1, body.Spine.X, body.Spine.Y, body.Spine.Z * -1);
            Visualization.updateLine(lineHipCenter, body.HipLeft.X, body.HipLeft.Y, body.HipLeft.Z * -1, body.HipRight.X, body.HipRight.Y, body.HipRight.Z * -1);
            Visualization.updateLine(lineBreastLeft, body.ShoulderLeft.X, body.ShoulderLeft.Y, body.ShoulderLeft.Z * -1, body.Spine.X, body.Spine.Y, body.Spine.Z * -1);
            Visualization.updateLine(lineBreastRight, body.ShoulderRight.X, body.ShoulderRight.Y, body.ShoulderRight.Z * -1, body.Spine.X, body.Spine.Y, body.Spine.Z * -1);
            Visualization.updateLine(lineBreastCenter, body.ShoulderLeft.X, body.ShoulderLeft.Y, body.ShoulderLeft.Z * -1, body.ShoulderRight.X, body.ShoulderRight.Y, body.ShoulderRight.Z * -1);
            */

            //Wrapper.positionModel(hand, body.HandRight.X, body.HandRight.Y, 0);
            //Wrapper.scaleModel(hand, 0.2f, 0.2f, 0.2f);
            //Wrapper.scaleModel(start, 0.8f, 1, 1);
            //Visualization.draw();
        }

        /// <summary>
        /// Ausgabe und Sensor sauber Beenden
        /// </summary>
        public void End()
        {
            //Visualization.close();
            sensor.Stop();
        }

        public void GetEvent()
        {
            if (init == 0)
            {
                Initialize();
                init = 1;
            }
            Update();
        }
    }
}
