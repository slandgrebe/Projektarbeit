/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;*/

namespace Sound
{
    public class Sound
    {
        public float Volume
        {
            get
            {
                return Volume / 100f;
            }
            set
            {
                Player.settings.volume = (int)(value * 100);
            }
        }
        private WMPLib.WindowsMediaPlayer Player { get; set; }

        public Sound(string filename)
        {
            this.Volume = 1.0f;
            Player.URL = filename;
        }

        public bool Play()
        {
            Player.controls.play();
            return true;
        }
        public bool Stop()
        {
            Player.controls.stop();
            return true;
        }
    }
}
