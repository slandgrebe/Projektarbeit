using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using View;

namespace View
{
    /// <summary>
    /// Ladebildschirm des Spieles
    /// </summary>
    public class LoadingUi
    {
        /// <summary>Position des GUIS im Koordinatensystem</summary>
        public float Position { get; set; }
        /// <summary>ID des Hintergrundbildes</summary>
        private uint backgroundId = 0;
        /// <summary>ID des Textes</summary>
        private uint textId = 0;
        /// <summary>Flag zum umschalten zwischen Hide und Show</summary>
        private bool show = true;

        /// <summary>
        /// Initialisiert das GUI, zeigt sie aber nicht an.
        /// </summary>
        public LoadingUi()
        {
            // Hintergrund erzeugen
            backgroundId = Model.AddPoint("data/background/white.jpg");
            while (backgroundId != 0 && !Model.IsCreated(backgroundId)) { }
            Model.Scale(backgroundId, 10, 10, 1);

            // Text erzeugen
            textId = Text.AddText("data/fonts/arial.ttf");
            while (!Text.IsCreated(textId)) { }
            Text.String(textId, "Level wird geladen");
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
                Model.Position(backgroundId, Position, 0f, -0.3f);
                Text.Position(textId, 0, 0, 0);
                Camera.PositionCamera(Position, 0, 0);
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
                Text.Position(textId, -1000, 0, 0);
                show = false;
            }
        }
    }
}
