using System;
using System.Runtime.InteropServices;

namespace View
{
    /// <summary>
    /// Einbinden der Modellfunktionen der Visualization.dll in C#
    /// </summary>
    public static class Model
    {
        /// <summary>
        /// Fügt ein 3D Modell hinzu
        /// </summary>
        /// <param name="filename">Dateipfad des 3D Modells</param>
        /// <returns>ID des Modells. Diese wird für die weitere Arbeit mit diesem Modell benötigt.</returns>
        [DllImport("Visualization.dll", EntryPoint = "addModel")]
        public extern static uint AddModel(string filename);
        /// <summary>
        /// Fügt eine Fläche welche von einem Bild ausgefüllt wird hinzu.
        /// </summary>
        /// <param name="textureFilename">Dateipfad des Bildes</param>
        /// <returns>ID des Modells</returns>
        [DllImport("Visualization.dll", EntryPoint = "addPoint")]
        public extern static uint AddPoint(string textureFilename);
        /// <summary>
        /// Fügt ein Button Objekt hinzu.
        /// </summary>
        /// <param name="fontname">Dateipfad zur Schriftart.</param>
        /// <returns>Liefert die modelId zurück.</returns>
        [DllImport("Visualization.dll", EntryPoint = "addButton")]
        public extern static uint AddButton(string fontname);
        /// <summary>
        /// Prüft ob das Modell existiert. Diese Methode kann für alle Arten von Modellen verwendet werden (Model, Point, Text, Button). Bevor ein Modell bearbeitet wird, sollte sichergestellt werden, dass das Modell existiert. Dies hat den Hintergrund, dass die Modelle effektiv in einem separaten Thread erstellt werden. 
        /// </summary>
        /// <param name="modelId">ID des zu prüfenden Modells</param>
        /// <returns>Liefert True wenn das Objekt existiert, ansonsten False</returns>

        [DllImport("Visualization.dll")]
        public extern static uint addText(string fontFilename);

