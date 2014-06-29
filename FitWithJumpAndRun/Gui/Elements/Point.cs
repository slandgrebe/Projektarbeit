using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JumpAndRun.Gui.Elements
{
    /// <summary>
    /// Abstraktion des Point Objekts der Visualization Library
    /// </summary>
    public class Point
    {
        private uint modelId = 0;
        private float x = 0f;
        private float y = 0f;
        private float z = -1f;
        private bool isVisible = false;

        /// <summary>
        /// Sichtbarkeit
        /// </summary>
        public bool IsVisible
        {
            get
            {
                return isVisible;
            }
            private set
            {
                isVisible = value;
                View.Model.modelVisibility(modelId, isVisible);
            }
        }
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="filename">zu verwendendes Bild für die Textur</param>
        public Point(string filename) {
            modelId = View.Model.AddPoint(filename);
            while (modelId != 0 && !View.Model.IsCreated(modelId)) { }
            View.Model.Scale(modelId, 1f, 1f, 1f);
            //View.Model.ScalingIsNormalized(modelId, true);
            IsVisible = false;
        }

        /// <summary>
        /// Skalierung
        /// </summary>
        /// <param name="scaleX">in x Richtung</param>
        /// <param name="scaleY">in y Richtung</param>
        public void Scale(float scaleX, float scaleY)
        {
            View.Model.Scale(modelId, scaleX, scaleY, 1f);
        }
        /// <summary>
        /// Positionierung
        /// </summary>
        /// <param name="x">x</param>
        /// <param name="y">y</param>
        /// <param name="z">z</param>
        public void Position(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            View.Model.Position(modelId, x, y, z);
        }
        /// <summary>
        /// Anhängen des Objekts an die Kamera, damit es sich zusammen mit dieser bewegt. Positionsangaben sind in diesem Fall relativ zur Kamera
        /// </summary>
        /// <param name="choice">Auswahl</param>
        public void AttachToCamera(bool choice)
        {
            View.Model.AttachToCamera(modelId, choice);
        }
        /// <summary>
        /// Objekt anzeigen
        /// </summary>
        public void Show()
        {
            if (!IsVisible)
            {
                IsVisible = true;
            }
            //View.Model.Position(modelId, x, y, z);
        }
        /// <summary>
        /// Objekt verstecken
        /// </summary>
        public void Hide()
        {
            if (IsVisible)
            {
                IsVisible = false;
            }
            //View.Model.Position(modelId, -100f, 0f, 0f);
        }
    }
}
