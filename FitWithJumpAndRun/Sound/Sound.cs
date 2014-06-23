/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;*/

namespace JumpAndRun.Sound
{
    /// <summary>
    /// Spielt Audiodateien ab.
    /// </summary>
    class Sound
    {
        /// <summary>
        /// Delegate ruft Methode auf, wenn Sound am Ende oder gestoppt wird.
        /// </summary>
        /// <param name="s">Soundobjekt welches beendet wurde</param>
        public delegate void SoundStoppedEventHandler(Sound s);
        /// <summary>
        /// Eventhandler Informiert wenn Sound am Ende ist oder gestoppt wird.
        /// </summary>
        public event SoundStoppedEventHandler SoundStopped;

        /// <summary>Windows Media Player Objekt</summary>
        private WMPLib.WindowsMediaPlayer audio = new WMPLib.WindowsMediaPlayer();
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
                audio.settings.volume = _Volume;
            }
        }
        /// <summary>Wiedergabe wiederholen</summary>
        public bool Loop
        {
            get { return _Loop; }
            set
            {
                _Loop = value;
                audio.settings.setMode("loop", _Loop);
            }
        }

        /// <summary>
        /// Sound erstellen
        /// </summary>
        public Sound()
        {
            audio.settings.volume = Volume;
            audio.PlayStateChange += new WMPLib._WMPOCXEvents_PlayStateChangeEventHandler(Sound_PlayStateChange);
        }

        /// <summary>
        /// Sound abspielen
        /// </summary>
        public void Play()
        {
            audio.URL = FilePath;
            audio.controls.play();
        }

        /// <summary>
        /// Sound Stoppen
        /// </summary>
        public void Stop()
        {
            audio.controls.stop();
        }

        /// <summary>
        /// Sound langsam ausklingen lassen
        /// </summary>
        /// <param name="time">Zeit in Sekunden</param>
        public void FadeOut(int time = 1)
        {
            int sleep = (time * 1000) / Volume;

            while (Volume > 0)
            {
                System.Threading.Thread.Sleep(sleep);
                Volume--;
            }
            Stop();
        }

        /// <summary>
        /// Abfangen wenn Sound gestoppt/zuenede ist
        /// </summary>
        /// <param name="NewState">aktueller Soundstatus</param>
        private void Sound_PlayStateChange(int NewState)
        {
            if ((WMPLib.WMPPlayState)NewState == WMPLib.WMPPlayState.wmppsStopped)
            {
                SoundStopped(this);
            }
        }
    }
}
