//-----------------------------------------------------------------------
// <copyright file="ScoreDbContext.cs" company="Wonderful Components">
//     Copyright (c) Faculty of Informatics, University of Eötvös Loránd,
//     Budapest, Hungary. All rights reserved.
// </copyright>
// <author>David Hornyák</author>
//-----------------------------------------------------------------------

namespace DatabaseScoreClient.Models
{
    using System.Data.Entity;
    using DomainModel;
    using Migrations;

    /// <summary>
    /// Represents a database context for storing leaderboards.
    /// </summary>
    public class ScoreDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScoreDbContext"/> class.
        /// </summary>
        public ScoreDbContext()
            : base("ScoreComponentDb")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ScoreDbContext, Configuration>("ScoreComponentDb"));
        }

        /// <summary>
        /// Gets or sets the table of <see cref="LeaderboardEntry"/> objects.
        /// </summary>
        public virtual DbSet<LeaderboardEntry> Leaderboards { get; set; }
    }
}