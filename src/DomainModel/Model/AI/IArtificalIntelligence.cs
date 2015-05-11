using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Model.AI
{
    public interface IArtificalIntelligence
    {
        IStep getNextStep();
        void setChildrenFunction(Func<IState, List<Tuple<IState, IStep>>> getChildren_);
        void setEvaluationFunction(Func<IState, int> evaluationFunction_);
        void setCurrentState(IState state);
    }
}
