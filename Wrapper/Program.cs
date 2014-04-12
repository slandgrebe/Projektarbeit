using System;
using System.Runtime.InteropServices;

namespace VisualizationExample
{
    class Program
    {
        /*[DllImport("Visualization.dll", EntryPoint = "doSomething")]
        extern static void doSomething(string text);*/
        [DllImport("Visualization.dll")]
        public extern static bool isRunning();


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
        public extern static bool highlightColor(uint modelId, float r, float g, float b, float a);
        [DllImport("Visualization.dll")]
        public extern static bool isHighlighted(uint modelId, bool choice);
        [DllImport("Visualization.dll")]
        public extern static bool attachToCamera(uint modelId, bool choice);

        [DllImport("Visualization.dll")]
        public extern static void text(uint textId, string text);
		[DllImport("Visualization.dll")]
        public extern static bool textSize(uint textId, int points);
        [DllImport("Visualization.dll")]
        public extern static void textColor(uint textId, float r, float g, float b, float a);

        [DllImport("Visualization.dll")]
        public extern static void positionCamera(float x, float y, float z);
        [DllImport("Visualization.dll")]
        public extern static void rotateCamera(float degrees);
        [DllImport("Visualization.dll")]
        public extern static void tiltCamera(float degrees);
        [DllImport("Visualization.dll")]
        public extern static void changeCameraSpeed(float speed);

        [DllImport("Visualization.dll")]
        public extern static uint collisionsTextLength();
        [DllImport("Visualization.dll")]
        public extern static void collisionsText(System.Text.StringBuilder text, int length);

        static void Main(string[] args)
        {
            System.Windows.Forms.MessageBox.Show("Mit <Esc> kann das Programm beendet werden.");
            
            // POINTS
            uint pointId = addPoint("data/textures/sample.png");
            while (!isCreated(pointId)) { }
            position(pointId, -0.5f, 0.5f, -5f);

            uint pointId2 = addPoint("data/textures/test.png");
            while (!isCreated(pointId2)) { }
            position(pointId2, 0.5f, 0.5f, -3f);

            // MODELS
            uint modelId = addModel("data/models/shuttle/SpaceShuttleOrbiter.3ds");
            while (!isCreated(modelId)) { }
            scale(modelId, 2.0f, 0.5f, 1f);
            rotate(modelId, -45.0f, 1.0f, 0.0f, 1.0f);
            position(modelId, -2f, -0.5f, -4.5f);
            highlightColor(modelId, 0.0f, 1.0f, 0.0f, 1.0f);
            isHighlighted(modelId, true);

            uint modelId2 = addModel("data/models/shuttle/SpaceShuttleOrbiter.3ds");
            while (!isCreated(modelId2)) { }
            scale(modelId2, 0.5f, 0.5f, 0.5f);
            rotate(modelId2, -45.0f, 0.0f, 1.0f, 1.0f);
            position(modelId2, -0.3f, -0.4f, -1.5f);
            highlightColor(modelId2, 0.0f, 1.0f, 0.0f, 0.5f);
            isHighlighted(modelId2, false);

            // TEXT
            uint textId = addText("");
            while (!isCreated(textId)) { }
            text(textId, "Es geht!");
            position(textId, 0.7f, 0.5f, 1.0f);
            textSize(textId, 36);
            textColor(textId, 0.92f, 0.95f, 0.16f, 1.0f);

            uint textId2 = addText("data/fonts/KBZipaDeeDooDah.ttf");
            while (!isCreated(textId2)) { }
            text(textId2, "noch viel mehr Text!");
            textSize(textId2, 60);
            textColor(textId2, 1.0f, 0.5f, 0.0f, 1.0f);

            // BUTTON
            uint buttonId = addButton("data/fonts/arial.ttf");
            while (!isCreated(buttonId)) { }

            uint buttonId2 = addButton("data/fonts/arial.ttf");
            while (!isCreated(buttonId2)) { }
            highlightColor(buttonId2, 0.5f, 0f, 0f, 1f);
            isHighlighted(buttonId2, true);
            position(buttonId2, 0.5f, -0.5f, 1); // Z-Koordinate wird ignoriert, da es sich beim Button um ein GUI Element handelt
            scale(buttonId2, 0.5f, 0.5f, 1); // Skalierung in z-Richtung wird ignoriert, da es sich beim Button um ein GUI Element handelt
            text(buttonId2, "anderer text");
            textColor(buttonId2, 0.1f, 0.1f, 0.0f, 1.0f);
            textSize(buttonId2, 24);
            

            Console.WriteLine("zurück in c#");
            System.Threading.Thread.Sleep(2000);

            // DISPOSE
            dispose(buttonId);
            dispose(pointId);
            dispose(modelId);
            dispose(textId);

            // CAMERA
            positionCamera(0, 0, 5);
            changeCameraSpeed(1f);

            // ATTACH MODEL TO CAMERA
            uint modelId_attachedToCamera = addModel("data/models/shuttle/SpaceShuttleOrbiter.3ds");
            while (!isCreated(modelId_attachedToCamera)) { }
            //scale(modelId3, 0.0005f, 0.0005f, 0.0005f);
            rotate(modelId_attachedToCamera, -90.0f, 1.0f, 0.0f, 0.0f);
            highlightColor(modelId_attachedToCamera, 0.0f, 0.0f, 1.0f, 1.0f);
            isHighlighted(modelId_attachedToCamera, true);
            attachToCamera(modelId_attachedToCamera, true);

            // MODEL TO COLLIDE
            uint modelId_collision = addModel("data/models/shuttle/SpaceShuttleOrbiter.3ds");
            while (!isCreated(modelId_collision)) { }
            //scale(modelId3, 0.0005f, 0.0005f, 0.0005f);
            rotate(modelId_collision, -90.0f, 1.0f, 0.0f, 0.0f);
            highlightColor(modelId_collision, 0.0f, 1.0f, 1.0f, 1.0f);
            isHighlighted(modelId_collision, true);

            // RUNNING
            while (isRunning()) {
                System.Threading.Thread.Sleep(1); // senkt die CPU Auslastung drastisch

                handleCollisions(modelId_attachedToCamera);
            }
        }

        static void handleCollisions(uint modelId)
        {
            // COLLISION DETECTION
            System.Collections.Generic.Dictionary<uint, System.Collections.Generic.List<uint>> collisionList = new System.Collections.Generic.Dictionary<uint, System.Collections.Generic.List<uint>>();

            uint length = collisionsTextLength();
            System.Text.StringBuilder str = new System.Text.StringBuilder((int)length+1);
            collisionsText(str, str.Capacity); // Daten aus DLL holen
            // str: 1:2,3;2:1,2 => 1 kollidiert mit 2 und 3. 2 kollidiert mit 1 und 2.
            String collisionsString = str.ToString(); // Daten parsen
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

            // Liste abarbeiten
            if (collisionList.ContainsKey(modelId))
            {
                System.Collections.Generic.List<uint> myCollisions = collisionList[modelId];
                if (myCollisions.Count > 0) // es gibt kollisionen => das Objekt selber behandeln
                {
                    highlightColor(modelId, 1f, 0f, 0f, 1f);
                    isHighlighted(modelId, true);
                }
                else // keine Kollision
                {
                    highlightColor(modelId, 1f, 1f, 1f, 1f);
                    isHighlighted(modelId, false);
                }

                // jede Kollision von diesem Objekt mit einem anderen
                foreach (uint anotherModelId in myCollisions)
                {
                    highlightColor(anotherModelId, 1f, 0f, 0f, 1f);
                    isHighlighted(anotherModelId, true);
                }
            }
        }
    }
}
