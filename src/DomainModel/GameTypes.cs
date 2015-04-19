//-----------------------------------------------------------------------
// <copyright file="GameTypes.cs" company="Wonderful Components">
//     Copyright (c) Faculty of Informatics, University of Eötvös Loránd,
//     Budapest, Hungary. All rights reserved.
// </copyright>
// <author>David Hornyák</author>
//-----------------------------------------------------------------------

namespace DomainModel
{
    /// <summary>
    /// Represents the enumeration of supported game types.
    /// </summary>
    public enum GameTypes
    {
        /// <summary>
        /// Type of the Connect Four game.
        /// </summary>
        ConnectFour = 1,

        /// <summary>
        /// Type of the Nine Men Morris game.
        /// </summary>
        NineMensMorris = 2,

        /// <summary>
        /// Type of the \Tic Tac Toe\ game.
        /// </summary>
        TicTacToe = 3
    }
}
