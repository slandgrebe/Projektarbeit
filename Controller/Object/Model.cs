using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using View;

namespace Controller
{
    class Model
    {
        public uint Id { get; set; }

        public Model(string path)
        {
            Id = Visualization.addModel(path);
            while (!Visualization.isCreated(Id)) { }
        }

        public void Scale(float scale)
        {
            Visualization.scale(Id, scale, scale, scale);
        }

        public void Rotate(float degrees, float x, float y, float z)
        {
            Visualization.rotate(Id, degrees, x, y, z);
        }

        public void Alignment(float fromX, float fromY, float fromZ, float toX, float toY, float toZ)
        {
            Visualization.drawLine(Id, fromX, fromY, fromZ, toX, toY, toZ);
        }

        public void Position(float x, float y, float z)
        {
            Visualization.position(Id, x, y, z);
        }

    }
}
