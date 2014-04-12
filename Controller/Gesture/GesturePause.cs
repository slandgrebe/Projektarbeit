using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Controller
{
    class GesturePause
    {
        public static bool IsTrue()
        {
            //if (Body.Instance.Spine.X != 0)
            //{
                if (Body.Instance.ElbowLeft.Y < Body.Instance.ShoulderLeft.Y && Body.Instance.ElbowRight.Y < Body.Instance.ShoulderRight.Y)
                {
                    if (Math.Round(Convert.ToDouble(Body.Instance.HandLeft.X),1) == Math.Round(Convert.ToDouble(Body.Instance.HandRight.X),1))
                    {
                        return true;
                    }
                }
            //}
            return false;
        }
    }
}
