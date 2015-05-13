using System;
using System.Collections.Generic;
using DomainModel.Model.AI;

namespace AI
{
    public class AlphaBetaAI : GeneralAI
    {
        public AlphaBetaAI()
        {
            root = new Node(null);
        }
        private int alphabeta(Node root, int alpha, int beta, bool maximizingPlayer)
        {
            if (root.isLeaf())
            {
                return root.value;
            }

            if (maximizingPlayer)
            {
                foreach (Node n in root.children)
                {
                    alpha = Math.Max(alpha, alphabeta(n, alpha, beta, false));
                    if (beta < alpha)
                    {
                        break;
                    }
                }
                root.value = alpha;
                return alpha;
            }
            else
            {
                foreach (Node n in root.children)
                {
                    beta = Math.Min(beta, alphabeta(n, alpha, beta, true));
                    if (beta < alpha)
                    {
                        break;
                    }
                }
                root.value = beta;
                return beta;
            }
        }

        public override IStep getNextStep()
        {
            generateGametree(10, root);
            int maxValue = alphabeta(root, Int32.MinValue, Int32.MaxValue, true);
            IStep result = root.children.Find(x => x.value == maxValue).lastStep;
            return result;
        }
    }
}
