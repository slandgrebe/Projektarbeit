using System;
using System.Runtime.InteropServices;

namespace Wrapper
{
    class Program
    {
        /*[DllImport("Visualization.dll", EntryPoint = "doSomething")]
        extern static void doSomething(string text);*/
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
        extern static bool addText(string text);

        static void Main(string[] args)
        {
            //addText("hallo");

            Console.WriteLine("hallo");

            uint modelId = addPoint("data/textures/sample.png");
            while (!isModelCreated(modelId)) { }
            positionModel(modelId, 0.5f, 0.5f, -0.5f);

            uint modelId2 = addModel("data/models/shuttle/SpaceShuttleOrbiter.3ds");
            while (!isModelCreated(modelId2)) { }
            scaleModel(modelId2, 0.0005f, 0.0005f, 0.0005f);
            rotateModel(modelId2, -45.0f, 0.0f, 0.0f, 1.0f);
            positionModel(modelId2, -0.5f, -0.5f, -0.5f);

            Console.WriteLine("zurück in c#");

            //addText("hallo");

            Console.ReadLine();
        }
    }
}
