using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.src.InterFaces
{
    class State { }
    class Step { }
    class Node { }

    public interface IArtificalIntelligence
    {
        private void generateGametree(int depthLevel, Node root);
        
        public Step getNextStep();
        public void setChildrenFunction(Func<State, List<Tuple<State, Step>>> getChildren_);
        public void setEvaluationFunction(Func<State, int> evaluationFunction_);
        public void setCurrentState(State state);
    }
}
