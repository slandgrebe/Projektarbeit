using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    public enum GameStatus : short
    {
        Initial = 0,
        Start = 1,
        Loadet = 2,
        Started = 3,
        Successful = 4,
        GameOver = 5
    }
}
