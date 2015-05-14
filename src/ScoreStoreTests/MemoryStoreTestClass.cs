using System;
using System.Linq;
using DomainModel;
using DomainModel.Contracts;
using LocalScoreStores.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ScoreStoreTests
{
    [TestClass]
    public class MemoryStoreTestClass
    {
        private IScoreStore _store;

        [TestMethod]
        public void ReadingFromEmptyMemoryStore()
        {
            _store = new MemoryScoreStore();
            StoreTestMethods.ReadingFromEmptyStore(_store);
        }

        [TestMethod]
        public void GetAllTimeLeadersFromMemoryStore()
        {
            _store = new MemoryScoreStore();
            StoreTestMethods.GetAllTimeLeaders(_store);
        }

        [TestMethod]
        public void GetWeeklyLeadersFromMemoryStore()
        {
            _store = new MemoryScoreStore();
            StoreTestMethods.GetWeeklyLeaders(_store);
        }

        [TestMethod]
        public void GetDailyLeadersFromMemoryStore()
        {
            _store = new MemoryScoreStore();
            StoreTestMethods.GetDailyLeaders(_store);
        }
    }
}
