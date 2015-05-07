using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Model.Player
{
    /// <summary>
    /// Defines the human player class.
    /// </summary>
    public class Human : Player
    {
        /// <summary>
        /// Initializes a new instance of Human player.
        /// </summary>
        public Human() : base(true) { }
    }
}
