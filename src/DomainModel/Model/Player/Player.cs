using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Model.Player
{
    /// <summary>
    /// Base class of the player types.
    /// </summary>
    public abstract class Player
    {
        /// <summary>
        /// Indicates whether the current player is a human or computer.
        /// </summary>
        public bool IsHuman { get; private set; }

        /// <summary>
        /// Score of the player in the current game.
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// Initializes the player object.
        /// </summary>
        /// <param name="isHuman">Determines the type of the player.</param>
        public Player(bool isHuman)
        {
            Score = 0;
            IsHuman = isHuman;
        }
    }
}
