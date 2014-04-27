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
            liveId = Visualization.AddText("data/fonts/arial.ttf");
            while (!Visualization.IsCreated(liveId)) { }
            scoreId = Visualization.AddText("data/fonts/arial.ttf");
            while (!Visualization.IsCreated(scoreId)) { }

            Visualization.TextSize(liveId, 36);
            Visualization.TextColor(liveId, 1f, 0f, 0f, 1f);

            Visualization.TextSize(scoreId, 36);
            Visualization.TextColor(scoreId, 1f, 0f, 0f, 1f);

            Hide();
        }

        public void Update()
        {
            Visualization.Text(liveId, "Lives: " + Lives);
            Visualization.Text(scoreId, "Score: " + Score);
        }

        public void Show()
        {
            if (!show)
            {
                Visualization.Position(liveId, -0.7f, 0.7f, 1.0f);
                Visualization.Position(scoreId, 0.7f, 0.7f, 1.0f);
                show = true;
            }
        }

        public void Hide()
        {
            if (show)
            {
                Visualization.Position(liveId, -1000, 0, 1); // Z-Koordinate wird ignoriert, da es sich beim Button um ein GUI Element handelt
                Visualization.Position(scoreId, -1000, 0, 1); // Z-Koordinate wird ignoriert, da es sich beim Button um ein GUI Element handelt
                show = false;
            }
        }
    }
}
