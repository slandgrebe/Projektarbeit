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
    public class ButtonTutorialUi
    {
        private static ButtonTutorialUi instance = null;
        /// <summary>ID des Hintergrundbildes</summary>
        private JumpAndRun.Gui.Elements.Point background = null;
        /// <summary>obere Text</summary>
        private JumpAndRun.Gui.Elements.Text textTop = null;
        private JumpAndRun.Gui.Elements.Text textBottom = null;
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
        /// Singleton
        /// </summary>
        public static ButtonTutorialUi Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ButtonTutorialUi();
                }
                return instance;
            }
        }
        /// <summary>
        /// Initialisiert das GUI, zeigt sie aber nicht an.
        /// </summary>
        private ButtonTutorialUi()
        {
            // Hintergrund erzeugen
            background = new JumpAndRun.Gui.Elements.Point("data/background/farn.jpg");
            background.Position(0, 0, -0.8f);

            // Text erzeugen
            textTop = new JumpAndRun.Gui.Elements.Text();
            textTop.setText("Beweg doch einmal deine rechte Hand");
            textTop.Position(0, 0.8f);

            textBottom = new JumpAndRun.Gui.Elements.Text();
            textBottom.setText("über den Knopf");
            textBottom.Position(0, 0.5f);

            // Button erzeugen
            button = new Gui.Elements.Button();
            button.Text("Knopf");
            button.ClickEvent += new Gui.Elements.Button.Clicked(ButtonClicked);

            // Cursor
            JumpAndRun.Gui.Elements.Cursor.Instance.MoveEvent += new JumpAndRun.Gui.Elements.Cursor.Move(CursorMove); // nur zum testen

            // GUI nicht anzeigen
            Hide();
        }

        /// <summary>
        /// Listener des Button Click Events
        /// </summary>
        public void ButtonClicked()
        {
            //DifficultySelectedEvent(JumpAndRun.Difficulty.Easy);
            if (ButtonClickedEvent != null)
            {
                ButtonClickedEvent();
            }

            Program.Log("Button Tutorial clicked");
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
            textTop.Show();
            textBottom.Show();
            button.Show();

            JumpAndRun.Gui.Elements.Cursor.Instance.Show();
        }

        /// <summary>
        /// GUI nicht mehr anzeigen
        /// </summary>
        public void Hide()
        {
            background.Hide();
            textTop.Hide();
            textBottom.Hide();
            button.Hide();

            JumpAndRun.Gui.Elements.Cursor.Instance.Hide();
        }

        private void CursorMove(float x, float y) // nur zum testen
        {
            button.Text(truncate(x, 2) + "/" + truncate(y, 2));
            JumpAndRun.Gui.Elements.Cursor.Instance.Show();
        }

        private float truncate(float value, int digits) // nur zum testen
        {
            double mult = Math.Pow(10.0, digits);
            double result = Math.Truncate(mult * value) / mult;
            return (float)result;
        }
    }
}
