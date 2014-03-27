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
        extern static uint addText(string text);

        [DllImport("Visualization.dll")]
        extern static void setText(uint textId, string text);
		[DllImport("Visualization.dll")]
        extern static void setTextPosition(uint textId, int x, int y);
        [DllImport("Visualization.dll")]
		extern static bool setTextSize(uint textId, int points);
        [DllImport("Visualization.dll")]
		extern static void setTextColor(uint textId, float r, float g, float b, float a);
        [DllImport("Visualization.dll")]
		extern static bool setFontFamily(uint textId, string filename);

        static void Main(string[] args)
        {
            //addText("hallo");

            Console.WriteLine("Start");

            
            uint pointId = addPoint("data/textures/sample.png");
            while (!isModelCreated(pointId)) { }
            positionModel(pointId, 0.5f, 0.5f, -0.5f);

            uint pointId2 = addPoint("data/textures/sample.png");
            while (!isModelCreated(pointId2)) { }
            positionModel(pointId2, 0.4f, 0.4f, -0.5f);

            /*uint modelId = addModel("data/models/shuttle/SpaceShuttleOrbiter.3ds");
            while (!isModelCreated(modelId)) { }
            scaleModel(modelId, 0.0005f, 0.0005f, 0.0005f);
            rotateModel(modelId, -45.0f, 0.0f, 0.0f, 1.0f);
            positionModel(modelId, -0.5f, -0.5f, -0.5f);

            uint modelId2 = addModel("data/models/shuttle/SpaceShuttleOrbiter.3ds");
            while (!isModelCreated(modelId2)) { }
            scaleModel(modelId2, 0.0005f, 0.0005f, 0.0005f);
            rotateModel(modelId2, -45.0f, 0.0f, 0.0f, 1.0f);
            positionModel(modelId2, -0.4f, -0.4f, -0.5f);

            Console.WriteLine("zurück in c#");*/
            
            uint textId = addText("Toller Text");
            while (!isModelCreated(textId)) { }
            setText(textId, "Es geht!");
            setTextPosition(textId, 100, 100);
            setTextSize(textId, 36);
            setTextColor(textId, 0.92f, 0.95f, 0.16f, 1.0f);

            uint textId2 = addText("Toller Text");
            while (!isModelCreated(textId2)) { }
            setText(textId2, "noch viel mehr Text!");
            setTextPosition(textId2, 100, 50);
            setTextSize(textId2, 24);
            setTextColor(textId2, 1.0f, 0.5f, 0.0f, 1.0f);

            while (isRunning()) { }
        }
    }
}
