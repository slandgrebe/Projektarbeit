using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// Speichert die Position eines Punktes in einem 3D-Raum.
    /// </summary>
    public class Position
    {
        private float _x = 0;
        private float _y = 0;
        private float _z = 0;
        /// <summary>x Koordinate</summary>
        public float X {
            get { return _x; }
            set { _x = ManipulateX(value); }
        }
        /// <summary>y Koordinate</summary>
        public float Y
        {
            get { return _y; }
            set { _y = ManipulateY(value); }
        }
        /// <summary>z Koordinate</summary>
        public float Z
        {
            get { return _z; }
            set { _z = ManipulateZ(value); }
        }

        public float XModifikator { get; set; }
        public float YModifikator { get; set; }
        public float ZModifikator { get; set; }
        public float Scale { get; set; }

        private float ManipulateX(float value){
            if (XModifikator <= 0)
            {
                value += XModifikator;
            }
            else
            {
                value -= XModifikator;
            }
            return value*Scale;
        }

        private float ManipulateY(float value)
        {
            if (YModifikator <= 0)
            {
                value += YModifikator;
            }
            else
            {
                value -= YModifikator;
            }
            return value*Scale;
        }

        private float ManipulateZ(float value)
        {
            if (ZModifikator <= 0)
            {
                value += ZModifikator;
            }
            else
            {
                value -= ZModifikator;
            }
            return value * Scale;
        }
    }
}
