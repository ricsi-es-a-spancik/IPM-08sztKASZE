using DomainModel;
using DomainModel.Contracts;
using LocalScoreStores.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreStoreTests
{
    [TestClass]
    public class FileStoreTestClass
    {
        private IScoreStore _store;
        private string _filePath = @".\scores.csv";
        
        [TestInitialize]
        public void Setup()
        {
            if(File.Exists(_filePath))
            {
                File.Delete(_filePath);
            }
        }

        [TestMethod]
        public void ReadingFromEmptyFileStore()
        {
            _store = new FileScoreStore(_filePath);
            StoreTestMethods.ReadingFromEmptyStore(_store);
        }

        [TestMethod]
        public void GetAllTimeLeadersFromFileStore()
        {
            _store = new FileScoreStore(_filePath);
            StoreTestMethods.GetAllTimeLeaders(_store);
        }

        [TestMethod]
        public void GetWeeklyLeadersFromFileStore()
        {
            _store = new FileScoreStore(_filePath);
            StoreTestMethods.GetWeeklyLeaders(_store);
        }

        [TestMethod]
        public void GetDailyLeadersFromFileStore()
        {
            _store = new FileScoreStore(_filePath);
            StoreTestMethods.GetDailyLeaders(_store);
        }
    }
}
