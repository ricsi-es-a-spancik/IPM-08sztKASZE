using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.src.InterFaces
{
    public class State { }
    public class Step { }
    public class Node { }

    public interface IArtificalIntelligence
    {
        void generateGametree(int depthLevel, Node root);
        
        Step getNextStep();
        void setChildrenFunction(Func<State, List<Tuple<State, Step>>> getChildren_);
        void setEvaluationFunction(Func<State, int> evaluationFunction_);
        void setCurrentState(State state);
    }
}
