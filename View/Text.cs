using System;
using System.Runtime.InteropServices;

namespace View
{
    /// <summary>
    /// Einbinden der Textfunktionen der Visualization.dll in C#
    /// </summary>
    public class Text
    {
        /// <summary>
        /// Fügt ein Textelement hinzu. Hierbei handelt sich um ein GUI Element.
        /// </summary>
        /// <param name="fontFilename">Dateipfad der Schriftart. Es werden Truetype und Opentype Schriften unterstützt.</param>
        /// <returns>ID des Text Objektes</returns>
        [DllImport("Visualization.dll", EntryPoint = "addText")]
        public extern static uint AddText(string fontFilename);
        /// <summary>
        /// Ändert den darzustellenden Text Diese Methode kann für Text und Button Objekte verwendet werden. 
        /// </summary>
        /// <param name="textId">ID des Modells</param>
        /// <param name="text">Der neu darzustellende Text</param>
        /// <returns>Prüfung ob die Operation durchgeführt werden konnte</returns>
        [DllImport("Visualization.dll", EntryPoint = "text")]
        public extern static bool String(uint textId, string text);
        /// <summary>
        /// Positioniert ein Modell. Kann mit allen Arten von Modellen umgehen, allerdings werden diese unterschiedlich behandelt. Model und Point werden relativ zum Ursprung in einem normalen, dreidimensionalem, kartesischem Koordinatensystem positioniert. Bei Text und Button handelt es sich um GUI Elemente, weswegen hier die z-Koordinate ignoriert wird. X- und Y-Koordinaten gehen von -1 bis +1 wobei -1 dem linken bzw. unteren Rand und +1 dem rechten bzw. oberen Rand entspricht. 
        /// </summary>
        /// <param name="modelId">Id des Modells</param>
        /// <param name="x">x Koordinate</param>
        /// <param name="y">y Koordinate</param>
        /// <param name="z">z Koordinate</param>
        /// <returns>Prüfung ob die Operation durchgeführt werden konnte</returns>
        [DllImport("Visualization.dll", EntryPoint = "position")]
        public extern static bool Position(uint textId, float x, float y, float z);
        /// <summary>
        /// Prüft ob das Modell existiert. Diese Methode kann für alle Arten von Modellen verwendet werden (Model, Point, Text, Button). Bevor ein Modell bearbeitet wird, sollte sichergestellt werden, dass das Modell existiert. Dies hat den Hintergrund, dass die Modelle effektiv in einem separaten Thread erstellt werden. 
        /// </summary>
        /// <param name="modelId">ID des zu prüfenden Modells</param>
        /// <returns>Liefert True wenn das Objekt existiert, ansonsten False</returns>
        [DllImport("Visualization.dll", EntryPoint = "isCreated")]
        public extern static bool IsCreated(uint textId);
        /// <summary>
        /// Ändert die Grösse des Textes. Die Grössenangabe erfolgt in Punkten (http://de.wikipedia.org/wiki/Schriftgrad). Diese Methode kann für Text und Button Objekte verwendet werden.
        /// </summary>
        /// <param name="textId">ID des Modells</param>
        /// <param name="points">Die neue Grösse.</param>
        /// <returns>Prüfung ob die Operation durchgeführt werden konnte</returns>
        [DllImport("Visualization.dll", EntryPoint = "textSize")]
        public extern static bool TextSize(uint textId, int points);
        /// <summary>
        /// Ändert die Farbe des Textes. Diese Methode kann für Text und Button Objekte verwendet werden. 
        /// Die Farbe wird durch 4 Komponenten definiert: Rot, Grün, Blau, Alpha 
        /// Alle Komponenten Sollten einen Wert von 0 bis 1 haben, wobei der Wert 0 0% und der Wert 1 100% entspricht. 
        /// </summary>
        /// <param name="textId">ID des Modells</param>
        /// <param name="r">Rot Komponente der Farbe</param>
        /// <param name="g">Grün Komponente der Farbe</param>
        /// <param name="b">Blau Komponente der Farbe</param>
        /// <param name="a">Alpha Komponente (Undurchsichtigkeit) der Farbe</param>
        /// <returns>Prüfung ob die Operation durchgeführt werden konnte</returns>
        [DllImport("Visualization.dll", EntryPoint = "textColor")]
        public extern static bool TextColor(uint textId, float r, float g, float b, float a);
        /// <summary>
        /// Markiert ein Text zur Entfernung.
        /// </summary>
        /// <param name="modelId">ID des zu entfernenden Modells</param>
        [DllImport("Visualization.dll", EntryPoint = "dispose")]
        public extern static void Dispose(uint textId);


        private uint modelId = 0;
        private float x = 0f;
        private float y = 0f;
        public Text(string fontFilename)
        {
            modelId = View.Text.AddText(fontFilename);

            while (modelId != 0 && !View.Model.IsCreated(modelId)) { }
            View.Text.String(modelId, "Text");
            View.Text.TextSize(modelId, 36);
            View.Text.Position(modelId, x, y, 0f);
            View.Text.TextColor(modelId, 0.502f, 0.082f, 0.082f, 1f);
        }

        public void setText(string text)
        {
            View.Text.String(modelId, text);
        }
        public void Size(int points)
        {
            View.Text.TextSize(modelId, points);
        }
        public void Position(float x, float y)
        {
            this.x = x;
            this.y = y;
            View.Text.Position(modelId, x, y, 0f);
        }
        public void Color(float r, float g, float b, float a)
        {
            View.Text.TextColor(modelId, r, g, b, a);
        }
        public void Show()
        {
            View.Text.Position(modelId, x, y, 0f);
        }
        public void Hide()
        {
            View.Text.Position(modelId, -100f, 0f, 0f);
        }
    }
}
