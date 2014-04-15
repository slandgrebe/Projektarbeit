using System;

namespace VisualizationLibraryTest
{
    public class Program
    {
        static void Main(string[] args)
        {
            Window.RunAllTests();
            Point.RunAllTests();

            Console.WriteLine("*******************************************");
            Console.WriteLine(Window.ResultMessage());
            Console.WriteLine(Point.ResultMessage());
            Console.Read();
        }
    }
}
