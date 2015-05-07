using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Model.Player;

namespace DomainModel.Model.TicTacToe
{
    /// <summary>
    /// Interface for the TicTacToe game's model.
    /// </summary>
    public interface ITicTacToeModel
    {
        /// <summary>
        /// Initializes the TicTacToe model instance.
        /// </summary>
        /// <param name="p1">First player.</param>
        /// <param name="p2">Second player.</param>
        void Initialize(Player.Player p1, Player.Player p2);

        /// <summary>
        /// Gets the player who is on turn.
        /// </summary>
        Player.Player CurrentPlayer { get; }

        /// <summary>
        /// Gets the number of steps of the current game.
        /// </summary>
        Int32 StepNumber { get; }

       /// <summary>
       /// Gets the given field's value.
       /// </summary>
       /// <param name="col">Column index.</param>
       /// <param name="row">Row index.</param>
       /// <returns>The player whose mark is on the given field.</returns>
        Player.Player this[int col, int row] { get; }

        /// <summary>
        ///  Emited when the game has started.
        /// </summary>
        event EventHandler GameStarted;

        /// <summary>
        /// Emited when the game is over.
        /// </summary>
        event EventHandler<GameOverEventArgs> GameOver;

        /// <summary>
        /// Emited when a field of the board is changed.
        /// </summary>
        event EventHandler<FieldChangedEventArgs> FieldChanged;

        /// <summary>
        /// Starts a new game.
        /// </summary>
        void NewGame();

        /// <summary>
        /// Steps the game with marking the given field by the current player.
        /// </summary>
        /// <param name="col">Column index.</param>
        /// <param name="row">Row index.</param>
        void StepGame(int col, int row);
    }
}
