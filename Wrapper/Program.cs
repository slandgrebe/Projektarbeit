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
        extern static bool isCreated(uint modelId);

        [DllImport("Visualization.dll")]
        extern static bool position(uint modelId, float x, float y, float z);

        [DllImport("Visualization.dll")]
        extern static bool rotate(uint modelId, float degrees, float x, float y, float z);

        [DllImport("Visualization.dll")]
        extern static bool scale(uint modelId, float x, float y, float z);

        [DllImport("Visualization.dll")]
        extern static bool highlightColor(uint modelId, float r, float g, float b, float a);

        [DllImport("Visualization.dll")]
        extern static bool isHighlighted(uint modelId, bool choice);


        [DllImport("Visualization.dll")]
        extern static uint addText(string fontFilename);

        [DllImport("Visualization.dll")]
        extern static void text(uint textId, string text);
		[DllImport("Visualization.dll")]
		extern static bool textSize(uint textId, int points);
        [DllImport("Visualization.dll")]
		extern static void textColor(uint textId, float r, float g, float b, float a);


        [DllImport("Visualization.dll")]
        extern static uint addButton(string fontname);
        /*[DllImport("Visualization.dll")]
        extern static void setButtonText(uint buttonId, string text);
        
        // Normale highlight Methoden verwenden
        [DllImport("Visualization.dll")]
        extern static void setButtonHighlightColor(uint buttonId, float r, float g, float b, float a);
        [DllImport("Visualization.dll")]
        extern static void isButtonHighlighted(uint buttonId, bool choice);*/



        static void Main(string[] args)
        {
            Console.WriteLine("Start");

            
            uint pointId = addPoint("data/textures/sample.png");
            while (!isCreated(pointId)) { }
            position(pointId, -0.5f, 0.5f, -5f);

            uint pointId2 = addPoint("data/textures/test.png");
            while (!isCreated(pointId2)) { }
            position(pointId2, 0.5f, 0.5f, -3f);


            uint modelId = addModel("data/models/shuttle/SpaceShuttleOrbiter.3ds");
            while (!isCreated(modelId)) { }
            scale(modelId, 0.0005f, 0.0005f, 0.0005f);
            rotate(modelId, -45.0f, 1.0f, 0.0f, 1.0f);
            position(modelId, -2f, -0.5f, -4.5f);
            highlightColor(modelId, 0.0f, 1.0f, 0.0f, 1.0f);
            isHighlighted(modelId, true);

            uint modelId2 = addModel("data/models/shuttle/SpaceShuttleOrbiter.3ds");
            while (!isCreated(modelId2)) { }
            scale(modelId2, 0.0005f, 0.0005f, 0.0005f);
            rotate(modelId2, -45.0f, 0.0f, 1.0f, 1.0f);
            position(modelId2, -0.3f, -0.4f, -1.5f);
            highlightColor(modelId2, 0.0f, 1.0f, 0.0f, 0.5f);
            isHighlighted(modelId2, false);

            
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

            while (isRunning()) {
                // do Something
            }
        }
    }
}
