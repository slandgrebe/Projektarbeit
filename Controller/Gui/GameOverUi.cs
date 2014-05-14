using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using View;

namespace View
{
    /// <summary>
    /// Darstellung des GUI nach einem Game Over.
    /// </summary>
    public class GameOverUi
    {
        /// <summary>Position des GUIS im Koordinatensystem</summary>
        public float Position { get; set; }
        /// <summary>ID des Hintergrundbildes</summary>
        private uint backgroundId = 0;
        /// <summary>ID des Cursors</summary>
        private uint cursorId = 0;
        /// <summary>ID des Buttons</summary>
        private uint buttonId = 0;
        /// <summary>ID des Textes</summary>
        private uint textId = 0;
        /// <summary>Flag zum umschalten zwischen Hide und Show</summary>
        private bool show = true;

        /// <summary>
        /// Initialisiert das GUI, zeigt sie aber nicht an.
        /// </summary>
        public GameOverUi()
        {
            // Hintergrund erzeugen
            backgroundId = Model.AddPoint("data/background/white.jpg");
            while (backgroundId != 0 && !Model.IsCreated(backgroundId)) { }
            Model.Scale(backgroundId, 10, 10, 1);

            // Button erzeugen
            buttonId = Model.AddButton("data/fonts/arial.ttf");
            while (!Model.IsCreated(buttonId)) { }
            Model.Scale(buttonId, 1f, 0.5f, 1); // Skalierung in z-Richtung wird ignoriert, da es sich beim Button um ein GUI Element handelt
            Text.String(buttonId, "Nochmal");
            Text.TextColor(buttonId, 1f, 1f, 1f, 1.0f);
            Text.TextSize(buttonId, 70);
            Model.HighlightColor(buttonId, 0.5f, 0f, 0f, 1f);
            Model.IsHighlighted(buttonId, true);
            
            // Cursor erzeugen
            cursorId = Model.AddPoint("data/models/hand/hand-stop-2.jpg");
            while (cursorId != 0 && !Model.IsCreated(cursorId)) { }
            Model.Scale(cursorId, 0.03f, 0.05f, 1);

            // Text erzeugen
            textId = Text.AddText("data/fonts/arial.ttf");
            while (!Text.IsCreated(textId)) { }
            Text.String(textId, "GameOver!");
            Text.TextSize(textId, 50);
            Text.TextColor(textId, 0f, 0f, 0f, 1.0f);

            // GUI nicht anzeigen
            Hide();
        }

        /// <summary>
        /// GUI anzeigen
        /// </summary>
        public void Show()
        {
            if (!show)
            {
                Camera.ChangeCameraSpeed(0);
                Model.Position(backgroundId, Position, 0f, -0.3f);
                Model.Position(buttonId, 0, 0, 1); // Z-Koordinate wird ignoriert, da es sich beim Button um ein GUI Element handelt
                Camera.PositionCamera(Position, 0, 0);
                Model.AttachToCamera(cursorId, true);
                Text.Position(textId, 0, 0.5f, 0);
                show = true;
            }
        }

        /// <summary>
        /// GUI nicht mehr anzeigen
        /// </summary>
        public void Hide()
        {
            if (show)
            {
                Model.Position(backgroundId, -1000, 0f, -0.3f);
                Model.Position(buttonId, -1000, 0f, 1); // Z-Koordinate wird ignoriert, da es sich beim Button um ein GUI Element handelt
                Model.Position(cursorId, -1000, 0f, -0.2f);
                Text.Position(textId, -1000, 0f, 0);
                show = false;
            }
        }

        /// <summary>
        /// Positioniert den Cursor
        /// </summary>
        /// <param name="x">X Koordinate</param>
        /// <param name="y">Y Koordinate</param>
        public void PositionCursor(float x, float y)
        {
            Model.Position(cursorId, x, y, -0.2f);
            if (HoverButton(x, y))
            {
                Model.HighlightColor(buttonId, 1f, 1f, 0f, 1f);
            }
            else
            {
                Model.HighlightColor(buttonId, 0.5f, 0f, 0f, 1f);
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
