using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View
{
    public class Point
    {
        private uint modelId = 0;
        private float x = 0f;
        private float y = 0f;
        private float z = -1f;
        public Point(string filename) {
            modelId = View.Model.AddPoint(filename);
            while (modelId != 0 && !View.Model.IsCreated(modelId)) { }
            View.Model.Scale(modelId, 1f, 1f, 1f);
            //View.Model.ScalingIsNormalized(modelId, true);
        }

        public void Scale(float scaleX, float scaleY)
        {
            View.Model.Scale(modelId, scaleX, scaleY, 1f);
        }
        public void Position(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            View.Model.Position(modelId, x, y, z);
        }
        public void Hide()
        {
            View.Model.Position(modelId, -100f, 0f, 0f);
        }
        public void Show()
        {
            View.Model.Position(modelId, x, y, z);
        }
    }
}
