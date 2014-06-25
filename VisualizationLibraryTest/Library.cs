using System;
using System.Runtime.InteropServices;

namespace VisualizationLibraryTest
{
    class Library
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
        public extern static float positionX(uint modelId);
        [DllImport("Visualization.dll")]
        public extern static float positionY(uint modelId);
        [DllImport("Visualization.dll")]
        public extern static float positionZ(uint modelId);
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
        public extern static bool setModelVisibility(uint modelId, bool choice);
        [DllImport("Visualization.dll")]
        public extern static bool getModelVisibility(uint modelId);


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
        public extern static bool addCollisionModel(uint modelId, string filename);
        [DllImport("Visualization.dll")]
        public extern static bool collisionGroup(uint modelId, uint collisionGroup);
        [DllImport("Visualization.dll")]
        public extern static uint collisionsTextLength();
        [DllImport("Visualization.dll")]
        public extern static bool collisionsText(System.Text.StringBuilder text, int length);
    }
}
