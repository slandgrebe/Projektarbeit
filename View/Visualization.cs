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
        public extern static bool setModelHighlightColor(uint modelId, float r, float g, float b, float a);

        [DllImport("Visualization.dll")]
        public extern static bool isModelHighlighted(uint modelId, bool choice);


        [DllImport("Visualization.dll")]
        public extern static uint addText(string fontFilename);

        [DllImport("Visualization.dll")]
        public extern static void setText(uint textId, string text);
        [DllImport("Visualization.dll")]
        public extern static void setTextPosition(uint textId, int x, int y);
        [DllImport("Visualization.dll")]
        public extern static bool setTextSize(uint textId, int points);
        [DllImport("Visualization.dll")]
        public extern static void setTextColor(uint textId, float r, float g, float b, float a);

        public static void drawLine(uint id, float x1, float y1, float z1, float x2, float y2, float z2)
        {
            //Vector aus p1 und p2
            double xv = System.Convert.ToDouble(x2 - x1);
            double yv = System.Convert.ToDouble(y2 - y1);
            double zv = System.Convert.ToDouble(z2 - z1);

            //länge berechnen
            double lenght = System.Math.Sqrt(xv * xv + yv * yv + zv * zv);


            //Vector normalisieren
            double xvn = xv / lenght;
            double yvn = yv / lenght;
            double zvn = zv / lenght;

            //Standard Vector definieren
            double xs = 1;
            double ys = 0;
            double zs = 0;

            //berechnung der Rotationsachse
            double xa = yvn * zs - zvn * ys;
            double ya = zvn * xs - xvn * zs;
            double za = xvn * ys - yvn * xs;

            //berechnung cosinus des rotationswinkels
            double cosw = xvn * xs + yvn * ys + zvn * zs;

            //rotationswinkel in radian berechnen
            double w = System.Math.Acos(cosw);

            //radian in Grad umrechnen
            w = w * 180 / System.Math.PI;

            System.Console.WriteLine((float)w);
            System.Console.WriteLine((float)xa);
            System.Console.WriteLine((float)ya);
            System.Console.WriteLine((float)za);
            rotateModel(id,(float)w,(float)xa,(float)ya,(float)za);
            //rotateModel(id, -45.0f, 1.0f, 0.1252f, -1.0f);
            scaleModel(id, (float)lenght, 0.025f, 0.025f);
            positionModel(id, (float)(x1 + x2) / 2, (float)(y1 + y2) / 2, (float)(z1 + z2) / 2);
            //Visualization.positionModel(id, (float)(x1 + x2) / 2, (float)(y1 + y2) / 2, 0f);
            //rotate(id, w, xa,ya,za)  (float)
            //scale(id, lenght, 1,1);
            //position(id, (x1+x2)/2 , (y1+y2)/2, (z1+z2)/2);
        }
    }
}
