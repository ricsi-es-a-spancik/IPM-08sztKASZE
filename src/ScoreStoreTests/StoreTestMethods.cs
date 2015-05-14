using DomainModel;
using DomainModel.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreStoreTests
{
    public static class StoreTestMethods
    {
        public static void ReadingFromEmptyStore(IScoreStore store)
        {
            var tasks = new List<Task<IEnumerable<LeaderboardEntry>>>()
            {
                Task.Run(() => store.GetLeaderboardAsync(GameTypes.TicTacToe, Scopes.AllTime)),
                Task.Run(() => store.GetLeaderboardAsync(GameTypes.TicTacToe, Scopes.Weekly)),
                Task.Run(() => store.GetLeaderboardAsync(GameTypes.TicTacToe, Scopes.Daily))
            };

            Task.WaitAll(tasks.ToArray());

            Assert.IsFalse(tasks[0].Result.Any() || tasks[1].Result.Any() || tasks[2].Result.Any());
        }

        public static void GetAllTimeLeaders(IScoreStore store)
        {
            var entries = Mocks.Older.Concat(Mocks.Today).Concat(Mocks.Yesterday);

            foreach (var mock in entries)
            {
                Task.Run(() => store.AddLeaderboardEntryAsync(mock)).Wait();
            }

            var leaders = Task.Run(() => store.GetLeaderboardAsync(GameTypes.TicTacToe, Scopes.AllTime));
            leaders.Wait();
            Assert.IsTrue(leaders.Result.ToList().SequenceEqual(entries.ToList().OrderByDescending(l => l.Score), new LeaderboardEntryCsvComparer()));
        }

        public static void GetWeeklyLeaders(IScoreStore store)
        {
            var expectedResults = Mocks.Today.Concat(Mocks.Yesterday).ToList();
            var entries = Mocks.Older.Concat(Mocks.Today).Concat(Mocks.Yesterday);

            foreach (var mock in entries)
            {
                Task.Run(() => store.AddLeaderboardEntryAsync(mock)).Wait();
            }

            var leaders = Task.Run(() => store.GetLeaderboardAsync(GameTypes.TicTacToe, Scopes.Weekly));
            leaders.Wait();
            Assert.IsTrue(leaders.Result.ToList().SequenceEqual(expectedResults.OrderByDescending(l => l.Score), new LeaderboardEntryCsvComparer()));
        }

        public static void GetDailyLeaders(IScoreStore store)
        {
            var entries = Mocks.Older.Concat(Mocks.Today).Concat(Mocks.Yesterday);
            var expectedResults = Mocks.Today.ToList();

            foreach (var mock in entries)
            {
                Task.Run(() => store.AddLeaderboardEntryAsync(mock)).Wait();
            }

            var leaders = Task.Run(() => store.GetLeaderboardAsync(GameTypes.TicTacToe, Scopes.Daily));
            leaders.Wait();
            Assert.IsTrue(leaders.Result.ToList().SequenceEqual(expectedResults.OrderByDescending(l => l.Score), new LeaderboardEntryCsvComparer()));
        }
    }
}
