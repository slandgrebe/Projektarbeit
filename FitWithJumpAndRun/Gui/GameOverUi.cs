using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using View;

namespace JumpAndRun.Gui
{
    /// <summary>
    /// Darstellung des GUI nach einem Game Over.
    /// </summary>
    public class GameOverUi
    {
        /// <summary>ID des Hintergrundbildes</summary>
        private View.Point background = null;
        /// <summary>ID des Textes</summary>
        private View.Text text = null;
        /// <summary>ID des Buttons</summary>
        private View.Button button = null;
        /// <summary>ID des Cursors</summary>
        private uint cursorId = 0;

        /// <summary>
        /// Initialisiert das GUI, zeigt sie aber nicht an.
        /// </summary>
        public GameOverUi()
        {
            // Hintergrund erzeugen
            background = new View.Point("data/background/white.jpg");
            background.Scale(2, 2);

            // Text erzeugen
            text = new View.Text("data/fonts/arial.ttf");
            text.setText("Game over!");
            text.Position(0, 0.5f);

            // Button erzeugen
            button = new View.Button("data/fonts/arial.ttf");
            button.Text("Nochmal");
            
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
            // Kamera zurücksetzen
            Camera.ChangeCameraSpeed(0);
            Camera.PositionCamera(0, 0, 0);

            background.Show();
            text.Show();
            button.Show();

            View.Model.AttachToCamera(cursorId, true);
        }

        /// <summary>
        /// GUI nicht mehr anzeigen
        /// </summary>
        public void Hide()
        {
            background.Hide();
            text.Hide();
            button.Hide();

            View.Model.Position(cursorId, -1000, 0f, -0.2f);
        }

        /// <summary>
        /// Positioniert den Cursor
        /// </summary>
        /// <param name="x">X Koordinate</param>
        /// <param name="y">Y Koordinate</param>
        public void PositionCursor(float x, float y)
        {
            /*View.Model.Position(cursorId, x, y, -0.2f);
            if (HoverButton(x, y))
            {
                View.Model.HighlightColor(buttonId, 1f, 1f, 0f, 1f);
            }
            else
            {
                View.Model.HighlightColor(buttonId, 0.5f, 0f, 0f, 1f);
            }*/
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
