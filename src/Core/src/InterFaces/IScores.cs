using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.src.InterFaces
{
    interface IScores
    {
        void WriteScore(Int64 score);

        List<Int64> GetScores();
    }
}
