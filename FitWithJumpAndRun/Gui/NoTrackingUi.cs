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
        /// <summary>ID des Textes</summary>
        private uint textId = 0;
        /// <summary>Zeigt an, ob dieses GUI aktuell aktiv ist, oder nicht.</summary>
        public bool IsShow { get; set; }

        /// <summary>
        /// Initialisiert das GUI, zeigt sie aber nicht an.
        /// </summary>
        public NoTrackingUi()
        {
            IsShow = true;
            
            // Hintergrund erzeugen
            backgroundId = View.Model.AddPoint("data/background/white.jpg");
            while (backgroundId != 0 && !View.Model.IsCreated(backgroundId)) { }
            View.Model.Scale(backgroundId, 10, 10, 1);

            // Text erzeugen
            textId = Text.AddText("data/fonts/arial.ttf");
            while (!Text.IsCreated(textId)) { }
            Text.String(textId, "Keine Person erkannt!");
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
            if (!IsShow)
            {
                Camera.ChangeCameraSpeed(0);
                View.Model.Position(backgroundId, Position, 0f, -0.3f);
                Text.Position(textId, 0, 0, 0);
                Camera.PositionCamera(Position, 0, 0);
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
                Text.Position(textId, -1000, 0, 0);
                IsShow = false;
            }
        }
    }
}
