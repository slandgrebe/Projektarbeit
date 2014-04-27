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
            backgroundId = Visualization.AddPoint("Resource Files/Background/white.jpg");
            while (backgroundId != 0 && !Visualization.IsCreated(backgroundId)) { }
            Visualization.Scale(backgroundId, 10, 10, 1);

            textId = Visualization.AddText("data/fonts/arial.ttf");
            while (!Visualization.IsCreated(textId)) { }
            Visualization.Text(textId, "Keine Person erkannt!");
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
                Visualization.Position(textId, 0, 0, 0);
                Visualization.PositionCamera(Position, 0, 0);
                show = true;
            }
        }

        public void Hide()
        {
            if (show)
            {
                Visualization.Position(backgroundId, -1000, 0f, -0.3f);
                Visualization.Position(textId, -1000, 0, 0);
                show = false;
            }
        }
    }
}
