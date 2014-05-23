using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using View;

namespace Controller
{
    /// <summary>
    /// Darstellung des Hauptmenus
    /// </summary>
    public class MenuUi
    {
        /// <summary>Position des GUIS im Koordinatensystem</summary>
        public float Position { get; set; }
        /// <summary>ID des Hintergrundbildes</summary>
        private uint backgroundId = 0;
        /// <summary>ID des Cursors</summary>
        private uint cursorId = 0;
        /// <summary>ID des Buttons</summary>
        private uint buttonId = 0;
        /// <summary>Zeigt an, ob dieses GUI aktuell aktiv ist, oder nicht.</summary>
        public bool IsShow { get; set; }

        /// <summary>
        /// Initialisiert das GUI, zeigt sie aber nicht an.
        /// </summary>
        public MenuUi()
        {
            IsShow = true;

            // Hintergrund erzeugen
            backgroundId = View.Model.AddPoint("data/background/white.jpg");
            while (backgroundId != 0 && !View.Model.IsCreated(backgroundId)) { }
            View.Model.Scale(backgroundId, 10, 10, 1);

            // Button erzeugen
            buttonId = View.Model.AddButton("data/fonts/arial.ttf");
            while (!View.Model.IsCreated(buttonId)) { }
            View.Model.Scale(buttonId, 1f, 0.5f, 1); // Skalierung in z-Richtung wird ignoriert, da es sich beim Button um ein GUI Element handelt
            Text.String(buttonId, "Start");
            Text.TextColor(buttonId, 1f, 1f, 1f, 1.0f);
            Text.TextSize(buttonId, 70);
            View.Model.HighlightColor(buttonId, 0.5f, 0f, 0f, 1f);
            View.Model.IsHighlighted(buttonId, true);

            // Cursor erzeugen
            cursorId = View.Model.AddPoint("data/models/hand/hand-stop-2.jpg");
            while (cursorId != 0 && !View.Model.IsCreated(cursorId)) { }
            View.Model.Scale(cursorId, 0.03f, 0.05f, 1);

            // GUI nicht anzeigen
            Hide();
        }

        /// <summary>
        /// GUI anzeigen
        /// </summary>
        public void Show()
        {
            if (!IsShow)
            {
                Camera.ChangeCameraSpeed(0);
                View.Model.Position(backgroundId, Position, 0f, -0.3f);
                View.Model.Position(buttonId, 0, 0, 1); // Z-Koordinate wird ignoriert, da es sich beim Button um ein GUI Element handelt
                Camera.PositionCamera(Position, 0, 0);
                View.Model.AttachToCamera(cursorId, true);
                IsShow = true;
            }
        }

        /// <summary>
        /// GUI nicht mehr anzeigen
        /// </summary>
        public void Hide()
        {
            if (IsShow)
            {
                View.Model.Position(backgroundId, -1000, 0f, -0.3f);
                View.Model.Position(buttonId, -1000, 0f, 1); // Z-Koordinate wird ignoriert, da es sich beim Button um ein GUI Element handelt
                View.Model.Position(cursorId, -1000, 0f, -0.2f);
                IsShow = false;
            }
        }

        /// <summary>
        /// Positioniert den Cursor
        /// </summary>
        /// <param name="x">X Koordinate</param>
        /// <param name="y">Y Koordinate</param>
        public void PositionCursor(float x, float y)
        {
            View.Model.Position(cursorId, x, y, -0.2f);
            if (HoverButton(x, y))
            {
                View.Model.HighlightColor(buttonId, 1f, 1f, 0f, 1f);
            }
            else
            {
                View.Model.HighlightColor(buttonId, 0.5f, 0f, 0f, 1f);
            }
        }

        /// <summary>
        /// Überprüft, ob sich innerhalb des Buttons befindet.
        /// </summary>
        /// <param name="x">X Koordinate des Cursors</param>
        /// <param name="y">Y Koordinate des Cursors</param>
        /// <returns>True, wenn sich der Cursor sich innerhalb des Buttons befindet</returns>
        public bool HoverButton(float x, float y)
        {
            if (x > -0.04 && x < 0.04 && y > -0.025 && y < 0.025)
            {
                return true;
            }
            return false;
        }
    }
}