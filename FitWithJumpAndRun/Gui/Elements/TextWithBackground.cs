using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JumpAndRun.Gui.Elements
{
    /// <summary>
    /// Abstrahiert das Button Element der Visualization Library
    /// </summary>
    public class TextWithBackground
    {
        private uint modelId = 0;
        private float x = 0f;
        private float y = 0f;
        private float scaleX = 4f;
        private float scaleY = 0.25f;
        private string text = "Text";
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
        public TextWithBackground(string fontFilename = "data/fonts/JAPAB.TTF")
        {
            int textSize = 50;
            //float textR = 0.796f, TextG = 0.624f, textB = 0.157f, textA = 1f; // kaki
            float textR = 0.784f, TextG = 0.125f, textB = 0.125f, textA = 1f; // rot
            float backgroundR = 0.36f, backgroundG = 0.57f, backgroundB = 0.086f, backgroundA = 1f; // grün

            modelId = View.Model.AddButton(fontFilename);

            while (modelId != 0 && !View.Model.IsCreated(modelId)) { }
            View.Model.Scale(modelId, scaleX, scaleY, 0);
            View.Text.String(modelId, text);
            View.Text.TextColor(modelId, textR, TextG, textB, textA);
            View.Text.TextSize(modelId, textSize);
            View.Model.HighlightColor(modelId, backgroundR, backgroundG, backgroundB, backgroundA);
            View.Model.IsHighlighted(modelId, true);
            View.Model.Position(modelId, 0f, 0f, 0f);

            IsVisible = false;
        }
        /// <summary>
        /// Position setzen
        /// </summary>
        /// <param name="x">x Koordinate (-1 bis 1)</param>
        /// <param name="y">y Koordinate (-1 bis 1)</param>
        public void Position(float x, float y)
        {
            this.x = x;
            this.y = y;
            View.Model.Position(modelId, x, y, 0f);
        }
        /// <summary>
        /// Text ändern
        /// </summary>
        /// <param name="text">zu verwendender Text</param>
        public void setText(string text)
        {
            this.text = text;
            View.Text.String(modelId, text);
        }
        /// <summary>
        /// Textgrösse setzen
        /// </summary>
        /// <param name="points">Grösse in Punkten</param>
        public void Size(int points)
        {
            View.Text.TextSize(modelId, points);

            // Hintergrund mitskalieren
            scaleY = (float)points / 200.0f;
            View.Model.Scale(modelId, scaleX, scaleY, 0);
        }
        /// <summary>
        /// Schriftfarbe setzen
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
        /// Anzeigen
        /// </summary>
        public void Show()
        {
            //View.Model.Position(modelId, x, y, 0f);
            if (!IsVisible)
            {
                IsVisible = true;
            }
        }
        /// <summary>
        /// verstecken
        /// </summary>
        public void Hide()
        {
            //View.Model.Position(modelId, -100f, 0f, 0f);
            if (IsVisible)
            {
                IsVisible = false; 
            }
        }
    }
}
