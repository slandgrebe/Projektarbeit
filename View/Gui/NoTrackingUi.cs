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

        public NoTrackingUi()
        {
            backgroundId = Visualization.addPoint("Resource Files/Background/white.jpg");
            while (backgroundId != 0 && !Visualization.isCreated(backgroundId)) { }
            Visualization.scale(backgroundId, 10, 10, 1);

            textId = Visualization.addText("data/fonts/arial.ttf");
            while (!Visualization.isCreated(textId)) { }
            Visualization.text(textId, "Keine Person erkannt!");
            Visualization.textSize(textId, 50);
            Visualization.textColor(textId, 0f, 0f, 0f, 1.0f);
            
            Hide();
        }

        public void Show()
        {
            Visualization.position(backgroundId, Position, 0f, -0.3f);
            Visualization.position(textId, 0, 0, 0);
            Visualization.positionCamera(Position, 0, 0);
        }

        public void Hide()
        {
            Visualization.position(backgroundId, -1000, 0f, -0.3f);
            Visualization.position(textId, -1000, 0, 0);
        }
    }
}
