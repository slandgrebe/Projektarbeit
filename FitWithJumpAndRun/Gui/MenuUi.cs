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
        private View.Point cursor = null;
        
        private View.Text title = null;

        /// <summary>Buttons</summary>
        private View.Button buttonEasy = null;
        private View.Button buttonNormal = null;
        private View.Button buttonDifficult = null;

        private View.Text gameName = null;
        private View.Text slogan = null;

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

            buttonNormal = new View.Button("data/fonts/arial.ttf");
            buttonNormal.Text("normal");
            buttonNormal.Position(0f, 0f);

            buttonDifficult = new View.Button("data/fonts/arial.ttf");
            buttonDifficult.Text("anstrengend");
            buttonDifficult.Position(0f, -0.3f);

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
            cursor = new View.Point("data/models/hand/hand-stop-2.jpg");
            cursor.Scale(0.05f, 0.025f);
            cursor.Position(0, 0, -0.3f);

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
            title.Show();
                
            buttonEasy.Show();
            buttonNormal.Show();
            buttonDifficult.Show();

            gameName.Show();
            slogan.Show();

            cursor.Show();
            //View.Model.AttachToCamera(cursorId, true);
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

            cursor.Hide();
        }

        /// <summary>
        /// Positioniert den Cursor
        /// </summary>
        /// <param name="x">X Koordinate</param>
        /// <param name="y">Y Koordinate</param>
        public void PositionCursor(float handX, float handY, float headX, float headY, float shoulderX, float shoulderY)
        {
            // convert
            /*
             * Oben links: headX/headY
             * Oben rechts: (headX + shoulder
             */

            float x = 0f;
            float y = 0f;


            /*x = x * 2;
            y = y * 4;
            y = y - y / 2;*/

            //Console.WriteLine("Cursor: " + x + "/" + y);
            buttonEasy.Text("Cursor: " + x + "/" + y);

            //cursor.Position(x, y, -1f);
            /*if (CursorPosition(x, y))
            {
                buttonEasy.Highlight(true);
            }
            else
            {
                buttonEasy.Highlight(false);
            }*/
        }

        /// <summary>
        /// Überprüft, ob sich innerhalb des Buttons befindet.
        /// </summary>
        /// <param name="x">X Koordinate des Cursors</param>
        /// <param name="y">Y Koordinate des Cursors</param>
        /// <returns>True, wenn sich der Cursor sich innerhalb des Buttons befindet</returns>
        public bool CursorPosition(float x, float y)
        {            

            /*if (x > -0.04 && x < 0.04 && y > -0.025 && y < 0.025)
            {
                return true;
            }*/
            return false;
        }
    }
}