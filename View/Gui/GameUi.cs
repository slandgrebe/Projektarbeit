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
            liveId = Text.AddText("data/fonts/arial.ttf");
            while (!Text.IsCreated(liveId)) { }
            scoreId = Text.AddText("data/fonts/arial.ttf");
            while (!Text.IsCreated(scoreId)) { }

            Text.TextSize(liveId, 36);
            Text.TextColor(liveId, 1f, 0f, 0f, 1f);

            Text.TextSize(scoreId, 36);
            Text.TextColor(scoreId, 1f, 0f, 0f, 1f);

            Hide();
        }

        public void Update()
        {
            Text.String(liveId, "Lives: " + Lives);
            Text.String(scoreId, "Score: " + Score);
        }

        public void Show()
        {
            if (!show)
            {
                Text.Position(liveId, -0.7f, 0.7f, 1.0f);
                Text.Position(scoreId, 0.7f, 0.7f, 1.0f);
                show = true;
            }
        }

        public void Hide()
        {
            if (show)
            {
                Text.Position(liveId, -1000, 0, 1); // Z-Koordinate wird ignoriert, da es sich beim Button um ein GUI Element handelt
                Text.Position(scoreId, -1000, 0, 1); // Z-Koordinate wird ignoriert, da es sich beim Button um ein GUI Element handelt
                show = false;
            }
        }
    }
}
