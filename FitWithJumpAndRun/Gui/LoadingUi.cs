using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using View;

namespace JumpAndRun.Gui
{
    /// <summary>
    /// Ladebildschirm des Spieles
    /// </summary>
    public class LoadingUi
    {
        /// <summary>ID des Hintergrundbildes</summary>
        private View.Point background = null;
        /// <summary>ID des Textes</summary>
        private View.Text text = null;

        /// <summary>
        /// Initialisiert das GUI, zeigt sie aber nicht an.
        /// </summary>
        public LoadingUi()
        {
            // Hintergrund erzeugen
            background = new View.Point("data/background/white.jpg");
            background.Position(0, 0, -0.8f);
            background.Scale(2, 2);

            // Text erzeugen
            text = new View.Text("data/fonts/arial.ttf");
            text.setText("Level wird geladen");
            text.Size(50);

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
        }

        /// <summary>
        /// GUI nicht mehr anzeigen
        /// </summary>
        public void Hide()
        {
            background.Hide();
            text.Hide();
        }
    }
}
