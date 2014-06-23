using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JumpAndRun.Gui
{
    public class Cursor
    {
        private static Cursor instance = null;
        private View.Point cursor = null;
        public float X { get; private set; }
        public float Y { get; private set; }

        private float z = -0.4f;

        public delegate void Update(float x, float y);
        public event Update UpdateEvent;

        public static Cursor Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Cursor();
                }
                return instance;
            }
        }
        
        private Cursor()
        {
            X = 0;
            Y = 0;

            cursor = new View.Point("data/models/hand/hand-stop-2.jpg");
            cursor.Scale(0.03f, 0.05f);
            cursor.Position(X, Y, z);
            cursor.AttachToCamera(true);
        }

        public void UpdatePosition()
        {
            Position(MotionDetection.Body.Instance.HandRight.X,
                MotionDetection.Body.Instance.HandRight.Y,
                MotionDetection.Body.Instance.Head.X,
                MotionDetection.Body.Instance.Head.Y,
                MotionDetection.Body.Instance.ShoulderRight.X,
                MotionDetection.Body.Instance.ShoulderRight.Y);
        }
        /// <summary>
        /// Positioniert den Cursor
        /// </summary>
        /// <param name="x">X Koordinate</param>
        /// <param name="y">Y Koordinate</param>
        private void Position(float handX, float handY, float headX, float headY, float shoulderX, float shoulderY)
        {
            float xMax = (shoulderX - headX) * 2;
            float yMax = (shoulderY - headY);

            float x = handX - headX;
            float y = headY - handY;

            float xRelative = x / xMax - 1;
            float yRelative = y / yMax + 1;

            X = xRelative;
            Y = yRelative;

            cursor.Position(X /10, Y /10, z);

            UpdateEvent(X, Y); // event auslösen

            //Console.WriteLine("Cursor: " + x + "/" + y);

            //buttonEasy.Text(truncate(xRelative, 1) + "/" + truncate(yRelative, 1));
            //buttonEasy.Text("hand: " + truncate(handX, 4) + "/" + truncate(handY, 4));
            //buttonNormal.Text("head: " + truncate(headX, 4) + "/" + truncate(headY, 4));
            //buttonDifficult.Text("shoulder: " + truncate(shoulderX, 4) + "/" + truncate(shoulderY, 4));

            //cursor.Position(x, y, -1f);
            /*if (CursorPosition(x, y))
            {
                buttonEasy.Highlight(true);
            }
            else
            {
                buttonEasy.Highlight(false);
            }*/
        }

        public void Show()
        {
            cursor.Position(X, Y, z);
        }
        public void Hide()
        {
            cursor.Position(X, Y, 1);
        }
    }
}
