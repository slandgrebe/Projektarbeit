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

        public GameUi()
        {
            liveId = Visualization.addText("data/fonts/arial.ttf");
            while (!Visualization.isCreated(liveId)) { }
            scoreId = Visualization.addText("data/fonts/arial.ttf");
            while (!Visualization.isCreated(scoreId)) { }

            Visualization.position(liveId, -0.7f, 0.7f, 1.0f);
            Visualization.textSize(liveId, 36);
            Visualization.textColor(liveId, 1f, 0f, 0f, 1f);

            Visualization.position(scoreId, 0.7f, 0.7f, 1.0f);
            Visualization.textSize(scoreId, 36);
            Visualization.textColor(scoreId, 1f, 0f, 0f, 1f);
        }

        public void Update()
        {
            Visualization.text(liveId, "Lives: " + Lives);
            Visualization.text(scoreId, "Score: " + Score);
        }
    }
}
