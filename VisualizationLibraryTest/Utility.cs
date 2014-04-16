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

        public static bool CheckRemoval(uint id, float seconds)
        {
            Stopwatch sw = Stopwatch.StartNew();
            bool result = false;

            while (sw.ElapsedMilliseconds / 1000 < seconds)
            {
                if (!Library.isCreated(id)) // das hier macht irgendwie probleme ...
                {
                    result = true;
                    break;
                }
                System.Threading.Thread.Sleep(1); // ... darum gibts das hier, was bei Model aber auch nicht mehr hilft.
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

        public static bool Dispose(uint id)
        {
            bool result = false;
            Library.dispose(id);
            if (CheckRemoval(id, 2)) // Das macht bei Models Probleme
            {
                result = true;
            }

            return result;
        }

        public static bool TearDown(uint id)
        {
            bool result = Dispose(id);

            Library.close();

            return result;
        }

        public static uint SetupModel()
        {
            // Setup
            if (!InitWindow())
            {
                return 0; // da ging was schief
            }

            uint id = Library.addModel("data/models/cube.obj");

            if (!CheckCreation(id, 2))
            {
                return 0; // da ging was schief
            }

            return id;
        }

        public static uint SetupPoint()
        {
            // Setup
            if (!InitWindow())
            {
                return 0; // da ging was schief
            }

            uint id = Library.addPoint("data/textures/sample.png");

            if (!CheckCreation(id, 2))
            {
                return 0; // da ging was schief
            }

            return id;
        }

        public static uint SetupText()
        {
            // Setup
            if (!InitWindow())
            {
                return 0; // da ging was schief
            }

            uint id = Library.addText("data/fonts/arial.ttf");

            if (!CheckCreation(id, 2))
            {
                return 0; // da ging was schief
            }

            return id;
        }

        public static uint SetupButton()
        {
            // Setup
            if (!InitWindow())
            {
                return 0; // da ging was schief
            }

            uint id = Library.addButton("data/fonts/arial.ttf");

            if (!CheckCreation(id, 2))
            {
                return 0; // da ging was schief
            }

            return id;
        }
    }
}
