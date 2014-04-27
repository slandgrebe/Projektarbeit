using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using View;

namespace Controller
{
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
                Run.Instance.Start();
                //MenuUi u = new MenuUi();
                Console.Read();

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
