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
        private JumpAndRun.Gui.Elements.Text life = null;
        /// <summary>ID des Textes für die erreichten Punkte</summary>
        private JumpAndRun.Gui.Elements.Text score = null;
        /// <summary>Aktuelle anzahl Leben</summary>
        public uint Lifes { get; set; }
        /// <summary>Aktuell gesammelte Punkte</summary>
        public uint Score { get; set; }

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
            life = new JumpAndRun.Gui.Elements.Text("data/fonts/arial.ttf");
            life.Size(52);
            life.Position(-0.8f, 0.8f);

            score = new JumpAndRun.Gui.Elements.Text("data/fonts/arial.ttf");
            score.Size(52);
            score.Position(0.8f, 0.8f);

            // GUI nicht anzeigen
            Hide();
        }

        /// <summary>
        /// Lebens und Punkteanzeige aktualisieren
        /// </summary>
        public void Update()
        {
            life.setText("Leben: " + Lifes);
            score.setText("Punkte: " + Score);
        }

        /// <summary>
        /// GUI anzeigen
        /// </summary>
        public void Show()
        {
            life.Show();
            score.Show();
        }

        /// <summary>
        /// GUI nicht mehr anzeigen
        /// </summary>
        public void Hide()
        {
            life.Hide();
            score.Hide();
        }
    }
}
