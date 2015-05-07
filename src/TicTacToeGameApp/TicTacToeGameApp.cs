using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Model;
using DomainModel.Model.Player;

namespace TicTacToeGameApp
{
    public class TicTacToeGameApp : IGameApp
    {
        public void Initialize(Player p1, Player p2)
        {
            throw new NotImplementedException();
        }

        public event EventHandler GameOver;
    }
}
