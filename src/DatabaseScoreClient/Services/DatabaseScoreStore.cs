//-----------------------------------------------------------------------
// <copyright file="DatabaseScoreStore.cs" company="Wonderful Components">
//     Copyright (c) Faculty of Informatics, University of Eötvös Loránd,
//     Budapest, Hungary. All rights reserved.
// </copyright>
// <author>David Hornyák</author>
//-----------------------------------------------------------------------

namespace DatabaseScoreClient.Services
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using DomainModel;
    using DomainModel.Services;
    using Models;

    /// <summary>
    /// Represents a score store which uses a relational database as persistent data source.
    /// </summary>
    public class DatabaseScoreStore : EnumerableScoreStoreBase
    {
        /// <summary>
        /// Adds a new entry to the leaderboards.
        /// </summary>
        /// <param name="entry">New entry to be added.</param>
        /// <returns>Void result of task execution.</returns>
        public override async Task AddLeaderboardEntryAsync(LeaderboardEntry entry)
        {
            using (var context = new ScoreDbContext())
            {
                context.Leaderboards.Add(entry);
                await context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Returns an expression for filtering leaderboard entries from the actual week
        /// using database specific time formats.
        /// </summary>
        /// <param name="gameType">Game type of the leaderboard.</param>
        /// <returns>A searching expression.</returns>
        protected override Expression<Func<LeaderboardEntry, bool>> GetWeeklyLeaderboardQuery(GameTypes gameType)
        {
            var now = DateTime.Now.Date;
            var dow = now.DayOfWeek == DayOfWeek.Sunday ? 7 : (int)now.DayOfWeek;
            var firstDayOfWeek = now.AddDays(-1.0 * (dow - 1));
            var lastDayOfWeek = now.AddDays(7 - dow);

            return
                l => l.GameType == gameType &&
                DbFunctions.TruncateTime(l.Date) >= DbFunctions.TruncateTime(firstDayOfWeek) &&
                DbFunctions.TruncateTime(l.Date) <= DbFunctions.TruncateTime(lastDayOfWeek);
        }

        /// <summary>
        /// Returns an expression for filtering leaderboard entries from the actual day
        /// using database specific time formats.
        /// </summary>
        /// <param name="gameType">Game type of the leaderboard.</param>
        /// <returns>A searching expression.</returns>
        protected override Expression<Func<LeaderboardEntry, bool>> GetDailyLeaderboardQuery(GameTypes gameType)
        {
            return
                l => l.GameType == gameType &&
                DbFunctions.TruncateTime(l.Date) == DbFunctions.TruncateTime(DateTime.Now);
        }

        /// <summary>
        /// Filters the leaderboard objects by a specific search condition.
        /// </summary>
        /// <param name="searchCondition">Condition which the leaderboard entries searched by.</param>
        /// <returns>The filtered leaderboard.</returns>
        protected override async Task<IEnumerable<LeaderboardEntry>> GetEntries(Expression<Func<LeaderboardEntry, bool>> searchCondition)
        {
            using (var context = new ScoreDbContext())
            {
                var query = context.Leaderboards.Where(searchCondition).OrderByDescending(l => l.Score).Select(l => l);
                return await query.ToArrayAsync();
            }
        }
    }
}