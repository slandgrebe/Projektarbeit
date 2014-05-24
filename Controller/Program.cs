using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MotionDetection;
using View;

/// <summary>
/// Start Klasse
/// \mainpage
/// Project name
/// ==================
/// bla
/// Important classes
/// ------------------
/// * Class1
/// * Class2
/// * Class3
/// </summary>
namespace JumpAndRun
{

    class Program
    {
        internal Run Run
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
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
