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
        /// Eintritspunkt des Programmes
        /// </summary>
        /// <param name="args">Eine Liste von Übergabeparametern.</param>
        static void Main(string[] args)
        {
            try
            {
                // Programm Starten
                Run r = Run.Instance;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
