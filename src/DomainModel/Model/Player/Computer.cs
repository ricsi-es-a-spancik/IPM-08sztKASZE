using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Model.Player
{
    /// <summary>
    /// Defines the computer player class.
    /// </summary>
    public class Computer : Player
    {
        /// <summary>
        /// Initializes a new instance of Computer player.
        /// </summary>
        public Computer() : base(false) { }
    }
}
