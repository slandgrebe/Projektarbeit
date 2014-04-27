using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using View;

namespace View
{
    public class GameOverUi
    {
        public float Position { get; set; }
        private uint backgroundId = 0;
        private uint cursorId = 0;
        private uint buttonId = 0;
        private uint textId = 0;
        private bool show = true;

        public GameOverUi()
        {
            backgroundId = Visualization.AddPoint("Resource Files/Background/white.jpg");
            while (backgroundId != 0 && !Visualization.IsCreated(backgroundId)) { }
            Visualization.Scale(backgroundId, 10, 10, 1);

            buttonId = Visualization.AddButton("data/fonts/arial.ttf");
            while (!Visualization.IsCreated(buttonId)) { }
            Visualization.Scale(buttonId, 1f, 0.5f, 1); // Skalierung in z-Richtung wird ignoriert, da es sich beim Button um ein GUI Element handelt
            Visualization.Text(buttonId, "Nochmal");
            Visualization.TextColor(buttonId, 1f, 1f, 1f, 1.0f);
            Visualization.TextSize(buttonId, 70);
            Visualization.HighlightColor(buttonId, 0.5f, 0f, 0f, 1f);
            Visualization.IsHighlighted(buttonId, true);
            //
            //Visualization.textColor(buttonId, 0f, 0f, 0f, 1.0f);

            cursorId = Visualization.AddPoint("Resource Files/Models/Hand/hand-stop-2.jpg");
            while (cursorId != 0 && !Visualization.IsCreated(cursorId)) { }
            Visualization.Scale(cursorId, 0.03f, 0.05f, 1);

            textId = Visualization.AddText("data/fonts/arial.ttf");
            while (!Visualization.IsCreated(textId)) { }
            Visualization.Text(textId, "GameOver!");
            Visualization.TextSize(textId, 50);
            Visualization.TextColor(textId, 0f, 0f, 0f, 1.0f);

            Hide();
        }

        public void Show()
        {
            if (!show)
            {
                Visualization.ChangeCameraSpeed(0);
                Visualization.Position(backgroundId, Position, 0f, -0.3f);
                Visualization.Position(buttonId, 0, 0, 1); // Z-Koordinate wird ignoriert, da es sich beim Button um ein GUI Element handelt
                Visualization.PositionCamera(Position, 0, 0);
                Visualization.AttachToCamera(cursorId, true);
                Visualization.Position(textId, 0, 0.5f, 0);
                show = true;
            }
        }

        public void Hide()
        {
            if (show)
            {
                Visualization.Position(backgroundId, -1000, 0f, -0.3f);
                Visualization.Position(buttonId, -1000, 0f, 1); // Z-Koordinate wird ignoriert, da es sich beim Button um ein GUI Element handelt
                Visualization.Position(cursorId, -1000, 0f, -0.2f);
                Visualization.Position(textId, -1000, 0f, 0);
                show = false;
            }
        }

        public void PositionCursor(float x, float y)
        {
            Visualization.Position(cursorId, x, y, -0.2f);
            if (HoverButton(x, y))
            {
                Visualization.HighlightColor(buttonId, 1f, 1f, 0f, 1f);
            }
            else
            {
                Visualization.HighlightColor(buttonId, 0.5f, 0f, 0f, 1f);
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
