using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using View;

namespace View
{
    public class NoTrackingUi
    {
        public float Position { get; set; }
        private uint backgroundId = 0;
        private uint textId = 0;
        private bool show = true;

        public NoTrackingUi()
        {
            backgroundId = Model.AddPoint("Resource Files/Background/white.jpg");
            while (backgroundId != 0 && !Model.IsCreated(backgroundId)) { }
            Model.Scale(backgroundId, 10, 10, 1);

            textId = Text.AddText("data/fonts/arial.ttf");
            while (!Text.IsCreated(textId)) { }
            Text.String(textId, "Keine Person erkannt!");
            Text.TextSize(textId, 50);
            Text.TextColor(textId, 0f, 0f, 0f, 1.0f);
            
            Hide();
        }

        public void Show()
        {
            if (!show)
            {
                Camera.ChangeCameraSpeed(0);
                Model.Position(backgroundId, Position, 0f, -0.3f);
                Text.Position(textId, 0, 0, 0);
                Camera.PositionCamera(Position, 0, 0);
                show = true;
            }
        }

        public void Hide()
        {
            if (show)
            {
                Model.Position(backgroundId, -1000, 0f, -0.3f);
                Text.Position(textId, -1000, 0, 0);
                show = false;
            }
        }
    }
}
