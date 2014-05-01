using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    /// <summary>
    /// Verwaltet ein 3D Modell
    /// </summary>
    public class Model : IDisposable
    {
        /// <summary>Id des Objekts</summary> 
        public uint Id { get; set; }
        /// <summary>Dateipfad zum 3D Modelles</summary>
        public string Path { get; set; }
        /// <summary>Modell beim erstellen auf einen Durchmesser von einem Meter bzw. einer Einheit skallieren</summary>
        public bool ScalingNormalized { get; set; }
        /// <summary>Modell wurde gelöscht</summary>
        private bool disposed = false;

        /// <summary>
        /// Konstruktor der Klasse ohne automatische Objekterstellung.
        /// </summary>
        public Model()
        {
            ScalingNormalized = false;
        }

        /// <summary>
        /// Konstruktor der Klasse. Erstellt das im Pfad mitgegebene Modell automatisch.
        /// </summary>
        /// <param name="path">Dateipfad zur 3D-Datei</param>
        /// <param name="scalingNormalized">Modell beim erstellen auf einen Durchmesser von einem Meter bzw. einer Einheit skallieren.</param>
        public Model(string path, bool scalingNormalized = false)
        {
            Path = path;
            ScalingNormalized = scalingNormalized;
            Create();
        }

        /// <summary>
        /// Erstellt das Modell in der Ausgabe
        /// </summary>
        public void Create()
        {
            Id = View.Model.AddModel(Path);
            while (!View.Model.IsCreated(Id)) { }
            View.Model.ScalingIsNormalized(Id, ScalingNormalized);
        }

        /// <summary>
        /// Skalliert das Modell
        /// </summary>
        /// <param name="scale">Skalierungsangabe</param>
        public void Scale(float scale)
        {
            View.Model.Scale(Id, scale, scale, scale);
        }

        /// <summary>
        /// Rotiert das Madell
        /// </summary>
        /// <param name="degrees">Rotationswinkel in Grad</param>
        /// <param name="x">x Komponente der Rotationsachse</param>
        /// <param name="y">y Komponente der Rotationsachse</param>
        /// <param name="z">z Komponente der Rotationsachse</param>
        public void Rotate(float degrees, float x, float y, float z)
        {
            View.Model.Rotate(Id, degrees, x, y, z);
        }

        /// <summary>
        /// Modell auf einen Zielpunkt ausrichten
        /// </summary>
        /// <param name="fromX">X Koordinate des Modells</param>
        /// <param name="fromY">Y Koordinate des Modells</param>
        /// <param name="fromZ">Z Koordinate des Modells</param>
        /// <param name="toX">Ziel X Koordinate</param>
        /// <param name="toY">Ziel Y Koordinate</param>
        /// <param name="toZ">Ziel Z Koordinate</param>
        /// <returns>Ausrichtung erfolgreich ausgeführt</returns>
        public bool Alignment(float fromX, float fromY, float fromZ, float toX, float toY, float toZ)
        {
            // Ausgangspunkt darf nicht Zielpunkt sein.
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
            View.Model.Rotate(Id, (float)w, (float)xa, (float)ya, (float)za);
            View.Model.Position(Id, fromX, fromY, fromZ);

            return true;
        }

        /// <summary>
        /// Positioniert das Modell
        /// </summary>
        /// <param name="x">x Koordinate</param>
        /// <param name="y">y Koordinate</param>
        /// <param name="z">z Koordinate</param>
        public void Position(float x, float y, float z)
        {
            View.Model.Position(Id, x, y, z);
        }

        /// <summary>
        /// Hängt ein Modell an die Kamera an. Dies hat zur Folge, dass dieses Objekt relativ zur Kamera positioniert wird.
        /// </summary>
        /// <param name="choice"></param>
        public void AttachToCamera(bool choice)
        {
            View.Model.AttachToCamera(Id, choice);
        }

        /// <summary>
        /// Entfernt ein Modell.
        /// </summary>
        public void Dispose()
        {
            if (!disposed)
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }
        }

        /// <summary>
        /// Wird aufgerufen, wenn das Objekt als nicht mehr verwendet erkannt wird.
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Freigabe verwalteter Objekte
            }
            // Freigabe von Fremdresourcen
            View.Model.Dispose(Id);
        }

        /// <summary>
        /// Destruktor der Klasse. Entfernt das Modell aus der Ausgabe.
        /// </summary>
        ~Model()
        {
            Dispose(false);
        }
    }
}
