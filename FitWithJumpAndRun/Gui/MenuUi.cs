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
        /// <summary>Position des GUIS im Koordinatensystem</summary>
        public float Position { get; set; }

        /// <summary>ID des Hintergrundbildes</summary>
        private View.Point backgroundImage = null;

        /// <summary>ID des Cursors</summary>
        private View.Point cursor = null;
        
        private View.Text title = null;

        /// <summary>Buttons</summary>
        private View.Button buttonEasy = null;
        private View.Button buttonNormal = null;
        private View.Button buttonDifficult = null;

        private View.Text gameName = null;
        private View.Text slogan = null;

        /// <summary>Zeigt an, ob dieses GUI aktuell aktiv ist, oder nicht.</summary>
        public bool IsShown { get; set; }

        /// <summary>
        /// Initialisiert das GUI, zeigt sie aber nicht an.
        /// </summary>
        public MenuUi()
        {
            IsShown = true;

            /*
             * Create Objects
             */

            // Hintergrund
            backgroundImage = new View.Point("data/background/dschungel.png");

            // Titel
            title = new View.Text("data/fonts/SUPERTIK.TTF");

            // Buttons
            buttonEasy = new View.Button("data/fonts/arial.ttf");
            buttonNormal = new View.Button("data/fonts/arial.ttf");
            buttonDifficult = new View.Button("data/fonts/arial.ttf");

            // Spielname
            gameName = new View.Text("data/fonts/SUPERTIK.TTF");
            slogan = new View.Text("data/fonts/SUPERTIK.TTF");

            // Cursor
            cursor = new View.Point("data/models/hand/hand-stop-2.jpg");

            /*
             * Modify Object
             */
            // Hintergrund
            backgroundImage.Scale(0.4f, 0.4f);
            backgroundImage.Position(0, 0, 100);

            // Titel
            title.setText("Wie anspruchsvoll darfs denn sein");
            title.Size(72);
            title.Position(0f, 0.8f);

            // Buttons
            buttonEasy.Text("entspannt");
            buttonEasy.Position(0f, 0.3f);

            buttonNormal.Text("normal");
            buttonNormal.Position(0f, 0f);

            buttonDifficult.Text("anstrengend");
            buttonDifficult.Position(0f, -0.3f);

            // Spielname
            gameName.setText("Dschungel Fitness");
            gameName.Size(72);
            gameName.Position(0f, -0.7f);

            slogan.setText("spielend fit werden");
            slogan.Size(44);
            slogan.Position(0.2f, -0.9f);

            // Cursor
            //cursor.Scale(0.3f, 0.5f);
            cursor.Scale(100f, 100f);
            cursor.Position(0, 0, -100);

            // GUI nicht anzeigen
            Hide();
        }

        /// <summary>
        /// GUI anzeigen
        /// </summary>
        public void Show()
        {
            if (!IsShown)
            {
                Camera.ChangeCameraSpeed(0);
                //backgroundImage.Position(Position, 0f, -0.3f);
                backgroundImage.Show();

                title.Show();
                
                /*buttonEasy.Show();
                buttonNormal.Show();
                buttonDifficult.Show();*/

                gameName.Show();
                slogan.Show();

                Camera.PositionCamera(Position, 0, 0);
                Console.WriteLine("test");
                cursor.Show();
                //View.Model.AttachToCamera(cursorId, true);
                
                IsShown = true;
            }
        }

        /// <summary>
        /// GUI nicht mehr anzeigen
        /// </summary>
        public void Hide()
        {
            if (IsShown)
            {
                //View.Model.Position(backgroundImageId, -1000, 0f, -0.3f);
                backgroundImage.Hide();

                title.Hide();

                buttonEasy.Hide();
                buttonNormal.Hide();
                buttonDifficult.Hide();

                gameName.Hide();
                slogan.Hide();

                //View.Model.Position(cursorId, -1000, 0f, -0.2f);
                cursor.Hide();

                IsShown = false;
            }
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