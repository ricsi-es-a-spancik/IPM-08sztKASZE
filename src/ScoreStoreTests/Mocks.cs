using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreStoreTests
{
    public static class Mocks
    {
        public static IEnumerable<LeaderboardEntry> Today = new List<LeaderboardEntry>
            {
                // Entries from today
                new LeaderboardEntry { UserName = "user9", GameType = GameTypes.TicTacToe, Date = DateTime.Now, GameSessionId = "session11", Score = 100 },
                new LeaderboardEntry { UserName = "user9", GameType = GameTypes.TicTacToe, Date = DateTime.Now, GameSessionId = "session12", Score = 120 },
                new LeaderboardEntry { UserName = "user10", GameType = GameTypes.TicTacToe, Date = DateTime.Now, GameSessionId = "session13", Score = 70 },
                new LeaderboardEntry { UserName = "user11", GameType = GameTypes.TicTacToe, Date = DateTime.Now, GameSessionId = "session14", Score = 300 },
                new LeaderboardEntry { UserName = "user12", GameType = GameTypes.TicTacToe, Date = DateTime.Now, GameSessionId = "session15", Score = 100 }
            };

            public static IEnumerable<LeaderboardEntry> Yesterday = new List<LeaderboardEntry>
            {
                // Entries from yesterday
                new LeaderboardEntry { UserName = "user5", GameType = GameTypes.TicTacToe, Date = DateTime.Now.AddDays(-1), GameSessionId = "session6", Score = 100 },
                new LeaderboardEntry { UserName = "user5", GameType = GameTypes.TicTacToe, Date = DateTime.Now.AddDays(-1), GameSessionId = "session7", Score = 120 },
                new LeaderboardEntry { UserName = "user6", GameType = GameTypes.TicTacToe, Date = DateTime.Now.AddDays(-1), GameSessionId = "session8", Score = 70 },
                new LeaderboardEntry { UserName = "user7", GameType = GameTypes.TicTacToe, Date = DateTime.Now.AddDays(-1), GameSessionId = "session9", Score = 300 },
                new LeaderboardEntry { UserName = "user8", GameType = GameTypes.TicTacToe, Date = DateTime.Now.AddDays(-1), GameSessionId = "session10", Score = 100 }
            };

            public static IEnumerable<LeaderboardEntry> Older = new List<LeaderboardEntry>
            {
                // Entries from the previous week
                new LeaderboardEntry { UserName = "user1", GameType = GameTypes.TicTacToe, Date = DateTime.Now.AddDays(-7), GameSessionId = "session1", Score = 100 },
                new LeaderboardEntry { UserName = "user1", GameType = GameTypes.TicTacToe, Date = DateTime.Now.AddDays(-7), GameSessionId = "session2", Score = 120 },
                new LeaderboardEntry { UserName = "user2", GameType = GameTypes.TicTacToe, Date = DateTime.Now.AddDays(-7), GameSessionId = "session3", Score = 70 },
                new LeaderboardEntry { UserName = "user3", GameType = GameTypes.TicTacToe, Date = DateTime.Now.AddDays(-7), GameSessionId = "session4", Score = 300 },
                new LeaderboardEntry { UserName = "user4", GameType = GameTypes.TicTacToe, Date = DateTime.Now.AddDays(-7), GameSessionId = "session5", Score = 100 }
            };
    }
}
