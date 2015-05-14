using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreStoreTests
{
    public class LeaderboardEntryCsvComparer : IEqualityComparer<LeaderboardEntry>
    {

        public bool Equals(LeaderboardEntry x, LeaderboardEntry y)
        {
            return (x.ToCsvString() == y.ToCsvString());
        }

        public int GetHashCode(LeaderboardEntry obj)
        {
            return obj.GetHashCode();
        }
    }
}
