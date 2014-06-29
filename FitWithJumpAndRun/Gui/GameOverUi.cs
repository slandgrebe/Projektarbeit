using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using View;
using log4net;
using log4net.Config;

namespace JumpAndRun.Gui
{
    /// <summary>
    /// Darstellung des GUI nach einem Game Over.
    /// </summary>
    public class GameOverUi
    {
        private static GameOverUi instance = null;
        /// <summary>ID des Hintergrundbildes</summary>
        private JumpAndRun.Gui.Elements.Point background = null;
        /// <summary>ID des Textes</summary>
        private JumpAndRun.Gui.Elements.Text text = null;
        /// <summary>ID des Buttons</summary>
        private Gui.Elements.Button button = null;
        /// <summary>Logger</summary>
        private static readonly ILog log = LogManager.GetLogger(typeof(GameOverUi).Name);

        /// <summary>
        /// Delegate für das Button Click Event
        /// </summary>
        public delegate void ButtonClick();
        /// <summary>
        /// Button Click Event
        /// </summary>
        public event ButtonClick ButtonClickedEvent;

        /// <summary>
        /// Singleton
        /// </summary>
        public static GameOverUi Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameOverUi();
                }
                return instance;
            }
        }
        /// <summary>
        /// Initialisiert das GUI, zeigt sie aber nicht an.
        /// </summary>
        private GameOverUi()
        {
            // Hintergrund erzeugen
            background = new JumpAndRun.Gui.Elements.Point("data/background/hintergrund.png");
            background.Position(0, 0, -0.55f);
            background.Scale(1.333f, 1f);

            // Text erzeugen
            text = new JumpAndRun.Gui.Elements.Text();
            text.setText("Game over!");
            text.Position(0, 0.5f);

            // Button erzeugen
            button = new Gui.Elements.Button();
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
        /// Listener des Button Click Events
        /// </summary>
        public void ButtonClicked()
        {
            log.Info("game over click");
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
