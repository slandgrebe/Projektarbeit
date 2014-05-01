using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using View;

namespace Controller
{
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

                // Verhindert das schliessen des Programmes
                //Console.Read();
            }
            catch (DllNotFoundException)
            {
                Console.WriteLine("Benötigte dll fehlt!");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
