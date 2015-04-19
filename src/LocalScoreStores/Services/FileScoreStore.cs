//-----------------------------------------------------------------------
// <copyright file="FileScoreStore.cs" company="Wonderful Components">
//     Copyright (c) Faculty of Informatics, University of Eötvös Loránd,
//     Budapest, Hungary. All rights reserved.
// </copyright>
// <author>David Hornyák</author>
//-----------------------------------------------------------------------

namespace LocalScoreStores.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using DomainModel;
    using DomainModel.Services;
    
    /// <summary>
    /// Represents a score store which uses a csv file to store leaderboard entries.
    /// </summary>
    public class FileScoreStore : EnumerableScoreStoreBase
    {
        /// <summary>
        /// Path of the csv file.
        /// </summary>
        private readonly string _filePath;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileScoreStore"/> class.
        /// </summary>
        /// <param name="filePath">Path of the csv file.</param>
        public FileScoreStore(string filePath)
        {
            this._filePath = filePath;
        }

        /// <summary>
        /// Adds a new entry to the leaderboards.
        /// </summary>
        /// <param name="entry">New entry to be added.</param>
        /// <returns>Void result of task execution.</returns>
        public override async Task AddLeaderboardEntryAsync(LeaderboardEntry entry)
        {
            if (!File.Exists(this._filePath))
            {
                File.WriteAllLines(this._filePath, new[] { LeaderboardEntry.GetCsvHeader(), entry.ToCsvString() });
                return;
            }

            using (var writer = new StreamWriter(this._filePath, append: true))
            {
                await writer.WriteLineAsync(entry.ToCsvString());
            }
        }

        /// <summary>
        /// Filters the leaderboard objects by a specific search condition.
        /// </summary>
        /// <param name="searchCondition">Condition which the leaderboard entries searched by.</param>
        /// <returns>The filtered leaderboard.</returns>
        protected override async Task<IEnumerable<LeaderboardEntry>> GetEntries(Expression<Func<LeaderboardEntry, bool>> searchCondition)
        {
            if (!File.Exists(this._filePath))
            {
                return Enumerable.Empty<LeaderboardEntry>();
            }

            using (var reader = new StreamReader(this._filePath))
            {
                var query = this.ParseFromCsvReader(reader).Where(searchCondition.Compile()).OrderByDescending(l => l.Score).Select(l => l);
                return await Task.FromResult<IEnumerable<LeaderboardEntry>>(query.ToArray());
            }
        }

        /// <summary>
        /// Parses a stream into leaderboard entries by splitting the line at the semicolons and
        /// initialing a new <see cref="LeaderboardEntry"/> object.
        /// </summary>
        /// <param name="reader">Stream reader object to be parsed.</param>
        /// <returns>Enumeration of leaderboard entries.</returns>
        private IEnumerable<LeaderboardEntry> ParseFromCsvReader(StreamReader reader)
        {
            string line;
            reader.ReadLine(); // burn csv header
            while ((line = reader.ReadLine()) != null)
            {
                yield return new LeaderboardEntry(line);
            }
        }
    }
}
