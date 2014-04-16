using System.Diagnostics;

namespace VisualizationLibraryTest
{
    class Utility
    {
        

        public static bool CheckCreation(uint id, float seconds)
        {
            Stopwatch sw = Stopwatch.StartNew();
            bool result = false;

            while (sw.ElapsedMilliseconds / 1000 < seconds)
            {
                if (Library.isCreated(id))
                {
                    result = true;
                    break;
                }
            }

            sw.Stop();

            return result;
        }

        public static bool InitWindow()
        {
            if (!Library.init("Test", false, 640, 480))
            {
                return false;
            }

            return true;
        }

        public static bool TearDown(uint id)
        {
            Library.close();
            Library.dispose(id);
            if (!CheckCreation(id, 1)) // sollte nicht mehr existieren
            {
                return false;
            }

            return true;
        }

        public static uint SetupPoint()
        {
            // Setup
            if (!InitWindow())
            {
                return 0; // da ging was schief
            }

            uint id = Library.addPoint("data/textures/sample.png");

            if (!CheckCreation(id, 1))
            {
                return 0; // da ging was schief
            }

            return id;
        }
    }
}
