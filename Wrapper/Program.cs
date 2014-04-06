using System;
using System.Runtime.InteropServices;

namespace Wrapper
{
    class Program
    {
        /*[DllImport("Visualization.dll", EntryPoint = "doSomething")]
        extern static void doSomething(string text);*/
        [DllImport("Visualization.dll")]
        extern static bool isRunning();

        [DllImport("Visualization.dll")]
        extern static void doSomething(string text);

        [DllImport("Visualization.dll")]
        extern static uint addModel(string filename);

        [DllImport("Visualization.dll")]
        extern static uint addPoint(string textureFilename);

        [DllImport("Visualization.dll")]
        extern static bool isModelCreated(uint modelId);

        [DllImport("Visualization.dll")]
        extern static bool positionModel(uint modelId, float x, float y, float z);

        [DllImport("Visualization.dll")]
        extern static bool rotateModel(uint modelId, float degrees, float x, float y, float z);

        [DllImport("Visualization.dll")]
        extern static bool scaleModel(uint modelId, float x, float y, float z);

        [DllImport("Visualization.dll")]
        extern static bool setModelHighlightColor(uint modelId, float r, float g, float b, float a);

        [DllImport("Visualization.dll")]
        extern static bool isModelHighlighted(uint modelId, bool choice);


        [DllImport("Visualization.dll")]
        extern static uint addText(string fontFilename);

        [DllImport("Visualization.dll")]
        extern static void setText(uint textId, string text);
        [DllImport("Visualization.dll")]
		extern static bool setTextSize(uint textId, int points);
        [DllImport("Visualization.dll")]
		extern static void setTextColor(uint textId, float r, float g, float b, float a);


        [DllImport("Visualization.dll")]
        extern static uint addButton(string fontname);
        [DllImport("Visualization.dll")]
        extern static void setButtonText(uint buttonId, string text);
        /*
        // Normale highlight Methoden verwenden
        [DllImport("Visualization.dll")]
        extern static void setButtonHighlightColor(uint buttonId, float r, float g, float b, float a);
        [DllImport("Visualization.dll")]
        extern static void isButtonHighlighted(uint buttonId, bool choice);*/



        static void Main(string[] args)
        {
            Console.WriteLine("Start");

            
            uint pointId = addPoint("data/textures/sample.png");
            while (!isModelCreated(pointId)) { }
            positionModel(pointId, -0.5f, 0.5f, -5f);

            uint pointId2 = addPoint("data/textures/test.png");
            while (!isModelCreated(pointId2)) { }
            positionModel(pointId2, 0.5f, 0.5f, -3f);


            uint modelId = addModel("data/models/shuttle/SpaceShuttleOrbiter.3ds");
            while (!isModelCreated(modelId)) { }
            scaleModel(modelId, 0.0005f, 0.0005f, 0.0005f);
            rotateModel(modelId, -45.0f, 1.0f, 0.0f, 1.0f);
            positionModel(modelId, -2f, -0.5f, -4.5f);
            setModelHighlightColor(modelId, 0.0f, 1.0f, 0.0f, 1.0f);
            isModelHighlighted(modelId, true);

            uint modelId2 = addModel("data/models/shuttle/SpaceShuttleOrbiter.3ds");
            while (!isModelCreated(modelId2)) { }
            scaleModel(modelId2, 0.0005f, 0.0005f, 0.0005f);
            rotateModel(modelId2, -45.0f, 0.0f, 1.0f, 1.0f);
            positionModel(modelId2, -0.3f, -0.4f, -1.5f);
            setModelHighlightColor(modelId2, 0.0f, 1.0f, 0.0f, 0.5f);
            isModelHighlighted(modelId2, false);

            
            uint textId = addText("");
            while (!isModelCreated(textId)) { }
            setText(textId, "Es geht!");
            positionModel(textId, 0.7f, 0.5f, 1.0f);
            setTextSize(textId, 36);
            setTextColor(textId, 0.92f, 0.95f, 0.16f, 1.0f);

            uint textId2 = addText("data/fonts/KBZipaDeeDooDah.ttf");
            while (!isModelCreated(textId2)) { }
            setText(textId2, "noch viel mehr Text!");
            setTextSize(textId2, 60);
            setTextColor(textId2, 1.0f, 0.5f, 0.0f, 1.0f);


            uint buttonId = addButton("data/fonts/arial.ttf");
            while (!isModelCreated(buttonId)) { }

            uint buttonId2 = addButton("data/fonts/arial.ttf");
            while (!isModelCreated(buttonId2)) { }
            setModelHighlightColor(buttonId2, 0.5f, 0f, 0f, 1f);
            isModelHighlighted(buttonId2, true);
            positionModel(buttonId2, 0.5f, -0.5f, 1); // Z-Koordinate wird ignoriert, da es sich beim Button um ein GUI Element handelt
            scaleModel(buttonId2, 0.5f, 0.5f, 1); // Skalierung in z-Richtung wird ignoriert, da es sich beim Button um ein GUI Element handelt
            setText(buttonId2, "anderer text");
            setTextColor(buttonId2, 0.1f, 0.1f, 0.0f, 1.0f);
            setTextSize(buttonId2, 24);
            

            Console.WriteLine("zurück in c#");

            while (isRunning()) {
                // do Something
            }
        }
    }
}
