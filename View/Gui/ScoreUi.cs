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
        private uint buttonId = Visualization.addButton("data/fonts/arial.ttf");

        public ScoreUi()
        {
            while (!Visualization.isCreated(buttonId)) { }
            Visualization.position(buttonId, 0f, 0f, 1); // Z-Koordinate wird ignoriert, da es sich beim Button um ein GUI Element handelt
            Visualization.scale(buttonId, 1f, 0.5f, 1); // Skalierung in z-Richtung wird ignoriert, da es sich beim Button um ein GUI Element handelt
            Visualization.text(buttonId, "Start");
            Visualization.textColor(buttonId, 1f, 1f, 1f, 1.0f);
            Visualization.textSize(buttonId, 70);
            Visualization.highlightColor(buttonId, 0.5f, 0f, 0f, 1f);
            Visualization.isHighlighted(buttonId, true);
            Visualization.highlightColor(buttonId, 1f, 1f, 0f, 1f);
            Visualization.textColor(buttonId, 0f, 0f, 0f, 1.0f);
        }

        public void ShowMenu()
        {

        }
    }
}
