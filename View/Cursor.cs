using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View
{
    public class Cursor
    {
        private static Cursor instance = null;
        private View.Point cursor = null;
        public float X { get; private set; }
        public float Y { get; private set; }

        private float z = -0.4f; // diesen wert nicht ändern!

        private float clickZCached = 0f;
        private float clickXCached = 0f;
        private float clickYCached = 0f;
        private float clickZPotentialStart = 0f;
        private float clickXPotentialStart = 0f;
        private float clickYPotentialStart = 0f;
        private DateTime clickTimePotentialStart = DateTime.Now;

        public delegate void Move(float x, float y);
        public event Move MoveEvent;

        public delegate void Click();
        public event Click ClickEvent;

        public static Cursor Instance
        {
            get
            {
                if (instance == null)
                {
                    Console.WriteLine("new cursor");
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

        public void UpdateCursor(float handX, float handY, float handZ, float headX, float headY, float headZ, float shoulderX, float shoulderY)
        {
            if (cursor.IsVisible) // nur ausführen, wenn der cursor auch sichtbar ist
            {
                Position(handX, handY, headX, headY, shoulderX, shoulderY);
                CheckClick(handX, handY, handZ, headX, headY, headZ, shoulderX, shoulderY);
            }
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


            cursor.Position(X / 45.5f *4, Y / 45.5f *4, z);

            MoveEvent(X, Y); // event auslösen
            //MoveEvent(X, Y /10);
        }

        private void CheckClick(float handX, float handY, float handZ, float headX, float headY, float headZ, float shoulderX, float shoulderY)
        {
            float zMax = (shoulderX - headX);
            float zAbsolute = handZ - headZ;
            float zRelative = zAbsolute / zMax / 2;

            float xMax = (shoulderX - headX) * 2;
            float xAbolute = handX - headX;
            float xRelative = xAbolute / xMax - 1;

            float yMax = (shoulderY - headY);
            float yAbsolute = headY - handY;
            float yRelative = yAbsolute / yMax + 1;

            if ((clickZCached - zRelative) > 0.05) // mehr als 5% vorwärts
            {
                if ((clickZPotentialStart - clickZCached) > 0.4) // 50% vorwärts seit dem letzten rückwärts
                {
                    if (DateTime.Now.Subtract(clickTimePotentialStart).TotalSeconds < 1.0) // in unter einer sekunde ausgeführt
                    {
                        if ((Math.Abs(clickXPotentialStart - clickXCached) < 0.5) // zuviel in x-Richtung bewegt => kein absichtlicher klick
                            && (Math.Abs(clickYPotentialStart - clickYCached) < 0.5)) // zuviel in y-Richtung bewegt => kein absichtlicher klick
                        {
                            // we did it!
                            ClickEvent();
                        }
                    }

                    // reset
                    clickXPotentialStart = clickXCached;
                    clickYPotentialStart = clickYCached;
                    clickZPotentialStart = clickZCached;
                    clickTimePotentialStart = DateTime.Now;
                }
                else if (clickZCached == clickZPotentialStart) // noch keine potentielle geste erkannt => möglicher start
                {
                    clickTimePotentialStart = DateTime.Now;
                }
                clickXCached = xRelative;
                clickYCached = yRelative;
                clickZCached = zRelative;
                //ClickEvent("vorwärts " + zCached);
            }
            else if ((clickZCached - zRelative) < -0.05) // mehr als 5% rückwärts
            {
                clickXCached = xRelative;
                clickYCached = yRelative;
                clickZCached = zRelative;

                clickXPotentialStart = clickXCached;
                clickYPotentialStart = clickYCached;
                clickZPotentialStart = clickZCached;

                clickTimePotentialStart = DateTime.Now;
                //ClickEvent("rückwärts " + zCached);
            }
        }

        public void Show()
        {
            cursor.Show();
            //cursor.Position(X, Y, z);         
        }
        public void Hide()
        {
            cursor.Hide();
            //cursor.Position(X, Y, 1);
        }
    }
}
