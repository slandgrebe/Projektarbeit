using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using View;

namespace JumpAndRun.Gui
{
    /// <summary>
    /// Darstellung des GUI nach einem Game Over.
    /// </summary>
    public class GameOverUi
    {
        private static GameOverUi instance = null;
        /// <summary>ID des Hintergrundbildes</summary>
        private View.Point background = null;
        /// <summary>ID des Textes</summary>
        private View.Text text = null;
        /// <summary>ID des Buttons</summary>
        private Gui.Elements.Button button = null;

        public delegate void ButtonClick();
        public event ButtonClick ButtonClickedEvent;

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
            background = new View.Point("data/background/white.jpg");
            background.Scale(2, 2);

            // Text erzeugen
            text = new View.Text("data/fonts/arial.ttf");
            text.setText("Game over!");
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

        public void ButtonClicked()
        {
            Console.WriteLine("game over click");
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

            View.Cursor.Instance.Show();
        }

        /// <summary>
        /// GUI nicht mehr anzeigen
        /// </summary>
        public void Hide()
        {
            background.Hide();
            text.Hide();
            button.Hide();

            View.Cursor.Instance.Hide();
        }
    }
}
