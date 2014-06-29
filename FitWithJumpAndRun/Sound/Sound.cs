/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;*/
using log4net;
using log4net.Config;


namespace JumpAndRun.Sound
{
    /// <summary>
    /// Spielt Audiodateien ab.
    /// </summary>
    class Sound
    {
        /// <summary>
        /// Delegate ruft Methode auf, wenn Sound am Ende
        /// </summary>
        /// <param name="s">Soundobjekt welches zu Ende gelaufen ist</param>
        public delegate void SoundFinishedEventHandler(Sound s);
        /// <summary>
        /// Eventhandler Informiert wenn Sound am Ende ist
        /// </summary>
        public event SoundFinishedEventHandler SoundFinished;

        private enum SoundState { None = 0, Play, Pause, Stop, Finished }
        private SoundState state = SoundState.None;

        /// <summary>Windows Media Player Objekt</summary>
        private WMPLib.WindowsMediaPlayer audio = null;
        /// <summary>Dateipfad zur Audiodatei</summary>
        public string FilePath { get; set; }
        /// <summary>Wiedergabelautstärke</summary>
        private int _Volume = 100;
        /// <summary>Wiergabe wiederholen</summary>
        private bool _Loop;
        private System.DateTime FadeOutTime = System.DateTime.Now;
        /// <summary>Logger</summary>
        private static readonly ILog log = LogManager.GetLogger(typeof(Sound).Name);
        /// <summary>Wiedergabelautstärke</summary>
        public int Volume
        {
            get { return _Volume; }
            set
            {
                if (CreateObject())
                {
                    _Volume = value;
                    try
                    {
                        System.Threading.Thread.Sleep(20); // Kann sonst zu exception führen
                    audio.settings.volume = _Volume;
                }
                    catch (System.Exception e)
                    {
                        log.Error("Beim Versuch die Lautstaerke zu erhoehen ist ein Fehler aufgetreten. Datei: " + this.FilePath);
                    }
            }
        }
        }
        /// <summary>Wiedergabe wiederholen</summary>
        public bool Loop
        {
            get { return _Loop; }
            set
            {
                _Loop = value;
                if (CreateObject())
                {
                    System.Threading.Thread.Sleep(20); // Kann sonst zu exception führen
                audio.settings.setMode("loop", _Loop);
            }
        }
        }

        /// <summary>
        /// Sound erstellen
        /// </summary>
        public Sound(string file)
        {
            FilePath = file;
            log.Debug("Neuer Sound wird erstellt: " + FilePath);
            try
            {
                if (CreateObject())
                {
                    
                }
            }
            catch (System.Exception e)
            {
                log.Error("Sounderror: " + FilePath);
                log.Error(e.ToString());
            }
        }

        private bool CreateObject()
        {
            if (audio == null)
            {
                try
                {
                    audio = new WMPLib.WindowsMediaPlayer();
                    audio.URL = FilePath;
                    System.Threading.Thread.Sleep(10); // Kann sonst zu exception führen
                    audio.settings.volume = Volume;
                    System.Threading.Thread.Sleep(10); // Kann sonst zu exception führen
                    audio.PlayStateChange += new WMPLib._WMPOCXEvents_PlayStateChangeEventHandler(Sound_PlayStateChange);
                    
                }
                catch (System.Exception e)
                {
                    log.Error("Beim Versuch ein Audio Objekt zu erzeugen ist eine Ausnahme aufgetreten: \n " + e.ToString());
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Sound abspielen
        /// </summary>
        public void Play()
        {
            // wenn diese Methode zu schnell zu oft aufgerufen wird, wird eine exception geworfen
            try
            {
                // Sound nur neu starten, wenn er noch nicht läuft
                if (state != SoundState.Play)
                {
                    state = SoundState.Play;

                    if (CreateObject())
                    {
                            audio.controls.play();
                            //log.Debug("audio: " + audio.currentMedia.name);
                    }
                }
            }
            catch (System.Runtime.InteropServices.COMException e)
            {
                log.Error("Sound Play Excpetion: " + e);
            }
            catch (System.Exception e)
            {
                log.Error("Fehler beim starten eines Sounds: " + this.FilePath);
            }
        }

        /// <summary>
        /// Sound Stoppen
        /// </summary>
        public void Stop()
        {
            state = SoundState.Stop;

            if (CreateObject())
            {
                try
                {
                audio.controls.stop();
            }
                catch (System.Exception e)
                {
                    log.Error("Fehler beim stoppen eines Sounds: " + this.FilePath);
        }
            }
        }

        /*/// <summary>
        /// Sound langsam ausklingen lassen
        /// </summary>
        /// <param name="time">Zeit in Sekunden</param>
        public void FadeOut(int time = 1)
        {
            if (Volume > 0)
            {
                int sleep = (time * 1000) / Volume;


                while (Volume > 0)
                {
                    //System.Threading.Thread.Sleep(sleep);
                    Volume--;
                }
            }
            Stop();
        }*/

        /// <summary>
        /// Abfangen wenn Sound zu Ende gelaufen ist
        /// </summary>
        /// <param name="NewState">aktueller Soundstatus</param>
        private void Sound_PlayStateChange(int NewState)
        {
            if ((WMPLib.WMPPlayState)NewState == WMPLib.WMPPlayState.wmppsStopped)
            {
                if (state != SoundState.Stop)
                {
                    state = SoundState.Finished;
                    if (SoundFinished != null) SoundFinished(this);
                }
            }
        }
    }
}
