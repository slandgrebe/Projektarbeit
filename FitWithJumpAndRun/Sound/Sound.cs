/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;*/

namespace Sound
{
    public class Sound
    {
        /// <summary>Windows Media Player Objekt</summary>
        private WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();
        /// <summary>Dateipfad zur Audiodatei</summary>
        public string FilePath { get; set; }
        /// <summary>Wiedergabelautstärke</summary>
        private int _Volume = 50;
        /// <summary>Wiergabe wiederholen</summary>
        private bool _Loop;
        /// <summary>Wiedergabelautstärke</summary>
        public int Volume
        {
            get { return _Volume; }
            set
            {
                _Volume = value;
                wplayer.settings.volume = _Volume;
                wplayer.StatusChange 
            }
        }
        /// <summary>Wiedergabe wiederholen</summary>
        public bool Loop
        {
            get { return _Loop; }
            set
            {
                _Loop = value;
                wplayer.settings.setMode("loop", _Loop);
            }
        }

        /// <summary>
        /// Initiaisiert das Soundobjekt
        /// </summary>
        public Sound()
        {
            Volume = _Volume;
        }

        /// <summary>
        /// Initiaisiert das Soundobjekt
        /// </summary>
        /// <param name="filePath">Dateipfad zur Sounddatei</param>
        public Sound(string filePath) : this()
        {
            FilePath = filePath;
        }

        /// <summary>
        /// Sound abspielen
        /// </summary>
        public void Play()
        {
            if (FilePath.Length > 0)
            {
                wplayer.URL = FilePath;
                wplayer.controls.play();
            }
        }

        /// <summary>
        /// Sound Stoppen
        /// </summary>
        public void Stop()
        {
            wplayer.controls.stop();
        }

        /// <summary>
        /// Sound langsam ausklingen lassen
        /// </summary>
        public void FadeOut()
        {
            while (Volume > 0)
            {
                System.Threading.Thread.Sleep(10);
                Volume--;
            }
            Stop();
        }
    }
}
