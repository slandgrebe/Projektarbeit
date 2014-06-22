using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace VisualizationExample
{
    class Program
    {
        /*[DllImport("Visualization.dll", EntryPoint = "doSomething")]
        extern static void doSomething(string text);*/
        [DllImport("Visualization.dll")]
        public extern static bool init(string windowTitle, bool fullscreen, uint windowWidth, uint windowHeight);
        [DllImport("Visualization.dll")]
        public extern static bool isRunning();
        [DllImport("Visualization.dll")]
        public extern static void close();


        [DllImport("Visualization.dll")]
        public extern static void doSomething(string text);


        [DllImport("Visualization.dll")]
        public extern static uint addModel(string filename);
        [DllImport("Visualization.dll")]
        public extern static uint addPoint(string textureFilename);
        [DllImport("Visualization.dll")]
        public extern static uint addButton(string fontname);
        [DllImport("Visualization.dll")]
        public extern static uint addText(string fontFilename);

        [DllImport("Visualization.dll")]
        public extern static bool isCreated(uint modelId);
        [DllImport("Visualization.dll")]
        public extern static void dispose(uint modelId);


        [DllImport("Visualization.dll")]
        public extern static bool position(uint modelId, float x, float y, float z);
        [DllImport("Visualization.dll")]
        public extern static bool rotate(uint modelId, float degrees, float x, float y, float z);
        [DllImport("Visualization.dll")]
        public extern static bool scale(uint modelId, float x, float y, float z);
        [DllImport("Visualization.dll")]
        public extern static bool scalingIsNormalized(uint modelId, bool choice);
        [DllImport("Visualization.dll")]
        public extern static bool highlightColor(uint modelId, float r, float g, float b, float a);
        [DllImport("Visualization.dll")]
        public extern static bool isHighlighted(uint modelId, bool choice);
        [DllImport("Visualization.dll")]
        public extern static bool attachToCamera(uint modelId, bool choice);

        [DllImport("Visualization.dll")]
        public extern static bool text(uint textId, string text);
		[DllImport("Visualization.dll")]
        public extern static bool textSize(uint textId, int points);
        [DllImport("Visualization.dll")]
        public extern static bool textColor(uint textId, float r, float g, float b, float a);

        [DllImport("Visualization.dll")]
        public extern static void positionCamera(float x, float y, float z);
        [DllImport("Visualization.dll")]
        public extern static void rotateCamera(float degrees);
        [DllImport("Visualization.dll")]
        public extern static void tiltCamera(float degrees);
        [DllImport("Visualization.dll")]
        public extern static void changeCameraSpeed(float speed);

        [DllImport("Visualization.dll")]
        public extern static bool setCollisionGroup(uint modelId, uint collisionGroup);
        [DllImport("Visualization.dll")]
        public extern static uint collisionsTextLength();
        [DllImport("Visualization.dll")]
        public extern static bool collisionsText(System.Text.StringBuilder text, int length);

        static void Main(string[] args)
        {
            //System.Windows.Forms.MessageBox.Show("Mit <Esc> kann das Programm beendet werden.");

            // WINDOW
            init("Test", false, 640, 480);

            // CAMERA
            positionCamera(0, 0, 0);
            //changeCameraSpeed(1f);

            /*// ATTACH MODEL TO CAMERA
            uint modelId_attachedToCamera = addModel("data/models/cube.obj");
            while (!isCreated(modelId_attachedToCamera)) { }
            scale(modelId_attachedToCamera, 0.5f, 0.5f, 0.5f);
            scalingIsNormalized(modelId_attachedToCamera, true);
            position(modelId_attachedToCamera, -0.5f, 0f, -2f);
            rotate(modelId_attachedToCamera, -90.0f, 1.0f, 0.0f, 0.0f);
            highlightColor(modelId_attachedToCamera, 1.0f, 0.0f, 0.0f, 1.0f);
            isHighlighted(modelId_attachedToCamera, true);
            setCollisionGroup(modelId_attachedToCamera, 1);
            //attachToCamera(modelId_attachedToCamera, true);*/

            /*// MODEL TO COLLIDE
            uint modelId_collision = addModel("data/models/cube.obj");
            while (!isCreated(modelId_collision)) { }
            scale(modelId_collision, 0.5f, 0.5f, 0.5f);
            scalingIsNormalized(modelId_collision, true);
            position(modelId_collision, 0f, 0f, -2f);
            rotate(modelId_collision, -90.0f, 1.0f, 0.0f, 0.0f);
            highlightColor(modelId_collision, 0.0f, 1.0f, 0.0f, 1.0f);
            isHighlighted(modelId_collision, true);*/


            // 20 spieler
            List<uint> list = new List<uint>();
            uint numCubes = 20;
            for (int i = 0; i < numCubes; i++)
            {
                // MODEL TO COLLIDE
                list.Add(addModel("data/models/cube.obj"));
            }

            for (int i = 0; i < numCubes; i++)
            {
                uint modelId = list[i];
                while (!isCreated(modelId)) { }
                scale(modelId, 0.5f, 0.5f, 0.5f);
                scalingIsNormalized(modelId, true);
                position(modelId, 0.1f, -0.9f, -2.1f);
                rotate(modelId, -90.0f, 1.0f, 0.0f, 0.0f);
                highlightColor(modelId, 0.0f, 1.0f, 0.0f, 1.0f);
                isHighlighted(modelId, true);
                setCollisionGroup(modelId, 1);
                attachToCamera(modelId, true);
            }

            // 100 hindernisse
            list.Clear();
            numCubes = 100;
            for (int i = 0; i < numCubes; i++)
            {
                // MODEL TO COLLIDE
                list.Add(addModel("data/models/cube.obj"));
            }

            for (int i = 0; i < numCubes; i++)
            {
                uint modelId = list[i];
                while (!isCreated(modelId)) { }
                scale(modelId, 0.5f, 0.5f, 0.5f);
                scalingIsNormalized(modelId, true);
                position(modelId, 0f, -1f, -2f);
                rotate(modelId, -90.0f, 1.0f, 0.0f, 0.0f);
                highlightColor(modelId, 0.0f, 1.0f, 0.0f, 1.0f);
                isHighlighted(modelId, true);
                setCollisionGroup(modelId, 2);
            }

            // 100 bonus
            list.Clear();
            numCubes = 100;
            for (int i = 0; i < numCubes; i++)
            {
                // MODEL TO COLLIDE
                list.Add(addModel("data/models/cube.obj"));
            }

            for (int i = 0; i < numCubes; i++)
            {
                uint modelId = list[i];
                while (!isCreated(modelId)) { }
                scale(modelId, 0.5f, 0.5f, 0.5f);
                scalingIsNormalized(modelId, true);
                position(modelId, 0f, -1f, -2f);
                rotate(modelId, -90.0f, 1.0f, 0.0f, 0.0f);
                highlightColor(modelId, 0.0f, 1.0f, 0.0f, 1.0f);
                isHighlighted(modelId, true);
                setCollisionGroup(modelId, 3);
            }

            float rotation = 0f;
            float x = -2f;
            // RUNNING
            while (isRunning()) {
                System.Threading.Thread.Sleep(1); // senkt die CPU Auslastung drastisch

                handleCollisions(0);

                //rotate(modelId_attachedToCamera, rotation++, 0f, 1f, 0f);
                
                x += 0.001f;
                //position(0, x, 0f, -2f);
                //tiltCamera(rotation / 5);
            }
        }

        static void handleCollisions(uint modelId)
        {
            bool collision = false;
            
            // COLLISION DETECTION
            System.Collections.Generic.Dictionary<uint, System.Collections.Generic.List<uint>> collisionList = new System.Collections.Generic.Dictionary<uint, System.Collections.Generic.List<uint>>();

            uint length = collisionsTextLength();
            System.Text.StringBuilder str = new System.Text.StringBuilder((int)length+1);
            collisionsText(str, str.Capacity); // Daten aus DLL holen

            // str: 1:2,3;2:1,2 => 1 kollidiert mit 2 und 3. 2 kollidiert mit 1 und 2.
            String collisionsString = str.ToString(); // Daten parsen
            //Console.WriteLine(collisionsString);

            string[] models = collisionsString.Split(';');
            foreach (string model in models)
            {
                // model: 1:2,3
                string[] parts = model.Split(':');
                if (parts.Length != 2) continue; // Fehlerbehandlung: tritt beim schliessen des Fensters auf

                string aModelId = parts[0].ToString(); // modelId

                // wer kollidierte alles mit diesem Model?
                System.Collections.Generic.List<uint> collideeList = new System.Collections.Generic.List<uint>();

                if (parts[1] != "")
                {
                    string[] collidees = parts[1].Split(',');

                    foreach (string collidee in collidees)
                    {
                        if (collidee != "")
                        {
                            collideeList.Add(Convert.ToUInt32(collidee));
                        }
                    }
                }
                collisionList.Add(Convert.ToUInt32(aModelId), collideeList);
            }

            /*// Liste abarbeiten
            if (collisionList.ContainsKey(modelId))
            {
                System.Collections.Generic.List<uint> myCollisions = collisionList[modelId];
                if (myCollisions.Count > 0) // es gibt kollisionen => das Objekt selber behandeln
                {
                    //Console.WriteLine("Collision detected!");
                    highlightColor(modelId, 1f, 1f, 0f, 1f);
                    isHighlighted(modelId, true);
                    //dispose(modelId);
                }
                else // keine Kollision
                {
                    highlightColor(modelId, 1f, 0f, 1f, 1f);
                    isHighlighted(modelId, false);
                }

                // jede Kollision von diesem Objekt mit einem anderen
                foreach (uint anotherModelId in myCollisions)
                {
                    highlightColor(anotherModelId, 0f, 1f, 1f, 1f);
                    isHighlighted(anotherModelId, true);
                }
            }*/
        }
    }
}
