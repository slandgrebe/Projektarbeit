using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Controller
{
    class Click
    {
        private float HandRightStart = 0;
        private float HandLeftStart = 0;
        private DateTime StartTime = DateTime.Now;

        public bool IsClicked(){
            if (Math.Abs(StartTime.Subtract(DateTime.Now).TotalSeconds) >= 0.5)
            {
                HandRightStart = Body.Instance.HandRight.Z;
                HandLeftStart = Body.Instance.HandLeft.Z;
                StartTime = DateTime.Now;
            }


            if ((HandRightStart - Body.Instance.HandRight.Z) >= 0.3)
            {
                return true;
            }
            if ((HandLeftStart - Body.Instance.HandLeft.Z) >= 0.3)
            {
                return true;
            }
            return false;
        }
    }
}
