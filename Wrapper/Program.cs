using System;
using System.Runtime.InteropServices;

namespace Wrapper
{
    class Program
    {
        [DllImport("Visualization.dll", EntryPoint = "hello")]
        extern static void hello();

        static void Main(string[] args)
        {
            Console.WriteLine("hallo");
            hello();

            Console.ReadLine();
        }
    }
}
