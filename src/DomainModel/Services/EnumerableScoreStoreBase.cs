//-----------------------------------------------------------------------
// <copyright file="EnumerableScoreStoreBase.cs" company="Wonderful Components">
//     Copyright (c) Faculty of Informatics, University of Eötvös Loránd,
//     Budapest, Hungary. All rights reserved.
// </copyright>
// <author>David Hornyák</author>
//-----------------------------------------------------------------------

namespace DomainModel.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Contracts;

    /// <summary>
    /// Represents the base class of score stores which use an underlying container
    /// implementing the IEnumerable interface.
    /// </summary>
    public abstract class EnumerableScoreStoreBase : IScoreStore
    {
        /// <summary>
        /// Gets the leaderboard for a specific game type with a given scope.
        /// </summary>
        /// <param name="gameType">Type of the game.</param>
        /// <param name="scope">Scope of the leaderboard in time.</param>
        /// <returns>A leaderboard sorted by scores descending.</returns>
        public async Task<IEnumerable<LeaderboardEntry>> GetLeaderboardAsync(GameTypes gameType, Scopes scope)
        {
            switch (scope)
            {
                case Scopes.AllTime: return await this.GetEntries(this.GetAllTimeLeaderboardQuery(gameType));
                case Scopes.Weekly: return await this.GetEntries(this.GetWeeklyLeaderboardQuery(gameType));
                case Scopes.Daily: return await this.GetEntries(this.GetDailyLeaderboardQuery(gameType));
                default: throw new ArgumentException("Scope is not supported.");
            }
        }

        /// <summary>
        /// Adds a new entry to the container.
        /// </summary>
        /// <param name="entry">Leaderboard entry to be added.</param>
        /// <returns>Void result of task execution.</returns>
        public abstract Task AddLeaderboardEntryAsync(LeaderboardEntry entry); 

        /// <summary>
        /// Filters the entries out of the container.
        /// </summary>
        /// <param name="searchCondition">Condition used as where condition.</param>
        /// <returns>A filtered leaderboard.</returns>
        protected abstract Task<IEnumerable<LeaderboardEntry>> GetEntries(Expression<Func<LeaderboardEntry, bool>> searchCondition);

        /// <summary>
        /// Gets a search condition for querying weekly leaderboards for a specific game type.
        /// </summary>
        /// <param name="gameType">Type of the game listed in the leaderboard.</param>
        /// <returns>A search condition.</returns>
        protected virtual Expression<Func<LeaderboardEntry, bool>> GetWeeklyLeaderboardQuery(GameTypes gameType)
        {
            var now = DateTime.Now.Date;
            var dow = now.DayOfWeek == DayOfWeek.Sunday ? 7 : (int)now.DayOfWeek;
            var firstDayOfWeek = now.AddDays(-1.0 * (dow - 1)).Date;
            var lastDayOfWeek = now.AddDays(7 - dow).Date;

            return l => l.GameType == gameType && l.Date.Date >= firstDayOfWeek && l.Date.Date <= lastDayOfWeek;
        }

        /// <summary>
        /// Gets a search condition for querying daily leaderboards for a specific game type.
        /// </summary>
        /// <param name="gameType">Type of the game listed in the leaderboard.</param>
        /// <returns>A search condition.</returns>
        protected virtual Expression<Func<LeaderboardEntry, bool>> GetDailyLeaderboardQuery(GameTypes gameType)
        {
            return l => l.GameType == gameType && l.Date.Date == DateTime.Now.Date;
        }

        /// <summary>
        /// Gets a search condition for querying all time leaderboards for a specific game type.
        /// </summary>
        /// <param name="gameType">Type of the game listed in the leaderboard.</param>
        /// <returns>A search condition.</returns>
        private Expression<Func<LeaderboardEntry, bool>> GetAllTimeLeaderboardQuery(GameTypes gameType)
        {
            return l => l.GameType == gameType;
        }
    }
}
