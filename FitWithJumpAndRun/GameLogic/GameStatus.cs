using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JumpAndRun.GameLogic
{
    /// <summary>
    /// Enum für den Status des Spiels.
    /// </summary>
    public enum GameStatus : short
    {
        Loading = 0,
        Start = 1,
        LoadingComplete = 2,
        Playing = 3,
        Successful = 4,
        GameOver = 5
    }
}
