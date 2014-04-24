using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    public enum GameStatus : short
    {
        Nothing = 0,
        Loadet = 1,
        Started = 2,
        Successful = 3,
        GameOver = 4,
    }
}
