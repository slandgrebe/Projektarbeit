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


        static void Main(string[] args)
        {
            Console.WriteLine("hallo");

            uint modelId = addModel("data/models/shuttle/SpaceShuttleOrbiter.3ds");
            while (!isModelCreated(modelId)) { }
            scaleModel(modelId, 0.001f, 0.001f, 0.001f);


            Console.WriteLine("zurück in c#");

            Console.ReadLine();
        }
    }
}
