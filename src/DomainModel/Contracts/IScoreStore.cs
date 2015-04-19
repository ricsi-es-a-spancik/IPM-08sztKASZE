//-----------------------------------------------------------------------
// <copyright file="IScoreStore.cs" company="Wonderful Components">
//     Copyright (c) Faculty of Informatics, University of Eötvös Loránd,
//     Budapest, Hungary. All rights reserved.
// </copyright>
// <author>David Hornyák</author>
//-----------------------------------------------------------------------

namespace DomainModel.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Represent a store which is able to store and retrieve leaderboard information.
    /// </summary>
    public interface IScoreStore
    {
        /// <summary>
        /// Gets the leaderboard for a specific game type and a scope.
        /// </summary>
        /// <param name="gameType">Type of the game.</param>
        /// <param name="scope">Scope of the leaderboard in time.</param>
        /// <returns>A leaderboard containing the results.</returns>
        Task<IEnumerable<LeaderboardEntry>> GetLeaderboardAsync(GameTypes gameType, Scopes scope);

        /// <summary>
        /// Adds a new entry to the leaderboards.
        /// </summary>
        /// <param name="entry">Leaderboard entry to be added.</param>
        /// <returns>Void result of task execution.</returns>
        Task AddLeaderboardEntryAsync(LeaderboardEntry entry);
    }
}
