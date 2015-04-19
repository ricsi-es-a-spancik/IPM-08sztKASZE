//-----------------------------------------------------------------------
// <copyright file="MemoryScoreStore.cs" company="Wonderful Components">
//     Copyright (c) Faculty of Informatics, University of Eötvös Loránd,
//     Budapest, Hungary. All rights reserved.
// </copyright>
// <author>David Hornyák</author>
//-----------------------------------------------------------------------

namespace LocalScoreStores.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using DomainModel;
    using DomainModel.Services;

    /// <summary>
    /// Represents a score store which uses an in memory list to store leaderboard entries.
    /// </summary>
    public class MemoryScoreStore : EnumerableScoreStoreBase
    {
        /// <summary>
        /// Leaderboard entries.
        /// </summary>
        private readonly List<LeaderboardEntry> _entries;

        /// <summary>
        /// Initializes a new instance of the <see cref="MemoryScoreStore"/> class.
        /// </summary>
        public MemoryScoreStore()
        {
            this._entries = new List<LeaderboardEntry>();
        }

        /// <summary>
        /// Adds a new entry to the leaderboards.
        /// </summary>
        /// <param name="entry">New entry to be added.</param>
        /// <returns>Void result of task execution.</returns>
        public override Task AddLeaderboardEntryAsync(LeaderboardEntry entry)
        {
            entry.Id = this._entries.Count + 1;
            this._entries.Add(entry);
            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// Filters the leaderboard objects by a specific search condition.
        /// </summary>
        /// <param name="searchCondition">Condition which the leaderboard entries searched by.</param>
        /// <returns>The filtered leaderboard.</returns>
        protected override async Task<IEnumerable<LeaderboardEntry>> GetEntries(Expression<Func<LeaderboardEntry, bool>> searchCondition)
        {
            var query = this._entries.Where(searchCondition.Compile()).OrderByDescending(l => l.Score).Select(l => l);
            return await Task.FromResult<IEnumerable<LeaderboardEntry>>(query.ToArray());
        }
    }
}
