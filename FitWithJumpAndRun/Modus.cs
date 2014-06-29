using System;
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
        KinectMissing = 0,
        NotTracked,
        ButtonTutorial,
        Menu,
        Loading,
        LoadingComplete,
        Play,
        GameOver,
        Score
    }
}
