using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JumpAndRun.Gui
{
    /// <summary>
    /// GUI für die Darstellung wenn die Kinect nicht angeschlossen bzw. gestartet wird.
    /// </summary>
    class KinectUi
    {
        private static KinectUi instance = null;
        private JumpAndRun.Gui.Elements.Text text = null;
        /// <summary>Hintergrundbild</summary>
        private JumpAndRun.Gui.Elements.Point background = null;

        /// <summary>
        /// Singleton
        /// </summary>
        public static KinectUi Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new KinectUi();
                }
                return instance;
            }
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        private KinectUi()
        {
            background = new JumpAndRun.Gui.Elements.Point("data/background/hintergrund.png");
            background.Position(0, 0, -0.55f);

            text = new JumpAndRun.Gui.Elements.Text();
            text.setText("Kinect wird gestartet");
            text.Size(50);
        }

        /// <summary>
        /// GUI anzeigen
        /// </summary>
        public void Show()
        {
            background.Show();
            text.Show();
        }

        /// <summary>
        /// GUI ausblenden
        /// </summary>
        public void Hide()
        {
            background.Hide();
            text.Hide();
        }

        /// <summary>
        /// Darzustellender Text auf dem GUI ändern
        /// </summary>
        /// <param name="text">darzustellender Text</param>
        public void SetText(string text)
        {
            this.text.setText(text);
        }
    }
}
