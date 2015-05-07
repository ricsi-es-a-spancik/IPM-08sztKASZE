using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Model.TicTacToe
{
    /// <summary>
    /// Type of the argumentum of the game over event.
    /// </summary>
    public class GameOverEventArgs
    {
        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="winner">The player who has won the current game.</param>
        public GameOverEventArgs(Player.Player winner)
        {
            Winner = winner;
        }

        public Player.Player Winner { get; private set; }
    }
}