        [DllImport("Visualization.dll", EntryPoint = "isCreated")]
        public extern static bool IsCreated(uint modelId);
        /// <summary>
        /// Markiert ein Modell zur Entfernung. Diese Methode kann für alle Arten von Modellen verwendet werden (Model, Point, Text, Button).
        /// </summary>
        /// <param name="modelId">ID des zu entfernenden Modells</param>
        [DllImport("Visualization.dll", EntryPoint = "dispose")]
        public extern static void Dispose(uint modelId);
        /// <summary>
        /// Positioniert ein Modell. Kann mit allen Arten von Modellen umgehen, allerdings werden diese unterschiedlich behandelt. Model und Point werden relativ zum Ursprung in einem normalen, dreidimensionalem, kartesischem Koordinatensystem positioniert. Bei Text und Button handelt es sich um GUI Elemente, weswegen hier die z-Koordinate ignoriert wird. X- und Y-Koordinaten gehen von -1 bis +1 wobei -1 dem linken bzw. unteren Rand und +1 dem rechten bzw. oberen Rand entspricht. 
        /// </summary>
        /// <param name="modelId">Id des Modells</param>
        /// <param name="x">x Koordinate</param>
        /// <param name="y">y Koordinate</param>
        /// <param name="z">z Koordinate</param>
        /// <returns>Prüfung ob die Operation durchgeführt werden konnte</returns>
        [DllImport("Visualization.dll", EntryPoint = "position")]
        public extern static bool Position(uint modelId, float x, float y, float z);
        /// <summary>
        /// Liefert die x-Komponente der Position eines Objekts. Kann mit allen Arten von Modellen umgehen, allerdings werden diese unterschiedlich behandelt. Model und Point werden relativ zum Ursprung in einem normalen, dreidimensionalem, kartesischem Koordinatensystem positioniert. Bei Text und Button handelt es sich um GUI Elemente, weswegen hier die z-Koordinate ignoriert wird. X- und Y-Koordinaten gehen von -1 bis +1 wobei -1 dem linken bzw. unteren Rand und +1 dem rechten bzw. oberen Rand entspricht. 
        /// </summary>
        /// <param name="modelId">ID des Modells</param>
        /// <returns>x-Komponente der Position eines Objekts</returns>
        [DllImport("Visualization.dll", EntryPoint = "positionX")]
        public extern static float PositionX(uint modelId);
        /// <summary>
        /// Liefert die x-Komponente der Position eines Objekts. Kann mit allen Arten von Modellen umgehen, allerdings werden diese unterschiedlich behandelt. Model und Point werden relativ zum Ursprung in einem normalen, dreidimensionalem, kartesischem Koordinatensystem positioniert. Bei Text und Button handelt es sich um GUI Elemente, weswegen hier die z-Koordinate ignoriert wird. X- und Y-Koordinaten gehen von -1 bis +1 wobei -1 dem linken bzw. unteren Rand und +1 dem rechten bzw. oberen Rand entspricht. 
        /// </summary>
        /// <param name="modelId">ID des Modells</param>
        /// <returns>y-Komponente der Position eines Objekts</returns>
        [DllImport("Visualization.dll", EntryPoint = "positionY")]
        public extern static float PositionY(uint modelId);
        /// <summary>
        /// Liefert die x-Komponente der Position eines Objekts. Kann mit allen Arten von Modellen umgehen, allerdings werden diese unterschiedlich behandelt. Model und Point werden relativ zum Ursprung in einem normalen, dreidimensionalem, kartesischem Koordinatensystem positioniert. Bei Text und Button handelt es sich um GUI Elemente, weswegen hier die z-Koordinate ignoriert wird. X- und Y-Koordinaten gehen von -1 bis +1 wobei -1 dem linken bzw. unteren Rand und +1 dem rechten bzw. oberen Rand entspricht. 
        /// </summary>
        /// <param name="modelId">ID des Modells</param>
        /// <returns>z-Komponente der Position eines Objekts</returns>
        [DllImport("Visualization.dll", EntryPoint = "positionZ")]
        public extern static float PositionZ(uint modelId);
        /// <summary>
        /// Rotiert ein Modell. Diese Methode kann nur für Model und Point verwendet werden.
        /// </summary>
        /// <param name="modelId">ID des Modells</param>
        /// <param name="degrees">Rotationswinkel in Grad</param>
        /// <param name="x">x Komponente der Rotationsachse</param>
        /// <param name="y">y Komponente der Rotationsachse</param>
        /// <param name="z">z Komponente der Rotationsachse</param>
        /// <returns></returns>
        [DllImport("Visualization.dll", EntryPoint = "rotate")]
        public extern static bool Rotate(uint modelId, float degrees, float x, float y, float z);
        /// <summary>
        /// skaliert das Modell auf die angegebene Grösse Diese Methode kann nicht für Text Modelle verwendet werden. Für Texte kann die textSize Methode verwendet werden. 
        /// </summary>
        /// <param name="modelId">ID des Modells</param>
        /// <param name="x">Skalierung auf der x-Achse</param>
        /// <param name="y">Skalierung auf der y-Achse</param>
        /// <param name="z">Skalierung auf der z-Achse</param>
        /// <returns>Prüfung ob die Operation durchgeführt werden konnte</returns>
        [DllImport("Visualization.dll", EntryPoint = "scale")]
        public extern static bool Scale(uint modelId, float x, float y, float z);
        /// <summary>
        /// Definiert den Zustand der Skalierungsnormalisierung. 
        /// Die Skalierungnormalisierung sorgt dafür, dass die Boundingsphere (kleinstmögliche Kugel welche denselben Ursprung wie das Modell hat und das gesamte Modell umschliesst) vor der Veränderung der Skalierung einen Durchmesser von 1m (bzw. 1 Einheit) hat. Dadurch kann mit der scale() Methode die Grösse des Modells in Metern angegeben werden. Standardmässig ist dieses Verhalten deaktiviert. 
        /// Diese Methode kann nur für Model und Point Objekte verwendet werden. 
        /// </summary>
        /// <param name="modelId">ID des Modells</param>
        /// <param name="choice">True aktiviert und False deaktiviert die Skalierungsnormalisierung</param>
        /// <returns>Prüfung ob die Operation durchgeführt werden konnte</returns>
        [DllImport("Visualization.dll", EntryPoint = "scalingIsNormalized")]
        public extern static bool ScalingIsNormalized(uint modelId, bool choice);
        /// <summary>
        /// Setzt die Hervorhebungsfarbe für dieses Objekt. Diese Methode kann nicht für Text Objekte verwendet werden. 
        /// Die Farbe wird durch 4 Komponenten definiert: Rot, Grün, Blau, Alpha 
        /// </summary>
        /// <param name="modelId">ID des Modells</param>
        /// <param name="r">Rot Komponente der Farbe</param>
        /// <param name="g">Grün Komponente der Farbe</param>
        /// <param name="b">Blau Komponente der Farbe</param>
        /// <param name="a">Alpha Komponente (Undurchsichtigkeit) der Farbe</param>
        /// <returns>Prüfung ob die Operation durchgeführt werden konnte</returns>
        [DllImport("Visualization.dll", EntryPoint = "highlightColor")]
        public extern static bool HighlightColor(uint modelId, float r, float g, float b, float a);
        /// <summary>
        /// Ändert den Status der Hervorhebung dieses Objektes. Zur Hervorhebung wird die Hervorhebungsfarbe verwendet. 
        /// </summary>
        /// <param name="modelId">ID des Modells</param>
        /// <param name="choice">True aktiviert und False deaktiviert die Hervorhebung.</param>
        /// <returns>Prüfung ob die Operation durchgeführt werden konnte</returns>
        [DllImport("Visualization.dll", EntryPoint = "isHighlighted")]
        public extern static bool IsHighlighted(uint modelId, bool choice);
        /// <summary>
        /// 
        /// Hängt ein Modell an die Kamera an. Dies hat zur Folge, dass dieses Objekt relativ zur Kamera positioniert wird. Diese Methode kann nur für Modelle und Punkte verwendet werden. 
        /// </summary>
        /// <param name="modelId">ID des Modells</param>
        /// <param name="choice">True aktiviert und False deaktiviert die Anhängung an die Kamera.</param>
        /// <returns>Prüfung ob die Operation durchgeführt werden konnte</returns>
        [DllImport("Visualization.dll", EntryPoint = "attachToCamera")]
        public extern static bool AttachToCamera(uint modelId, bool choice);

