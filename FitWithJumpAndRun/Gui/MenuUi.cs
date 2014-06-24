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
        /// <summary>ID des Hintergrundbildes</summary>
        private View.Point background = null;

        /// <summary>ID des Cursors</summary>
        //private View.Point cursor = null;
        
        private View.Text title = null;

        /// <summary>Buttons</summary>
        private Gui.Elements.Button buttonEasy = null;
        private Gui.Elements.Button buttonNormal = null;
        private Gui.Elements.Button buttonDifficult = null;

        private View.Text gameName = null;
        private View.Text slogan = null;

        public delegate void DifficultySelected(JumpAndRun.Difficulty difficulty);
        public event DifficultySelected DifficultySelectedEvent;


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
        public MenuUi()
        {
            // Hintergrund
            background = new View.Point("data/background/dschungel.png");
            background.Position(0, 0, -0.8f);

            // Titel
            title = new View.Text("data/fonts/SUPERTIK.TTF");
            title.setText("Wie anspruchsvoll darfs denn sein");
            title.Size(72);
            title.Position(0f, 0.8f);

            // Buttons
            buttonEasy = new Gui.Elements.Button("data/fonts/arial.ttf");
            buttonEasy.Text("entspannt");
            buttonEasy.Position(0f, 0.3f);
            buttonEasy.ClickEvent += new Gui.Elements.Button.Clicked(ButtonEasyClicked);

            buttonNormal = new Gui.Elements.Button("data/fonts/arial.ttf");
            buttonNormal.Text("normal");
            buttonNormal.Position(0f, 0f);
            buttonNormal.ClickEvent += new Gui.Elements.Button.Clicked(ButtonNormalClicked);

            buttonDifficult = new Gui.Elements.Button("data/fonts/arial.ttf");
            buttonDifficult.Text("anstrengend");
            buttonDifficult.Position(0f, -0.3f);
            buttonDifficult.ClickEvent += new Gui.Elements.Button.Clicked(ButtonDifficultClicked);

            // Spielname
            gameName = new View.Text("data/fonts/SUPERTIK.TTF");
            gameName.setText("Dschungel Fitness");
            gameName.Size(72);
            gameName.Position(0f, -0.7f);

            slogan = new View.Text("data/fonts/SUPERTIK.TTF");
            slogan.setText("spielend fit werden");
            slogan.Size(44);
            slogan.Position(0.2f, -0.9f);

            // Cursor
            View.Cursor.Instance.MoveEvent += new View.Cursor.Move(CursorMove); // nur zum testen

            // GUI nicht anzeigen
            Hide();
        }

        public void ButtonEasyClicked()
        {
            Console.WriteLine("easy");
            DifficultySelectedEvent(JumpAndRun.Difficulty.Easy);
        }
        public void ButtonNormalClicked()
        {
            Console.WriteLine("normal");
            DifficultySelectedEvent(JumpAndRun.Difficulty.Normal);
        }
        public void ButtonDifficultClicked()
        {
            Console.WriteLine("difficult");
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

            View.Cursor.Instance.Show();
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

            View.Cursor.Instance.Hide();
        }


        private void CursorMove(float x, float y) // nur zum testen
        {
            //buttonEasy.Text(truncate(x, 3) + "/" + truncate(y, 3));
        }

        private float truncate(float value, int digits) // nur zum testen
        {
            double mult = Math.Pow(10.0, digits);
            double result = Math.Truncate(mult * value) / mult;
            return (float)result;
        }
    }
}