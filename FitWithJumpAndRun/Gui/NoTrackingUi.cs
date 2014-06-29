﻿using System;
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
        private JumpAndRun.Gui.Elements.Text text1 = null;
        private JumpAndRun.Gui.Elements.Text text2 = null;
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
            background = new JumpAndRun.Gui.Elements.Point("data/background/hintergrund.png");
            background.Position(0, 0, -0.55f);
            background.Scale(1.333f, 1f);
            
            // Text erzeugen
            text1 = new JumpAndRun.Gui.Elements.Text();
            text1.setText("Ich kann im Moment");
            text1.Size(60);
            //text.Color(0.502f, 0.082f, 0.082f, 1f);
            text1.Position(0f, 0.2f);

            text2 = new JumpAndRun.Gui.Elements.Text();
            text2.setText("keine Person erkennen.");
            text2.Size(60);
            //text.Color(0.502f, 0.082f, 0.082f, 1f);
            text2.Position(0f, 0f);

            hint = new JumpAndRun.Gui.Elements.Text();
            hint.setText("Stell dich etwa 3-5m vor der Kinect hin");
            hint.Size(44);
            //hint.Color(0.502f, 0.082f, 0.082f, 1f);
            hint.Color(0.784f, 0.125f, 0.125f, 1f); // rot
            hint.Position(0f, -0.4f);

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
            text1.Show(); 
            text2.Show();
            hint.Show();
        }

        /// <summary>
        /// GUI nicht mehr anzeigen
        /// </summary>
        public void Hide()
        {
            background.Hide();
            text1.Hide();
            text2.Hide();
            hint.Hide();
        }
    }
}
