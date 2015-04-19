//-----------------------------------------------------------------------
// <copyright file="Configuration.cs" company="Wonderful Components">
//     Copyright (c) Faculty of Informatics, University of Eötvös Loránd,
//     Budapest, Hungary. All rights reserved.
// </copyright>
// <author>David Hornyák</author>
//-----------------------------------------------------------------------

namespace DatabaseScoreClient.Migrations
{
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using DomainModel;
    using Models;

    /// <summary>
    /// Represents a code-first database configuration class used by the Entity Framework.
    /// </summary>
    internal sealed class Configuration : DbMigrationsConfiguration<ScoreDbContext>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Configuration"/> class.
        /// </summary>
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        /// <summary>
        /// Seeds the newly created database with initial data.
        /// </summary>
        /// <param name="context">Database context which needs to be seeded.</param>
        protected override void Seed(ScoreDbContext context)
        {
            var entries = new List<LeaderboardEntry>
            {
                // Entries from the previous week
                /*new LeaderboardEntry { UserName = "user1", GameType = GameTypes.TicTacToe, Date = DateTime.Now.AddDays(-7), GameSessionId = "session1", Score = 100 },
                new LeaderboardEntry { UserName = "user1", GameType = GameTypes.TicTacToe, Date = DateTime.Now.AddDays(-7), GameSessionId = "session2", Score = 120 },
                new LeaderboardEntry { UserName = "user2", GameType = GameTypes.TicTacToe, Date = DateTime.Now.AddDays(-7), GameSessionId = "session3", Score = 70 },
                new LeaderboardEntry { UserName = "user3", GameType = GameTypes.TicTacToe, Date = DateTime.Now.AddDays(-7), GameSessionId = "session4", Score = 300 },
                new LeaderboardEntry { UserName = "user4", GameType = GameTypes.TicTacToe, Date = DateTime.Now.AddDays(-7), GameSessionId = "session5", Score = 100 },

                // Entries from yesterday
                new LeaderboardEntry { UserName = "user5", GameType = GameTypes.TicTacToe, Date = DateTime.Now.AddDays(-1), GameSessionId = "session6", Score = 100 },
                new LeaderboardEntry { UserName = "user5", GameType = GameTypes.TicTacToe, Date = DateTime.Now.AddDays(-1), GameSessionId = "session7", Score = 120 },
                new LeaderboardEntry { UserName = "user6", GameType = GameTypes.TicTacToe, Date = DateTime.Now.AddDays(-1), GameSessionId = "session8", Score = 70 },
                new LeaderboardEntry { UserName = "user7", GameType = GameTypes.TicTacToe, Date = DateTime.Now.AddDays(-1), GameSessionId = "session9", Score = 300 },
                new LeaderboardEntry { UserName = "user8", GameType = GameTypes.TicTacToe, Date = DateTime.Now.AddDays(-1), GameSessionId = "session10", Score = 100 },

                // Entries from today
                new LeaderboardEntry { UserName = "user9", GameType = GameTypes.TicTacToe, Date = DateTime.Now, GameSessionId = "session11", Score = 100 },
                new LeaderboardEntry { UserName = "user9", GameType = GameTypes.TicTacToe, Date = DateTime.Now, GameSessionId = "session12", Score = 120 },
                new LeaderboardEntry { UserName = "user10", GameType = GameTypes.TicTacToe, Date = DateTime.Now, GameSessionId = "session13", Score = 70 },
                new LeaderboardEntry { UserName = "user11", GameType = GameTypes.TicTacToe, Date = DateTime.Now, GameSessionId = "session14", Score = 300 },
                new LeaderboardEntry { UserName = "user12", GameType = GameTypes.TicTacToe, Date = DateTime.Now, GameSessionId = "session15", Score = 100 }*/
            };

            foreach (var entry in entries)
            {
                context.Leaderboards.Add(entry);
            }

            context.SaveChanges();
        }
    }
}
