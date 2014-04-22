using System;
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
        public extern static float positionZ(uint modelId);

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
        
        public static bool drawLine(uint id, float x1, float y1, float z1, float x2, float y2, float z2)
        {
            if (x1 == x2 && y1 == y2 && z1 == z2)
            {
                return false;
            }
            
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
            double xa = zvn * ys - yvn * zs;
            double ya = xvn * zs - zvn * xs;
            double za = yvn * xs - xvn * ys;

            //berechnung cosinus des rotationswinkels
            double cosw = xvn * xs + yvn * ys + zvn * zs;

            //rotationswinkel in radian berechnen
            double w = System.Math.Acos(cosw);

            //radian in Grad umrechnen
            w = w * 180 / System.Math.PI;


            rotate(id,(float)w,(float)xa,(float)ya,(float)za);
            //scaleModel(id, (float)lenght, 0.025f, 0.025f);
            //position(id, (float)(x1 + x2) / 2, (float)(y1 + y2) / 2, (float)(z1 + z2) / 2);
            position(id, x1, y1, z1);
            //rotate(id, w, xa,ya,za)  (float)
            //scale(id, lenght, 1,1);
            //position(id, (x1+x2)/2 , (y1+y2)/2, (z1+z2)/2);
            return true;
        }

        public static void handleCollisions(uint modelId)
        {
            // COLLISION DETECTION
            System.Collections.Generic.Dictionary<uint, System.Collections.Generic.List<uint>> collisionList = new System.Collections.Generic.Dictionary<uint, System.Collections.Generic.List<uint>>();

            uint length = collisionsTextLength();
            System.Text.StringBuilder str = new System.Text.StringBuilder((int)length + 1);
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
                    //dispose(modelId);
                }
                else // keine Kollision
                {
                    //highlightColor(modelId, 1f, 1f, 1f, 1f);
                    //isHighlighted(modelId, false);
                }

                // jede Kollision von diesem Objekt mit einem anderen
                foreach (uint anotherModelId in myCollisions)
                {
                    //highlightColor(anotherModelId, 1f, 0f, 0f, 1f);
                    //isHighlighted(anotherModelId, true);
                }
            }
        }
    }
}
