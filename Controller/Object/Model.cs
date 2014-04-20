using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using View;

namespace Controller
{
    public class Model
    {
        public uint Id { get; set; }
        public string Path { get; set; }
        public bool ScalingNormalized { get; set; }

        public Model()
        {
            ScalingNormalized = false;
        }

        public Model(string path, bool scalingNormalized = false)
        {
            Path = path;
            ScalingNormalized = scalingNormalized;
            Create();
        }

        public void Create()
        {
            Id = Visualization.addModel(Path);
            while (!Visualization.isCreated(Id)) { }
            Visualization.scalingIsNormalized(Id, ScalingNormalized);
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

        public void AttachToCamera(bool choice)
        {
            Visualization.attachToCamera(Id, choice);
        }
    }
}
