using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View
{
    public class GameUi
    {
        private uint liveId = 0;
        private uint scoreId = 0;
        public uint Lives { get; set; }
        public uint Score { get; set; }
        private bool show = true;

        public GameUi()
        {
            liveId = Visualization.addText("data/fonts/arial.ttf");
            while (!Visualization.isCreated(liveId)) { }
            scoreId = Visualization.addText("data/fonts/arial.ttf");
            while (!Visualization.isCreated(scoreId)) { }

            Visualization.textSize(liveId, 36);
            Visualization.textColor(liveId, 1f, 0f, 0f, 1f);

            Visualization.textSize(scoreId, 36);
            Visualization.textColor(scoreId, 1f, 0f, 0f, 1f);

            Hide();
        }

        public void Update()
        {
            Visualization.text(liveId, "Lives: " + Lives);
            Visualization.text(scoreId, "Score: " + Score);
        }

        public void Show()
        {
            if (!show)
            {
                Visualization.position(liveId, -0.7f, 0.7f, 1.0f);
                Visualization.position(scoreId, 0.7f, 0.7f, 1.0f);
                show = true;
            }
        }

        public void Hide()
        {
            if (show)
            {
                Visualization.position(liveId, -1000, 0, 1); // Z-Koordinate wird ignoriert, da es sich beim Button um ein GUI Element handelt
                Visualization.position(scoreId, -1000, 0, 1); // Z-Koordinate wird ignoriert, da es sich beim Button um ein GUI Element handelt
                show = false;
            }
        }
    }
}