        /// <summary>
        /// Länge des Textes mit allen erkannten Kollisionen
        /// </summary>
        /// <returns>Länge des Textes</returns>
        [DllImport("Visualization.dll", EntryPoint = "collisionsTextLength")]
        public extern static uint CollisionsTextLength();
        /// <summary>
        /// Text mit allen Kollisionen. 
        /// Die Kollisionserkennung findet nur für Objekte welche mit der addModel Methode hinzugefügt wurden statt. 
        /// Format: 1:2,3;2:1;3:1;4: 
        /// Bedeutung: Objekt mit der ID 1 kollidiert mit Objekt 2 und 3 
        /// Objekt 2 kollidiert mit Objekt 1 
        /// Objekt 3 kollidiert mit Objekt 1 
        /// Objekt 4 kollidiert mit nichts.
        /// </summary>
        /// <param name="text">Das zu beschreibende String Objekt</param>
        /// <param name="length">Die Anzahl Zeichen welche in den String kopiert werden sollen</param>
        /// <returns>Prüfung ob die Operation durchgeführt werden konnte</returns>
        [DllImport("Visualization.dll", EntryPoint = "collisionsText")]
        public extern static bool CollisionsText(System.Text.StringBuilder text, int length);
        [DllImport("Visualization.dll", EntryPoint = "addCollisionModel")]
        public extern static bool AddCollisionModel(uint modelId, string filename);
        [DllImport("Visualization.dll", EntryPoint = "collisionGroup")]
        public extern static bool CollisionGroup(uint modelId, uint collisionGroup);
    }
}
