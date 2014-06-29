using System;
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
        private static GameUi instance = null;
        /// <summary>ID des Textes für die Lebensanzeigen</summary>
        private JumpAndRun.Gui.Elements.Text penalty = null;
        /// <summary>ID des Textes für die erreichten Punkte</summary>
        private JumpAndRun.Gui.Elements.Text advantage = null;
        /// <summary>Aktuelle Anzahl Strafpunkte</summary>
        public uint Penalty { get; set; }
        /// <summary>Aktuell gesammelte Pluspunkte</summary>
        public uint Advantage { get; set; }

        /// <summary>
        /// Singleton
        /// </summary>
        public static GameUi Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameUi();
                }
                return instance;
            }
        }
        /// <summary>
        /// Initialisiert das GUI, zeigt sie aber nicht an.
        /// </summary>
        private GameUi()
        {
            // Texte erzeugen
            penalty = new JumpAndRun.Gui.Elements.Text();
            penalty.Size(40);
            penalty.Position(-0.7f, 0.8f);

            advantage = new JumpAndRun.Gui.Elements.Text();
            advantage.Size(40);
            advantage.Position(0.7f, 0.8f);

            // GUI nicht anzeigen
            Hide();
        }

        /// <summary>
        /// Lebens und Punkteanzeige aktualisieren
        /// </summary>
        public void Update()
        {
            penalty.setText("Strafe: " + Penalty);
            advantage.setText("Punkte: " + Advantage);
        }

        /// <summary>
        /// GUI anzeigen
        /// </summary>
        public void Show()
        {
            penalty.Show();
            advantage.Show();
        }

        /// <summary>
        /// GUI nicht mehr anzeigen
        /// </summary>
        public void Hide()
        {
            penalty.Hide();
            advantage.Hide();
        }
    }
}
