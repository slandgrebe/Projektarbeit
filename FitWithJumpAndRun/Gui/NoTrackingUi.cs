using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using View;

namespace JumpAndRun.Gui
{
    /// <summary>
    /// Darstellung des GUI wenn keine Person erkannt wird.
    /// </summary>
    public class NoTrackingUi
    {
        /// <summary>Position des GUIS im Koordinatensystem</summary>
        public float Position { get; set; }
        /// <summary>ID des Hintergrundbildes</summary>
        private View.Point background = null;

        /// <summary>Text</summary>
        private View.Text text = null;
        private View.Text hint = null;

        /// <summary>
        /// Initialisiert das GUI, zeigt sie aber nicht an.
        /// </summary>
        public NoTrackingUi()
        {            
            // Hintergrund erzeugen
            background = new View.Point("data/background/dschungel.png");
            background.Position(0, 0, -0.8f);

            //Camera.PositionCamera(0, 0, 0);
            
            // Text erzeugen
            text = new Text("data/fonts/arial.ttf");
            text.setText("Ich kann im Moment keine Person erkennen.");
            text.Size(60);
            //text.Color(0.502f, 0.082f, 0.082f, 1f);
            text.Position(0f, 0f);

            hint = new Text("data/fonts/arial.ttf");
            hint.setText("Stell dich etwa 3-5m vor der Kinect hin");
            hint.Size(60);
            hint.Color(0.502f, 0.082f, 0.082f, 1f);
            hint.Position(0f, -0.25f);

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
            hint.Show();
        }

        /// <summary>
        /// GUI nicht mehr anzeigen
        /// </summary>
        public void Hide()
        {
            background.Hide();
            text.Hide();
            hint.Hide();
        }
    }
}
