using System.Runtime.InteropServices;

namespace View
{
    /// <summary>
    /// Einbinden der Visualization.dll in C#
    /// </summary>
    public static class Visualization
    {
        [DllImport("Visualization.dll")]
        public extern static bool isRunning();

        [DllImport("Visualization.dll")]
        public extern static void doSomething(string text);

        [DllImport("Visualization.dll")]
        public extern static uint addModel(string filename);

        [DllImport("Visualization.dll")]
        public extern static uint addPoint(string textureFilename);

        [DllImport("Visualization.dll")]
        public extern static bool isModelCreated(uint modelId);

        [DllImport("Visualization.dll")]
        public extern static bool positionModel(uint modelId, float x, float y, float z);

        [DllImport("Visualization.dll")]
        public extern static bool rotateModel(uint modelId, float degrees, float x, float y, float z);

        [DllImport("Visualization.dll")]
        public extern static bool scaleModel(uint modelId, float x, float y, float z);


        [DllImport("Visualization.dll")]
        public extern static uint addText(string text);

        [DllImport("Visualization.dll")]
        public extern static void setText(uint textId, string text);
        [DllImport("Visualization.dll")]
        public extern static void setTextPosition(uint textId, int x, int y);
        [DllImport("Visualization.dll")]
        public extern static bool setTextSize(uint textId, int points);
        [DllImport("Visualization.dll")]
        public extern static void setTextColor(uint textId, float r, float g, float b, float a);
        [DllImport("Visualization.dll")]
        public extern static bool setFontFamily(uint textId, string filename);
    }
}
