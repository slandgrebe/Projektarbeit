using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using View;

namespace JumpAndRun.Gui
{
    /// <summary>
    /// Darstellung des Hauptmenus
    /// </summary>
    public class MenuUi
    {
        private static MenuUi instance = null;
        /// <summary>Hintergrundbild</summary>
        private JumpAndRun.Gui.Elements.Point background = null;

        /// <summary>ID des Cursors</summary>
        //private View.Point cursor = null;

        private JumpAndRun.Gui.Elements.Text title = null;

        /// <summary>Buttons</summary>
        private Gui.Elements.Button buttonEasy = null;
        private Gui.Elements.Button buttonNormal = null;
        private Gui.Elements.Button buttonDifficult = null;

        private JumpAndRun.Gui.Elements.Text gameName = null;
        private JumpAndRun.Gui.Elements.Text slogan = null;

        /// <summary>
        /// Delegate für das Difficulty Selected Event
        /// </summary>
        /// <param name="difficulty">Schwierigkeitsgrad</param>
        public delegate void DifficultySelected(JumpAndRun.Difficulty difficulty);
        /// <summary>
        /// Difficulty Selected Event
        /// </summary>
        public event DifficultySelected DifficultySelectedEvent;

        /// <summary>
        /// Singleton
        /// </summary>
        public static MenuUi Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MenuUi();
                }
                return instance;
            }
        }
        /// <summary>
        /// Initialisiert das GUI, zeigt sie aber nicht an.
        /// </summary>
        private MenuUi()
        {
            // Hintergrund
            background = new JumpAndRun.Gui.Elements.Point("data/background/hintergrund.png");
            background.Position(0, 0, -0.55f);
            background.Scale(1.333f, 1f);

            // Titel
            title = new JumpAndRun.Gui.Elements.Text();
            title.setText("Wie anspruchsvoll darfs denn sein?");
            title.Size(50);
            title.Position(0f, 0.8f);

            // Buttons
            buttonEasy = new Gui.Elements.Button();
            buttonEasy.Text("entspannt");
            buttonEasy.Position(0f, 0.3f);
            buttonEasy.ClickEvent += new Gui.Elements.Button.Clicked(ButtonEasyClicked);

            buttonNormal = new Gui.Elements.Button();
            buttonNormal.Text("normal");
            buttonNormal.Position(0f, 0f);
            buttonNormal.ClickEvent += new Gui.Elements.Button.Clicked(ButtonNormalClicked);

            buttonDifficult = new Gui.Elements.Button();
            buttonDifficult.Text("anstrengend");
            buttonDifficult.Position(0f, -0.3f);
            buttonDifficult.ClickEvent += new Gui.Elements.Button.Clicked(ButtonDifficultClicked);

            // Spielname
            gameName = new JumpAndRun.Gui.Elements.Text();
            gameName.setText("Dschungel Fitness");
            gameName.Size(72);
            gameName.Position(0f, -0.7f);

            slogan = new JumpAndRun.Gui.Elements.Text();
            slogan.setText("- spielend fit werden");
            slogan.Size(44);
            slogan.Position(0.2f, -0.9f);

            // Cursor
            //JumpAndRun.Gui.Elements.Cursor.Instance.MoveEvent += new JumpAndRun.Gui.Elements.Cursor.Move(CursorMove); // nur zum testen

            // GUI nicht anzeigen
            Hide();
        }

        /// <summary>
        /// Button Click Event Listener für den Einfach Button
        /// </summary>
        public void ButtonEasyClicked()
        {
            DifficultySelectedEvent(JumpAndRun.Difficulty.Easy);
        }
        /// <summary>
        /// Button Click Event Listener für den Normal Button
        /// </summary>
        public void ButtonNormalClicked()
        {
            DifficultySelectedEvent(JumpAndRun.Difficulty.Normal);
        }
        /// <summary>
        /// Button Click Event Listener für den Schwer Button
        /// </summary>
        public void ButtonDifficultClicked()
        {
            DifficultySelectedEvent(JumpAndRun.Difficulty.Difficult);
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
            title.Show();
                
            buttonEasy.Show();
            buttonNormal.Show();
            buttonDifficult.Show();

            gameName.Show();
            slogan.Show();

            JumpAndRun.Gui.Elements.Cursor.Instance.Show();
        }

        /// <summary>
        /// GUI nicht mehr anzeigen
        /// </summary>
        public void Hide()
        {
            background.Hide();

            title.Hide();

            buttonEasy.Hide();
            buttonNormal.Hide();
            buttonDifficult.Hide();

            gameName.Hide();
            slogan.Hide();

            JumpAndRun.Gui.Elements.Cursor.Instance.Hide();
        }


        private void CursorMove(float x, float y) // nur zum testen
        {
            buttonEasy.Text(truncate(x, 1) + "/" + truncate(y, 1));
        }

        private float truncate(float value, int digits) // nur zum testen
        {
            double mult = Math.Pow(10.0, digits);
            double result = Math.Truncate(mult * value) / mult;
            return (float)result;
        }
    }
}