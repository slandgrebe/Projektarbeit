using System;
using System.Runtime.InteropServices;

namespace Wrapper
{
    class Program
    {
        [DllImport("Visualization.dll", EntryPoint = "doSomething")]
        extern static void doSomething(string text);

        static void Main(string[] args)
        {
            Console.WriteLine("hallo");
            doSomething("blabla");

            Console.ReadLine();
        }
    }
}
