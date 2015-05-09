using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DomainModel.Model
{
    /// <summary>
    /// Interface for the game unifying classes.
    /// </summary>
    public interface IGameApp
    {
        /// <summary>
        /// Initializes the instance with two players which can be either human or computer.
        /// </summary>
        /// <param name="p1">First player.</param>
        /// <param name="p2">Second player.</param>
        void Initialize(Player.Player p1, Player.Player p2);


        /// <summary>
        /// Emits when the user exits the current game.
        /// </summary>
        event EventHandler GameExit;

        /// <summary>
        /// Opens the game's window.
        /// </summary>
        void OpenWindow();

        /// <summary>
        /// Closes the game's window.
        /// </summary>
        void CloseWindow();
    }
}
