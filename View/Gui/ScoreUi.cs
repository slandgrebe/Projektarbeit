using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using View;

namespace View
{
    public class ScoreUi
    {
        public float Position { get; set; }
        private uint backgroundId = 0;
        private uint cursorId = 0;
        private uint buttonId = 0;
        private uint textId = 0;
        private bool show = true;

        public ScoreUi()
        {
            backgroundId = Visualization.addPoint("Resource Files/Background/white.jpg");
            while (backgroundId != 0 && !Visualization.isCreated(backgroundId)) { }
            Visualization.scale(backgroundId, 10, 10, 1);

            buttonId = Visualization.addButton("data/fonts/arial.ttf");
            while (!Visualization.isCreated(buttonId)) { }
            Visualization.scale(buttonId, 1f, 0.5f, 1); // Skalierung in z-Richtung wird ignoriert, da es sich beim Button um ein GUI Element handelt
            Visualization.text(buttonId, "Nochmal");
            Visualization.textColor(buttonId, 1f, 1f, 1f, 1.0f);
            Visualization.textSize(buttonId, 70);
            Visualization.highlightColor(buttonId, 0.5f, 0f, 0f, 1f);
            Visualization.isHighlighted(buttonId, true);
            //
            //Visualization.textColor(buttonId, 0f, 0f, 0f, 1.0f);

            cursorId = Visualization.addPoint("Resource Files/Models/Hand/hand-stop-2.jpg");
            while (cursorId != 0 && !Visualization.isCreated(cursorId)) { }
            Visualization.scale(cursorId, 0.03f, 0.05f, 1);

            textId = Visualization.addText("data/fonts/arial.ttf");
            while (!Visualization.isCreated(textId)) { }
            Visualization.text(textId, "Score: ");
            Visualization.textSize(textId, 50);
            Visualization.textColor(textId, 0f, 0f, 0f, 1.0f);

            Hide();
        }

        public void Show(uint score)
        {
            if (!show)
            {
                Visualization.changeCameraSpeed(0);
                Visualization.position(backgroundId, Position, 0f, -0.3f);
                Visualization.position(buttonId, 0, 0, 1); // Z-Koordinate wird ignoriert, da es sich beim Button um ein GUI Element handelt
                Visualization.positionCamera(Position, 0, 0);
                Visualization.attachToCamera(cursorId, true);
                Visualization.position(textId, 0, 0.5f, 0);
                Visualization.text(textId, "Score: " + score);
                show = true;
            }
        }

        public void Hide()
        {
            if (show)
            {
                Visualization.position(backgroundId, -1000, 0f, -0.3f);
                Visualization.position(buttonId, -1000, 0f, 1); // Z-Koordinate wird ignoriert, da es sich beim Button um ein GUI Element handelt
                Visualization.position(cursorId, -1000, 0f, -0.2f);
                Visualization.position(textId, -1000, 0f, 0);
                show = false;
            }
        }

        public void PositionCursor(float x, float y)
        {
            Visualization.position(cursorId, x, y, -0.2f);
            if (HoverButton(x, y))
            {
                Visualization.highlightColor(buttonId, 1f, 1f, 0f, 1f);
            }
            else
            {
                Visualization.highlightColor(buttonId, 0.5f, 0f, 0f, 1f);
            }
        }

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
