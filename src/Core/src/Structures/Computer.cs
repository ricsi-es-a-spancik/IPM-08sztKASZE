using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.src.InterFaces;


namespace Core.src.Structures
{
    public class Computer : Player
    {
        protected IArtificalIntelligence AIcomp;

        public Computer(IArtificalIntelligence AI) : base(false)
        {
            this.AIcomp = AI;
        }

        public List<Int32> getNextStep()
        {
            return new List<Int32>();
        }
    }
}
