using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Model.TicTacToe
{
    /// <summary>
    /// Type of the argumentum of the field changed event.
    /// </summary>
    public class FieldChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="x">Column index of the given field.</param>
        /// <param name="y">Row index of the given field.</param>
        /// <param name="player">The player who has marked the given field.</param>
        public FieldChangedEventArgs(int col, int row, Player.Player player)
        {
            Player = player;
            Col = col;
            Row = row;
        }

        /// <summary>
        /// Gets the player who has marked the given field.
        /// </summary>
        public Player.Player Player { get; private set; }

        /// <summary>
        /// Gets the column index of the given field.
        /// </summary>
        /// 
        public int Col { get; private set; }

        /// <summary>
        /// Gets the row index of the given field.
        /// </summary>
        public int Row { get; private set; }
    }
}
