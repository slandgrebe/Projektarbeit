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
        private uint backgroundId = 0;
        /// <summary>Text</summary>
        private View.Text text = null;
        private View.Text hint = null;
        /// <summary>Zeigt an, ob dieses GUI aktuell aktiv ist, oder nicht.</summary>
        public bool IsShown { get; set; }

        /// <summary>
        /// Initialisiert das GUI, zeigt sie aber nicht an.
        /// </summary>
        public NoTrackingUi()
        {
            IsShown = true;
            
            // Hintergrund erzeugen
            backgroundId = View.Model.AddPoint("data/background/white.jpg");
            while (backgroundId != 0 && !View.Model.IsCreated(backgroundId)) { }
            View.Model.Scale(backgroundId, 10, 10, 1);

            // Text erzeugen
            text = new Text("data/fonts/arial.ttf");
            text.setText("Ich kann im Moment keine Person erkennen.");
            text.Size(50);
            text.Color(0f, 0f, 0f, 1f);
            text.Position(0f, 0f);

            hint = new Text("data/fonts/arial.ttf");
            hint.setText("Stell dich etwa 3-5m vor der Kinect hin");
            hint.Size(50);
            hint.Color(0f, 0f, 0f, 1f);
            hint.Position(0f, -0.25f);

            // GUI nicht anzeigen
            Hide();
        }

        /// <summary>
        /// GUI anzeigen
        /// </summary>
        public void Show()
        {
            if (!IsShown)
            {
                Camera.ChangeCameraSpeed(0);
                View.Model.Position(backgroundId, Position, 0f, -0.3f);
                text.Show();
                hint.Show();
                Camera.PositionCamera(Position, 0, 0);
                IsShown = true;
            }
        }

        /// <summary>
        /// GUI nicht mehr anzeigen
        /// </summary>
        public void Hide()
        {
            if (IsShown)
            {
                View.Model.Position(backgroundId, -1000, 0f, -0.3f);
                text.Hide();
                hint.Hide();
                IsShown = false;
            }
        }
    }
}
