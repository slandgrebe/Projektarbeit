using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MotionDetection;
using View;
using log4net;
using log4net.Config;
using log4net.Appender;
using log4net.Core;

/// \mainpage
/// @author Tobias Karth
/// Fit with Jump & Run
/// ==================
/// Fit with Jump & Run ist ein Teil der Projektarbeit der KTSI. Unter Verwendung der beiden anderen Teilprojekte MotionDetection und VisualizationLibrary kann eine Figur durch eigene Körperbewegungen in einem Spiel bewegt werden.
namespace JumpAndRun
{
    /// <summary>
    /// Eintritspunkt des Programmes.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Mögliche Zustände der Applikation (Release, Debug)
        /// </summary>
        public enum State { Release, Debug, Trace }
        /// <summary>
        /// Zustand der Applikation
        /// </summary>
        public static State state = State.Debug;
        /// <summary>Logger</summary>
        private static readonly ILog log = LogManager.GetLogger(typeof(Program).Name);
        private RollingFileAppender appender = new RollingFileAppender();

        /// <summary>
        /// Eintritspunkt des Programmes
        /// </summary>
        /// <param name="args">Eine Liste von Übergabeparametern.</param>
        static void Main(string[] args)
        {
            try
            {
                if (args.Length > 0)
                {
                    if (args[0].Equals("debug"))
                    {
                        Program.state = State.Debug;
                        log.Info("Debug Modus aktiviert");
                    }
                    else if (args[0].Equals("release")) 
                    {
                        Program.state = State.Release;
                        log.Info("Release Modus aktiviert");
                        System.Windows.Forms.Cursor.Hide();
                    }
                }

                if (state == State.Debug)
                {
                    ((log4net.Repository.Hierarchy.Hierarchy)LogManager.GetRepository()).Root.Level = Level.Trace;
                }
                else if (state == State.Release)
                {
                    ((log4net.Repository.Hierarchy.Hierarchy)LogManager.GetRepository()).Root.Level = Level.Warn;
                }

                // Programm Starten
                Run r = Run.Instance;
            }
            catch (Exception e)
            {
                log.Fatal(e);
                Console.ReadLine();
            }
        }
    }
}
