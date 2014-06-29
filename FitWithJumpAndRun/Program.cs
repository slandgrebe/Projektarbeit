using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MotionDetection;
using View;

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
        /// <summary>
        /// Log Methode welche die Meldung nur dann schreibt, wenn der Debugmodus aktiviert ist
        /// </summary>
        /// <param name="message">zu loggende Nachricht</param>
        public static void Log(string message) 
        {
            if (state == State.Debug)
            {
                Console.WriteLine(message);
            }
        }
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
                        Log("Debug Modus aktiviert");
                    }
                    else if (args[0].Equals("release")) 
                    {
                        Program.state = State.Release;
                        Console.WriteLine("Release Modus aktiviert");
                        System.Windows.Forms.Cursor.Hide();
                    }
                }

                // Programm Starten
                Run r = Run.Instance;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.ReadLine();
            }
        }
    }
}
