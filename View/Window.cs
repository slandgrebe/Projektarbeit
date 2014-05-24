using System;
using System.Runtime.InteropServices;

namespace View
{
    /// <summary>
    /// Einbinden der Fensterfunktionen der Visualization.dll in C#
    /// </summary>
    public static class Window
    {
        /// <summary>
        /// Öffnet das Fenster explizit. 
        /// Das Fenster wird immer nur auf dem primären Bildschirm erzeugt. 
        /// </summary>
        /// <param name="windowTitle">Fenstertitel</param>
        /// <param name="fullscreen">Definiert ob das Fenster den gesamten Bildschirm ausfüllt</param>
        /// <param name="windowWidth">Fensterbreite in Pixel. Wird 0 übergeben, wird die native Breite verwendet.</param>
        /// <param name="windowHeight">Fensterhöhe in Pixel. Wird 0 übergeben, wird die native Höhe verwendet.</param>
        /// <returns></returns>
        [DllImport("Visualization.dll", EntryPoint = "init")]
        public extern static bool Init(string windowTitle, bool fullscreen, uint windowWidth, uint windowHeight);
        /// <summary>
        /// Prüft den Zustand der Bibliothek und ob das Fenster offen ist
        /// </summary>
        /// <returns>True wenn das Fenster offen ist, ansonsten False.</returns>
        [DllImport("Visualization.dll", EntryPoint = "isRunning")]
        public extern static bool IsRunning();
        /// <summary>
        /// Sorgt dafür dass das Fenster geschlossen wird.
        /// </summary>
        [DllImport("Visualization.dll", EntryPoint = "close")]
        public extern static void Close();
    }
}
