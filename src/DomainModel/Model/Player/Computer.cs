using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Model.AI;

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
        public Computer(IArtificalIntelligence ai) : base(false) 
        {
            AI = ai;
        }

        /// <summary>
        /// AI logic.
        /// </summary>
        public IArtificalIntelligence AI { get; private set; }
    }
}
