using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.src.Structures;

namespace Core.src.InterFaces
{
    interface IGameApp
    {
        void Initialize(Player p1, Player p2);

        event EventHandler GameOver;
    }
}
