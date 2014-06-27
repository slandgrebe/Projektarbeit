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
        private static NoTrackingUi instance = null;

        /// <summary>ID des Hintergrundbildes</summary>
        private JumpAndRun.Gui.Elements.Point background = null;

        /// <summary>Text</summary>
        private JumpAndRun.Gui.Elements.Text text = null;
        private JumpAndRun.Gui.Elements.Text hint = null;

        /// <summary>
        /// Singleton
        /// </summary>
        public static NoTrackingUi Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new NoTrackingUi();
                }
                return instance;
            }
        }

        /// <summary>
        /// Initialisiert das GUI, zeigt sie aber nicht an.
        /// </summary>
        private NoTrackingUi()
        {            
            // Hintergrund erzeugen
            background = new JumpAndRun.Gui.Elements.Point("data/background/dschungel.png");
            background.Position(0, 0, -0.8f);

            //Camera.PositionCamera(0, 0, 0);
            
            // Text erzeugen
            text = new JumpAndRun.Gui.Elements.Text();
            text.setText("Ich kann im Moment keine Person erkennen.");
            text.Size(40);
            //text.Color(0.502f, 0.082f, 0.082f, 1f);
            text.Position(0f, 0f);

            hint = new JumpAndRun.Gui.Elements.Text();
            hint.setText("Stell dich etwa 3-5m vor der Kinect hin");
            hint.Size(40);
            //hint.Color(0.502f, 0.082f, 0.082f, 1f);
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
