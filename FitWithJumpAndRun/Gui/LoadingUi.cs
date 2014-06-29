using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using View;

namespace JumpAndRun.Gui
{
    /// <summary>
    /// Ladebildschirm des Spieles
    /// </summary>
    public class LoadingUi
    {
        private static LoadingUi instance = null;
        /// <summary>ID des Hintergrundbildes</summary>
        private JumpAndRun.Gui.Elements.Point background = null;
        /// <summary>ID des Textes</summary>
        private JumpAndRun.Gui.Elements.TextWithBackground text = null;
        private JumpAndRun.Gui.Elements.Button button = null;

        /// <summary>
        /// Delegate für Button Click
        /// </summary>
        public delegate void ButtonClick();
        /// <summary>
        /// Button Click Event
        /// </summary>
        public event ButtonClick ButtonClickedEvent;

        /// <summary>
        /// Singleton
        /// </summary>
        public static LoadingUi Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new LoadingUi();
                }
                return instance;
            }
        }

        /// <summary>
        /// Initialisiert das GUI, zeigt sie aber nicht an.
        /// </summary>
        private LoadingUi()
        {
            // Hintergrund
            background = new JumpAndRun.Gui.Elements.Point("data/background/ladebildschirm.png");
            background.Position(0, 0, -0.55f);
            background.Scale(1.333f, 1f);

            // Text erzeugen
            text = new JumpAndRun.Gui.Elements.TextWithBackground();
            text.Size(40);
            text.Position(0f, -0.7f);
            text.Color(0.84f, 0.59f, 0.11f, 1f);

            // Button
            button = new JumpAndRun.Gui.Elements.Button();
            button.Text("Start");
            button.Position(0f, 0f);
            button.ClickEvent += new JumpAndRun.Gui.Elements.Button.Clicked(ButtonClicked);

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
            text.setText("Dschungel Trainer wird geladen");
            text.Show();
            button.Hide();
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

        /// <summary>
        /// Button Click Event Listener
        /// </summary>
        public void ButtonClicked()
        {
            ButtonClickedEvent();
        }

        public void LoadingComplete()
        {
            text.setText("Spiel geladen");
            button.Show();
            JumpAndRun.Gui.Elements.Cursor.Instance.Show();
        }
    }
}
