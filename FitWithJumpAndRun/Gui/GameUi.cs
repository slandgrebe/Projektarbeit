﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using View;

namespace JumpAndRun.Gui
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
        /// <summary>Zeigt an, ob dieses GUI aktuell aktiv ist, oder nicht.</summary>
        public bool IsShow { get; set; }

        /// <summary>
        /// Initialisiert das GUI, zeigt sie aber nicht an.
        /// </summary>
        public GameUi()
        {
            IsShow = true;

            // Texte erzeugen
            liveId = View.Text.AddText("data/fonts/arial.ttf");
            while (!View.Text.IsCreated(liveId)) { }
            scoreId = View.Text.AddText("data/fonts/arial.ttf");
            while (!View.Text.IsCreated(scoreId)) { }

            View.Text.TextSize(liveId, 36);
            View.Text.TextColor(liveId, 1f, 0f, 0f, 1f);

            View.Text.TextSize(scoreId, 36);
            View.Text.TextColor(scoreId, 1f, 0f, 0f, 1f);

            // GUI nicht anzeigen
            Hide();
        }

        /// <summary>
        /// Lebens und Punkteanzeige aktualisieren
        /// </summary>
        public void Update()
        {
            View.Text.String(liveId, "Lives: " + Lives);
            View.Text.String(scoreId, "Score: " + Score);
        }

        /// <summary>
        /// GUI anzeigen
        /// </summary>
        public void Show()
        {
            if (!IsShow)
            {
                View.Text.Position(liveId, -0.7f, 0.7f, 1.0f);
                View.Text.Position(scoreId, 0.7f, 0.7f, 1.0f);
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
                View.Text.Position(liveId, -1000, 0, 1); // Z-Koordinate wird ignoriert, da es sich beim Button um ein GUI Element handelt
                View.Text.Position(scoreId, -1000, 0, 1); // Z-Koordinate wird ignoriert, da es sich beim Button um ein GUI Element handelt
                IsShow = false;
            }
        }
    }
}
