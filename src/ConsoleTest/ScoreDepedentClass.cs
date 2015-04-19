//-----------------------------------------------------------------------
// <copyright file="ScoreDepedentClass.cs" company="Wonderful Components">
//     Copyright (c) Faculty of Informatics, University of Eötvös Loránd,
//     Budapest, Hungary. All rights reserved.
// </copyright>
// <author>David Hornyák</author>
//-----------------------------------------------------------------------

namespace ConsoleTest
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DomainModel;
    using DomainModel.Contracts;

    /// <summary>
    /// Represents a class which has a dependency on an IScoreStore interface.
    /// It is used to illustrate the usage of the Unity IoC container.
    /// </summary>
    public class ScoreDepedentClass
    {
        /// <summary>
        /// Represents the dependency.
        /// </summary>
        private readonly IScoreStore _store;

        /// <summary>
        /// Mock objects to populate the store.
        /// </summary>
        private readonly IEnumerable<LeaderboardEntry> _mocks = new List<LeaderboardEntry>
        {
            // Entries from the previous week
            new LeaderboardEntry { UserName = "user1", GameType = GameTypes.TicTacToe, Date = DateTime.Now.AddDays(-7), GameSessionId = "session1", Score = 100 },
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
            new LeaderboardEntry { UserName = "user12", GameType = GameTypes.TicTacToe, Date = DateTime.Now, GameSessionId = "session15", Score = 100 }
        };

        /// <summary>
        /// Initializes a new instance of the <see cref="ScoreDepedentClass"/> class.
        /// </summary>
        /// <param name="store">Resolved store object.</param>
        public ScoreDepedentClass(IScoreStore store)
        {
            this._store = store;
        }

        /// <summary>
        /// Demonstrates the functionality of the store.
        /// </summary>
        /// <returns>Void result of task execution.</returns>
        public async Task DemonstrateStore()
        {
            Console.WriteLine("Print all time leaders from an empty store:");
            this.PrintLeaderboard(await this._store.GetLeaderboardAsync(GameTypes.ConnectFour, Scopes.AllTime));

            Console.WriteLine("Populating the store...");
            foreach (var entry in this._mocks)
            {
                await this._store.AddLeaderboardEntryAsync(entry);
            }

            Console.WriteLine("Getting all time leaders:\n");
            this.PrintLeaderboard(await this._store.GetLeaderboardAsync(GameTypes.TicTacToe, Scopes.AllTime));

            Console.WriteLine("\nGetting weekly leaders:\n");
            this.PrintLeaderboard(await this._store.GetLeaderboardAsync(GameTypes.TicTacToe, Scopes.Weekly));

            Console.WriteLine("\nGetting daily leaders:\n");
            this.PrintLeaderboard(await this._store.GetLeaderboardAsync(GameTypes.TicTacToe, Scopes.Daily));
        }

        /// <summary>
        /// Formats the properties of the leaderboard entry.
        /// </summary>
        /// <param name="entry">Leaderboard entry to be formatted.</param>
        /// <returns>Formatted entry.</returns>
        private string PrettyEntry(LeaderboardEntry entry)
        {
            return string.Format("#{0} {1} {2} {3} {4}", entry.Id, entry.UserName, entry.Date.Date, entry.GameSessionId, entry.Score);
        }

        /// <summary>
        /// Prints the leaderboard received in parameter.
        /// </summary>
        /// <param name="entries">Leaderboard to be printed.</param>
        private void PrintLeaderboard(IEnumerable<LeaderboardEntry> entries)
        {
            foreach (var entry in entries)
            {
                Console.WriteLine(this.PrettyEntry(entry));
            }
        }
    }
}
