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
        /// <summary>ID des Hintergrundbildes</summary>
        private View.Point background = null;

        /// <summary>ID des Cursors</summary>
        //private View.Point cursor = null;
        
        private View.Text title = null;

        /// <summary>Buttons</summary>
        private View.Button buttonEasy = null;
        private View.Button buttonNormal = null;
        private View.Button buttonDifficult = null;

        private View.Text gameName = null;
        private View.Text slogan = null;

        public delegate void DifficultySelected(JumpAndRun.Difficulty difficulty);
        public event DifficultySelected DifficultySelectedEvent;

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
            buttonEasy = new View.Button("data/fonts/arial.ttf");
            buttonEasy.Text("entspannt");
            buttonEasy.Position(0f, 0.3f);
            buttonEasy.ClickEvent += new View.Button.Clicked(ButtonEasyClicked);

            buttonNormal = new View.Button("data/fonts/arial.ttf");
            buttonNormal.Text("normal");
            buttonNormal.Position(0f, 0f);
            buttonNormal.ClickEvent += new View.Button.Clicked(ButtonNormalClicked);

            buttonDifficult = new View.Button("data/fonts/arial.ttf");
            buttonDifficult.Text("anstrengend");
            buttonDifficult.Position(0f, -0.3f);
            buttonDifficult.ClickEvent += new View.Button.Clicked(ButtonDifficultClicked);

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
            //View.Cursor.Instance.MoveEvent += new View.Cursor.Move(CursorMove);
            //View.Cursor.Instance.ClickEvent += new View.Cursor.Click(CursorClick);
            /*cursor = new View.Point("data/models/hand/hand-stop-2.jpg");
            cursor.Scale(0.05f, 0.025f);
            cursor.Position(0, 0, -0.3f);*/

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


        /*private void CursorMove(float x, float y)
        {
            //Console.WriteLine("Cursor update: " + x + " " + y);
            buttonEasy.Text(truncate(x, 1) + "/" + truncate(y, 1));

            buttonEasy.CursorUpdate(x, y);
            buttonNormal.CursorUpdate(x, y);
            buttonDifficult.CursorUpdate(x, y);
        }*/
        /*private void CursorClick()
        {
            buttonNormal.Text("Clicked");
        }*/

        /*public bool IsButtonEasyHovered()
        {
            return buttonEasy.IsHovered;
        }
        public bool IsButtonNormalHovered()
        {
            return buttonNormal.IsHovered;
        }
        public bool IsButtonDifficultHovered()
        {
            return buttonDifficult.IsHovered;
        }*/

        /// <summary>
        /// Positioniert den Cursor
        /// </summary>
        /// <param name="x">X Koordinate</param>
        /// <param name="y">Y Koordinate</param>
        /*public void PositionCursor(float handX, float handY, float headX, float headY, float shoulderX, float shoulderY)
        {
            float xMax = (shoulderX - headX) * 2;
            float yMax = (shoulderY - headY);

            float x = handX - headX;
            float y = handY - headY;

            float xRelative = x / xMax -1;
            float yRelative = y / yMax -1;

            //Console.WriteLine("Cursor: " + x + "/" + y);

            buttonEasy.Text(truncate(xRelative, 1) + "/" + truncate(yRelative, 1));
            //buttonEasy.Text("hand: " + truncate(handX, 4) + "/" + truncate(handY, 4));
            //buttonNormal.Text("head: " + truncate(headX, 4) + "/" + truncate(headY, 4));
            //buttonDifficult.Text("shoulder: " + truncate(shoulderX, 4) + "/" + truncate(shoulderY, 4));

            //cursor.Position(x, y, -1f);
            if (CursorPosition(x, y))
            {
                buttonEasy.Highlight(true);
            }
            else
            {
                buttonEasy.Highlight(false);
            }
        }*/

        private float truncate(float value, int digits)
        {
            double mult = Math.Pow(10.0, digits);
            double result = Math.Truncate(mult * value) / mult;
            return (float)result;
        }

        /// <summary>
        /// Überprüft, ob sich innerhalb des Buttons befindet.
        /// </summary>
        /// <param name="x">X Koordinate des Cursors</param>
        /// <param name="y">Y Koordinate des Cursors</param>
        /// <returns>True, wenn sich der Cursor sich innerhalb des Buttons befindet</returns>
        /*public bool CursorPosition(float x, float y)
        {            

            if (x > -0.04 && x < 0.04 && y > -0.025 && y < 0.025)
            {
                return true;
            }
            return false;
        }*/
    }
}