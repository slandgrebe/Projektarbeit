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
            backgroundId = Model.AddPoint("Resource Files/Background/white.jpg");
            while (backgroundId != 0 && !Model.IsCreated(backgroundId)) { }
            Model.Scale(backgroundId, 10, 10, 1);

            buttonId = Model.AddButton("data/fonts/arial.ttf");
            while (!Model.IsCreated(buttonId)) { }
            Model.Scale(buttonId, 1f, 0.5f, 1); // Skalierung in z-Richtung wird ignoriert, da es sich beim Button um ein GUI Element handelt
            Text.String(buttonId, "Nochmal");
            Text.TextColor(buttonId, 1f, 1f, 1f, 1.0f);
            Text.TextSize(buttonId, 70);
            Model.HighlightColor(buttonId, 0.5f, 0f, 0f, 1f);
            Model.IsHighlighted(buttonId, true);
            //
            //Visualization.textColor(buttonId, 0f, 0f, 0f, 1.0f);

            cursorId = Model.AddPoint("Resource Files/Models/Hand/hand-stop-2.jpg");
            while (cursorId != 0 && !Model.IsCreated(cursorId)) { }
            Model.Scale(cursorId, 0.03f, 0.05f, 1);

            textId = Text.AddText("data/fonts/arial.ttf");
            while (!Text.IsCreated(textId)) { }
            Text.String(textId, "Score: ");
            Text.TextSize(textId, 50);
            Text.TextColor(textId, 0f, 0f, 0f, 1.0f);

            Hide();
        }

        public void Show(uint score)
        {
            if (!show)
            {
                Camera.ChangeCameraSpeed(0);
                Model.Position(backgroundId, Position, 0f, -0.3f);
                Model.Position(buttonId, 0, 0, 1); // Z-Koordinate wird ignoriert, da es sich beim Button um ein GUI Element handelt
                Camera.PositionCamera(Position, 0, 0);
                Model.AttachToCamera(cursorId, true);
                Text.Position(textId, 0, 0.5f, 0);
                Text.String(textId, "Score: " + score);
                show = true;
            }
        }

        public void Hide()
        {
            if (show)
            {
                Model.Position(backgroundId, -1000, 0f, -0.3f);
                Model.Position(buttonId, -1000, 0f, 1); // Z-Koordinate wird ignoriert, da es sich beim Button um ein GUI Element handelt
                Model.Position(cursorId, -1000, 0f, -0.2f);
                Text.Position(textId, -1000, 0f, 0);
                show = false;
            }
        }

        public void PositionCursor(float x, float y)
        {
            Model.Position(cursorId, x, y, -0.2f);
            if (HoverButton(x, y))
            {
                Model.HighlightColor(buttonId, 1f, 1f, 0f, 1f);
            }
            else
            {
                Model.HighlightColor(buttonId, 0.5f, 0f, 0f, 1f);
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
