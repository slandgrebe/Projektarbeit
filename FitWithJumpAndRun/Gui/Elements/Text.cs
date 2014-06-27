using System;
using System.Runtime.InteropServices;

namespace JumpAndRun.Gui.Elements
{
    /// <summary>
    /// Abstraktion des Text Objekts der Visualization Library
    /// </summary>
    public class Text
    {
        private uint modelId = 0;
        private float x = 0f;
        private float y = 0f;
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
        /// <param name="fontFilename">zu verwendende Schriftart</param>
        public Text(string fontFilename = "data/fonts/JAPAB.TTF")
        {
            modelId = View.Text.AddText(fontFilename);

            while (modelId != 0 && !View.Model.IsCreated(modelId)) { }
            View.Text.String(modelId, "Text");
            View.Text.TextSize(modelId, 36);
            View.Text.Position(modelId, x, y, 0f);
            View.Text.TextColor(modelId, 0.784f, 0.125f, 0.125f, 1f);
            IsVisible = false;
        }

        /// <summary>
        /// Anzuzeigender Text ändern
        /// </summary>
        /// <param name="text">anzuzeigender Text</param>
        public void setText(string text)
        {
            View.Text.String(modelId, text);
        }
        /// <summary>
        /// Schriftgrösse in Punkten
        /// </summary>
        /// <param name="points">Schriftgrösse</param>
        public void Size(int points)
        {
            View.Text.TextSize(modelId, points);
        }
        /// <summary>
        /// Positionierung
        /// </summary>
        /// <param name="x">x (-1 bis 1)</param>
        /// <param name="y">y (-1 bis 1)</param>
        public void Position(float x, float y)
        {
            this.x = x;
            this.y = y;
            View.Text.Position(modelId, x, y, 0f);
        }
        /// <summary>
        /// Schriftfarbe
        /// </summary>
        /// <param name="r">rot</param>
        /// <param name="g">grün</param>
        /// <param name="b">balu</param>
        /// <param name="a">alpha</param>
        public void Color(float r, float g, float b, float a)
        {
            View.Text.TextColor(modelId, r, g, b, a);
        }
        /// <summary>
        /// Text anzeigen
        /// </summary>
        public void Show()
        {
            if (!IsVisible)
            {
                IsVisible = true;
            }
            //View.Text.Position(modelId, x, y, 0f);
        }
        /// <summary>
        /// Text verstecken
        /// </summary>
        public void Hide()
        {
            if (IsVisible)
            {
                IsVisible = false;
            }
            //View.Text.Position(modelId, -100f, 0f, 0f);
        }
    }
}
