using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.src.Structures
{
    abstract class Player
    {
        public Boolean isHuman
        {
            get;
            set;
        }

        public Int64 Score
        {
            get;
            set;
        }
        
        public Player(Boolean is_human)
        {
            Score = 0;
            isHuman = is_human;
        }
    }

}
