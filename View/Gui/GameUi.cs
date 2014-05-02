using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View
{
    /// <summary>
    /// Darstellung des GUI innerhalb des Spieles.
    /// </summary>
    public class GameUi
    {
        /// <summary>ID des Textes für die Lebensanzeigen</summary>
        private uint liveId = 0;
        /// <summary>ID des Textes für die erreichten Punkte</summary>
        private uint scoreId = 0;
        /// <summary>Aktuelle anzahl Leben</summary>
        public uint Lives { get; set; }
        /// <summary>Aktuell gesammelte Punkte</summary>
        public uint Score { get; set; }
        /// <summary>Flag zum umschalten zwischen Hide und Show</summary>
        private bool show = true;

        /// <summary>
        /// Initialisiert das GUI, zeigt sie aber nicht an.
        /// </summary>
        public GameUi()
        {
            // Texte erzeugen
            liveId = Text.AddText("data/fonts/arial.ttf");
            while (!Text.IsCreated(liveId)) { }
            scoreId = Text.AddText("data/fonts/arial.ttf");
            while (!Text.IsCreated(scoreId)) { }

            Text.TextSize(liveId, 36);
            Text.TextColor(liveId, 1f, 0f, 0f, 1f);

            Text.TextSize(scoreId, 36);
            Text.TextColor(scoreId, 1f, 0f, 0f, 1f);

            // GUI nicht anzeigen
            Hide();
        }

        /// <summary>
        /// Lebens und Punkteanzeige aktualisieren
        /// </summary>
        public void Update()
        {
            Text.String(liveId, "Lives: " + Lives);
            Text.String(scoreId, "Score: " + Score);
        }

        /// <summary>
        /// GUI anzeigen
        /// </summary>
        public void Show()
        {
            if (!show)
            {
                Text.Position(liveId, -0.7f, 0.7f, 1.0f);
                Text.Position(scoreId, 0.7f, 0.7f, 1.0f);
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
                Text.Position(liveId, -1000, 0, 1); // Z-Koordinate wird ignoriert, da es sich beim Button um ein GUI Element handelt
                Text.Position(scoreId, -1000, 0, 1); // Z-Koordinate wird ignoriert, da es sich beim Button um ein GUI Element handelt
                show = false;
            }
        }
    }
}
