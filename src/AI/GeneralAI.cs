using System;
using System.Collections.Generic;
using DomainModel.Model.AI;

namespace AI
{
    public class GeneralAI : IArtificalIntelligence
    {
        public Node root { get; protected set; }
        protected Func<IState, List<Tuple<IState, IStep>>> getChildren;
        protected Func<IState, int> evaluationFunction;

        protected void generateGametree(int depthLevel, Node root)
        {
            if (depthLevel == 1) //leaves
            {
                root.value = evaluationFunction(root.state);
            }
            else
            {
                List<Tuple<IState, IStep>> children = getChildren(root.state);
                foreach (Tuple<IState, IStep> t in children)
                {
                    root.addChild(t.Item1, t.Item2);
                }
                foreach (Node n in root.children)
                {
                    generateGametree(depthLevel - 1, n);
                }
            }
        }

        public virtual IStep getNextStep()
        {
            return root.lastStep;
        }

        public void setChildrenFunction(Func<IState, List<Tuple<IState, IStep>>> getChildren_)
        {
            getChildren = getChildren_;
        }

        public void setEvaluationFunction(Func<IState, int> evaluationFunction_)
        {
            evaluationFunction = evaluationFunction_;
        }

        public void setCurrentState(IState state)
        {
            root = new Node(state);
        }
    }
}
