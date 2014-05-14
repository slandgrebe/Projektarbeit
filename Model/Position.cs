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
        /// <summary>Speicher x Koordinate</summary>
        private float _x = 0;
        /// <summary>Speicher y Koordinate</summary>
        private float _y = 0;
        /// <summary>Speicher z Koordinate</summary>
        private float _z = 0;
        /// <summary>x Koordinate</summary>
        public float X {
            get { return ManipulateX(_x); }
            set { _x = value; }
        }
        /// <summary>y Koordinate</summary>
        public float Y
        {
            get { return ManipulateY(_y); }
            set { _y = value; }
        }
        /// <summary>z Koordinate</summary>
        public float Z
        {
            get { return ManipulateZ(_z); }
            set { _z = value; }
        }
        /// <summary>Modifikator entlang der X-Achse</summary>
        public float XModifikator { get; set; }
        /// <summary>Modifikator entlang der Y-Achse</summary>
        public float YModifikator { get; set; }
        /// <summary>Modifikator entlang der Z-Achse</summary>
        public float ZModifikator { get; set; }
        /// <summary>Skalierwert</summary>
        public float Scale { get; set; }

        /// <summary>
        /// Modifiziert und Skaliert den X Wert.
        /// </summary>
        /// <param name="value">X Wert</param>
        /// <returns>Veränderter X Wert</returns>
        private float ManipulateX(float value){
            value += XModifikator;
            return value*Scale;
        }

        /// <summary>
        /// Modifiziert und Skaliert den Y Wert.
        /// </summary>
        /// <param name="value">Y Wert</param>
        /// <returns>Veränderter Y Wert</returns>
        private float ManipulateY(float value)
        {
            value += YModifikator;
            return value*Scale;
        }

        /// <summary>
        /// Modifiziert und Skaliert den Z Wert.
        /// </summary>
        /// <param name="value">Z Wert</param>
        /// <returns>Veränderter Z Wert</returns>
        private float ManipulateZ(float value)
        {
            value += ZModifikator;
            return value * Scale;
        }
    }
}
