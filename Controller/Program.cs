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
                bool kinect = true;
                if (kinect)
                {
                    Movement move = new Movement();
                }

                Console.Read();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
