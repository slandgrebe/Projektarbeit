using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JumpAndRun.Gui
{
    class KinectUi
    {
        private static KinectUi instance = null;
        private View.Text text = null;

        public static KinectUi Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new KinectUi();
                }
                return instance;
            }
        }

        public KinectUi()
        {
            text = new View.Text("data/fonts/arial.ttf");
            text.setText("Kinect wird gestartet");
            text.Size(50);
        }

        public void Show()
        {
            text.Show();
        }

        public void Hide()
        {
            text.Hide();
        }

        public void SetText(string text)
        {
            this.text.setText(text);
        }
    }
}
