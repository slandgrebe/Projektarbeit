using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using View;

namespace Controller
{
    public class Model : IDisposable
    {
        public uint Id { get; set; }
        public string Path { get; set; }
        public bool ScalingNormalized { get; set; }

        private bool disposed = false;

        public Model()
        {
            ScalingNormalized = false;
        }

        public Model(string path, bool scalingNormalized = false)
        {
            Path = path;
            ScalingNormalized = scalingNormalized;
            Create();
        }

        public void Create()
        {
            Id = Visualization.AddModel(Path);
            while (!Visualization.IsCreated(Id)) { }
            Visualization.ScalingIsNormalized(Id, ScalingNormalized);
        }

        public void Scale(float scale)
        {
            Visualization.Scale(Id, scale, scale, scale);
        }

        public void Rotate(float degrees, float x, float y, float z)
        {
            Visualization.Rotate(Id, degrees, x, y, z);
        }

        public bool Alignment(float fromX, float fromY, float fromZ, float toX, float toY, float toZ)
        {
            if (fromX == toX && fromY == toY && fromZ == toZ)
            {
                return false;
            }

            //Vector aus p1 und p2
            double xv = System.Convert.ToDouble(toX - fromX);
            double yv = System.Convert.ToDouble(toY - fromY);
            double zv = System.Convert.ToDouble(toZ - fromZ);

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

            //Model Positionieren und ausrichten
            Visualization.Rotate(Id, (float)w, (float)xa, (float)ya, (float)za);
            Visualization.Position(Id, fromX, fromY, fromZ);

            return true;
        }

        public void Position(float x, float y, float z)
        {
            Visualization.Position(Id, x, y, z);
        }

        public void AttachToCamera(bool choice)
        {
            Visualization.AttachToCamera(Id, choice);
        }

        public void Dispose()
        {
            if (!disposed)
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Freigabe verwalteter Objekte
            }
            // Freigabe von Fremdresourcen
            Visualization.Dispose(Id);
        }

        ~Model()
        {
            Dispose(false);
        }
    }
}
