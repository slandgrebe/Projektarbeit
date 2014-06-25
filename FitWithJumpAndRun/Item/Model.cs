using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JumpAndRun.Item
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
        /// <summary>Dateipfad zum Kollisionsmodel</summary>
        public string CollisionPath { get; set; }
        /// <summary>Modell beim erstellen auf einen Durchmesser von einem Meter bzw. einer Einheit skallieren</summary>
        public bool ScalingNormalized { get; set; }
        /// <summary>Sound bei Kollision</summary>
        public string Sound { get; set; }
        /// <summary>Soundlautstärke bei Kollision</summary>
        public int SoundVolume { get; set; }
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
        public Model(string path, bool scalingNormalized = false, uint group = 0)
        {
            Path = path;
            ScalingNormalized = scalingNormalized;
            Create();
            CollisionGroup(group);
        }

        /// <summary>
        /// Erstellt das Modell in der Ausgabe
        /// </summary>
        /// <returns>Prüfung ob die Operation durchgeführt werden konnte</returns>
        public bool Create()
        {
            if (Id > 0) return false;
            if (Path == null || String.IsNullOrEmpty(Path)) return false;
            Id = View.Model.AddModel(Path);
            while (!View.Model.IsCreated(Id)) { }
            View.Model.ScalingIsNormalized(Id, ScalingNormalized);
            Visibility(false);
            return true;
        }

        /// <summary>
        /// Skalliert das Modell
        /// </summary>
        /// <param name="scale">Skalierungsangabe</param>
        /// <returns>Prüfung ob die Operation durchgeführt werden konnte</returns>
        public bool Scale(float scale)
        {
            if (Id == 0) return false;
            return View.Model.Scale(Id, scale, scale, scale);
        }

        /// <summary>
        /// Rotiert das Madell
        /// </summary>
        /// <param name="degrees">Rotationswinkel in Grad</param>
        /// <param name="x">x Komponente der Rotationsachse</param>
        /// <param name="y">y Komponente der Rotationsachse</param>
        /// <param name="z">z Komponente der Rotationsachse</param>
        /// <returns>Prüfung ob die Operation durchgeführt werden konnte</returns>
        public bool Rotate(float degrees, float x, float y, float z)
        {
            if (Id == 0) return false;
            return View.Model.Rotate(Id, degrees, x, y, z);
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
        /// <returns>Prüfung ob die Operation durchgeführt werden konnte</returns>
        public bool Alignment(float fromX, float fromY, float fromZ, float toX, float toY, float toZ)
        {
            if (Id == 0) return false;

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

            //head Positionieren und ausrichten
            if (!View.Model.Position(Id, fromX, fromY, fromZ)) return false;
            if (!View.Model.Rotate(Id, (float)w, (float)xa, (float)ya, (float)za)) return false;

            return true;
        }

        /// <summary>
        /// Positioniert das Modell
        /// </summary>
        /// <param name="x">x Koordinate</param>
        /// <param name="y">y Koordinate</param>
        /// <param name="z">z Koordinate</param>
        /// <returns>Prüfung ob die Operation durchgeführt werden konnte</returns>
        public bool Position(float x, float y, float z)
        {
            if (Id == 0) return false;
            return View.Model.Position(Id, x, y, z);
        }

        /// <summary>
        /// Hängt ein Modell an die Kamera an. Dies hat zur Folge, dass dieses Objekt relativ zur Kamera positioniert wird.
        /// </summary>
        /// <param name="choice">True aktiviert und False deaktiviert die Anhängung an die Kamera.</param>
        /// <returns>Prüfung ob die Operation durchgeführt werden konnte</returns>
        public bool AttachToCamera(bool choice)
        {
            if (Id == 0) return false;
            return View.Model.AttachToCamera(Id, choice);
        }

        /// <summary>
        /// Modell einer Kollisionsgruppe zuweisen
        /// </summary>
        /// <param name="group">Kollisionsgruppe: 1: Kollidieren, 2: Punkte, 3: Hinternise</param>
        /// <returns>Prüfung ob die Operation durchgeführt werden konnte</returns>
        public bool CollisionGroup(uint group)
        {
            if (Id == 0) return false;
            return View.Model.CollisionGroup(Id, group);
        }

        /// <summary>
        /// Sichtbarkeit des Modelles
        /// </summary>
        /// <param name="visible">True wird das Model sichtbar.</param>
        /// <returns>Prüfung ob die Operation durchgeführt werden konnte</returns>
        public bool Visibility(bool visible)
        {
            return View.Model.modelVisibility(Id, visible);
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
            if(Id > 0) View.Model.Dispose(Id);
            Id = 0;
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
