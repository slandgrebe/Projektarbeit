using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JumpAndRun.Gui.Elements
{
    /// <summary>
    /// Cursor Klasse
    /// </summary>
    public class Cursor
    {
        private static Cursor instance = null;
        private JumpAndRun.Gui.Elements.Point cursor = null;
        /// <summary>
        /// X Koordinate des Cursors
        /// </summary>
        public float X { get; private set; }
        /// <summary>
        /// Y Koordinate des Cursors
        /// </summary>
        public float Y { get; private set; }

        private float z = -0.2f; // diesen wert nicht ändern!

        private float clickZCached = 0f;
        private float clickXCached = 0f;
        private float clickYCached = 0f;
        private float clickZPotentialStart = 0f;
        private float clickXPotentialStart = 0f;
        private float clickYPotentialStart = 0f;
        private DateTime clickTimePotentialStart = DateTime.Now;
        private DateTime lastClick = DateTime.Now;

        /// <summary>
        /// Delegate für das Cursor Bewegungs Event
        /// </summary>
        /// <param name="x">x Koordinate</param>
        /// <param name="y">y Koordinate</param>
        public delegate void Move(float x, float y);
        /// <summary>
        /// Cursor Bewegungs Event
        /// </summary>
        public event Move MoveEvent;

        /// <summary>
        /// Delegate für das Klick Event
        /// </summary>
        public delegate void Click();
        /// <summary>
        /// Klick Event
        /// </summary>
        public event Click ClickEvent;

        /// <summary>
        /// Singleton
        /// </summary>
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

            cursor = new JumpAndRun.Gui.Elements.Point("data/models/hand/hand.png");
            cursor.Scale(0.01f, 0.02f);
            cursor.Position(X, Y, z);
            cursor.AttachToCamera(true);
        }

        /// <summary>
        /// Setzt den Cursor neu anhand der ermittelten Personenerkennungsdaten
        /// </summary>
        public void UpdateCursor()
        {
            // nur updaten, wenn der Cursor auch angezeigt wird
            if (cursor.IsVisible)
            {
                // Position des Cursors updaten und events auslösen
                UpdateCursor(MotionDetection.Body.Instance.HandRight.X,
                    MotionDetection.Body.Instance.HandRight.Y,
                    MotionDetection.Body.Instance.HandRight.Z,
                    MotionDetection.Body.Instance.Head.X,
                    MotionDetection.Body.Instance.Head.Y,
                    MotionDetection.Body.Instance.Head.Z,
                    MotionDetection.Body.Instance.ShoulderRight.X,
                    MotionDetection.Body.Instance.ShoulderRight.Y);
            }
        }
        private void UpdateCursor(float handX, float handY, float handZ, float headX, float headY, float headZ, float shoulderX, float shoulderY)
        {
                Position(handX, handY, headX, headY, shoulderX, shoulderY);
                CheckClick(handX, handY, handZ, headX, headY, headZ, shoulderX, shoulderY);
        }
        /// <summary>
        /// Positioniert den Cursor
        /// </summary>
        /// <param name="x">X Koordinate</param>
        /// <param name="y">Y Koordinate</param>
        private void Position(float handX, float handY, float headX, float headY, float shoulderX, float shoulderY)
        {
            // Eine Einheit auf der Achse
            float xUnit = (shoulderX - headX) * 3.333f;
            float yUnit = (headY - shoulderY) * 3.333f;

            // rechte schulter ist an der position 0/0
            float xCursor = handX - shoulderX;
            float yCursor = handY - shoulderY;

            // Kinect Koordinaten in VisualisationLibrary GUI Koordinaten umrechnen
            X = xCursor * xUnit;
            Y = yCursor * yUnit;

            //cursor.Position(X / 45.5f *4, Y / 45.5f *4, z);
            float cursorAmplification = 0.1f /*/ GameLogic.Player.Instance.Scale*/;
            cursor.Position(X * cursorAmplification, Y * cursorAmplification, z);

            // event auslösen
            MoveEvent(X, Y);
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
                if ((clickZPotentialStart - clickZCached) > 0.6) // 50% vorwärts seit dem letzten rückwärts
                {
                    if (DateTime.Now.Subtract(clickTimePotentialStart).TotalSeconds < 1.0) // in unter einer sekunde ausgeführt
                    {
                        if ((Math.Abs(clickXPotentialStart - clickXCached) < 0.5) // zuviel in x-Richtung bewegt => kein absichtlicher klick
                            && (Math.Abs(clickYPotentialStart - clickYCached) < 0.5)) // zuviel in y-Richtung bewegt => kein absichtlicher klick
                        {
                            // 1s warten zwischen Klicks
                            if (DateTime.Now.Subtract(lastClick).TotalSeconds > 1)
                            {
                                lastClick = DateTime.Now;

                                // we did it!
                                if (ClickEvent != null)
                                {
                                    ClickEvent();
                                }
                            }
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

        /// <summary>
        /// Cursor anzeigen
        /// </summary>
        public void Show()
        {
            cursor.Show();  
        }
        /// <summary>
        /// Cursor verstecken
        /// </summary>
        public void Hide()
        {
            cursor.Hide();
        }
    }
}
