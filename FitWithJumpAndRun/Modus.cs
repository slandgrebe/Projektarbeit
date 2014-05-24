﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JumpAndRun
{
    /// <summary>
    /// Enum für den Programmmodus
    /// </summary>
    public enum Modus : short
    {
        NotTracked = 1,
        Menu = 2,
        Play = 3,
        GameOver = 4,
        Score = 5
    }
}
