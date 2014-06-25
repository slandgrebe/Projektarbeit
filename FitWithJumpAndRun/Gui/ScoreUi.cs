using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using View;

namespace JumpAndRun.Gui
{
    /// <summary>
    /// Darstellung des GUI nach erfolgreichem beenden des Spieles.
    /// </summary>
    public class ScoreUi
    {
        private static ScoreUi instance = null;
        /// <summary>ID des Hintergrundbildes</summary>
        private JumpAndRun.Gui.Elements.Point background = null;
        /// <summary>ID des Textes</summary>
        private JumpAndRun.Gui.Elements.Text text = null;
        /// <summary>ID des Buttons</summary>
        private Gui.Elements.Button button = null;

        /// <summary>
        /// Delegate für das Button Click Event
        /// </summary>
        public delegate void ButtonClick();
        /// <summary>
        /// Button Click Event
        /// </summary>
        public event ButtonClick ButtonClickedEvent;

        /// <summary>
        /// Anzuzeigende Punktzahl
        /// </summary>
        public uint Score { get; set; }

        /// <summary>
        /// Singleton
        /// </summary>
        public static ScoreUi Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ScoreUi();
                }
                return instance;
            }
        }

        /// <summary>
        /// Initialisiert das GUI, zeigt sie aber nicht an.
        /// </summary>
        private ScoreUi()
        {
            Score = 0;

            // Hintergrund erzeugen
            background = new JumpAndRun.Gui.Elements.Point("data/background/white.jpg");
            background.Scale(2, 2);

            // Text erzeugen
            text = new JumpAndRun.Gui.Elements.Text("data/fonts/arial.ttf");
            text.setText("Punkte: " + Score);
            text.Size(50);
            text.Position(0, 0.5f);

            // Button erzeugen
            button = new Gui.Elements.Button("data/fonts/arial.ttf");
            button.Text("Nochmal");
            button.ClickEvent += new Gui.Elements.Button.Clicked(ButtonClicked);

            // Cursor erzeugen

            /*cursorId = View.Model.AddPoint("data/models/hand/hand-stop-2.jpg");
            while (cursorId != 0 && !View.Model.IsCreated(cursorId)) { }
            View.Model.Scale(cursorId, 0.03f, 0.05f, 1);*/

            // GUI nicht anzeigen
            Hide();
        }

        /// <summary>
        /// Button Click Event Listener
        /// </summary>
        public void ButtonClicked()
        {
            Console.WriteLine("score click");
            //DifficultySelectedEvent(JumpAndRun.Difficulty.Easy);
            ButtonClickedEvent();
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
            text.setText("Punkte: " + Score);
            button.Show();

            JumpAndRun.Gui.Elements.Cursor.Instance.Show();
        }

        /// <summary>
        /// GUI nicht mehr anzeigen
        /// </summary>
        public void Hide()
        {
            background.Hide();
            text.Hide();
            button.Hide();

            JumpAndRun.Gui.Elements.Cursor.Instance.Hide();
        }
    }
}
