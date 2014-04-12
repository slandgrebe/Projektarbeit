using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Controller
{
    class GestureClose
    {
        public static bool IsTrue()
        {
            if (Body.Instance.Spine.X != 0)
            {
                if (Body.Instance.ElbowLeft.Y < Body.Instance.ShoulderLeft.Y && Body.Instance.ElbowRight.Y < Body.Instance.ShoulderRight.Y)
                {
                    if (Body.Instance.HandLeft.X > Body.Instance.HandRight.X)
                    {
                        if (Body.Instance.HandLeft.Y > Body.Instance.ElbowLeft.Y && Body.Instance.HandRight.Y > Body.Instance.ElbowRight.Y)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
    }
}
