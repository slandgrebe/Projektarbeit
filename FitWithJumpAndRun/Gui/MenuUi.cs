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
        private uint backgroundImageId = 0;

        /// <summary>ID des Cursors</summary>
        private uint cursorId = 0;
        
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
            backgroundImageId = View.Model.AddPoint("data/background/dschungel.png");
            
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
            cursorId = View.Model.AddPoint("data/models/hand/hand-stop-2.jpg");

            /*
             * Modify Object 
             * http://paletton.com/#uid=50J0B0kllll6HHOe1sOsEdSGU6p
             */
            // Hintergrund
            bool test = View.Model.IsCreated(backgroundImageId);
            while (backgroundImageId != 0 && !View.Model.IsCreated(backgroundImageId)) { }
            View.Model.Scale(backgroundImageId, 0.4f, 0.4f, 1);

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
            while (cursorId != 0 && !View.Model.IsCreated(cursorId)) { }
            View.Model.Scale(cursorId, 0.03f, 0.05f, 1);


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
                View.Model.Position(backgroundImageId, Position, 0f, -0.3f);

                title.Show();
                
                buttonEasy.Show();
                buttonNormal.Show();
                buttonDifficult.Show();

                gameName.Show();
                slogan.Show();

                Camera.PositionCamera(Position, 0, 0);
                View.Model.AttachToCamera(cursorId, true);
                
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
                View.Model.Position(backgroundImageId, -1000, 0f, -0.3f);

                title.Hide();

                buttonEasy.Hide();
                buttonNormal.Hide();
                buttonDifficult.Hide();

                gameName.Hide();
                slogan.Hide();

                View.Model.Position(cursorId, -1000, 0f, -0.2f);

                IsShown = false;
            }
        }

        /// <summary>
        /// Positioniert den Cursor
        /// </summary>
        /// <param name="x">X Koordinate</param>
        /// <param name="y">Y Koordinate</param>
        public void PositionCursor(float x, float y)
        {
            View.Model.Position(cursorId, x, y, -0.2f);
            if (HoverButton(x, y))
            {
                buttonEasy.Highlight(true);
            }
            else
            {
                buttonEasy.Highlight(false);
            }
        }

        /// <summary>
        /// Überprüft, ob sich innerhalb des Buttons befindet.
        /// </summary>
        /// <param name="x">X Koordinate des Cursors</param>
        /// <param name="y">Y Koordinate des Cursors</param>
        /// <returns>True, wenn sich der Cursor sich innerhalb des Buttons befindet</returns>
        public bool HoverButton(float x, float y)
        {
            if (x > -0.04 && x < 0.04 && y > -0.025 && y < 0.025)
            {
                return true;
            }
            return false;
        }
    }
}