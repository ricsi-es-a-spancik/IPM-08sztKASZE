using System;
using System.Collections.Generic;
using DomainModel.Model.AI;

namespace AI
{
    class AlphaBetaAI : GeneralAI
    {
        private int alphabeta(Node root, int alpha, int beta, bool maximizingPlayer)
        {
            if (root.isLeaf())
            {
                return root.value;
            }

            if (maximizingPlayer)
            {
                int v = Int32.MinValue;

                foreach (Node n in root.children)
                {
                    v = Math.Max(v, alphabeta(n, alpha, beta, false));
                    alpha = Math.Max(alpha, v);
                    if (beta <= alpha)
                    {
                        break;
                    }
                }
                return v;
            }
            else
            {
                int v = Int32.MaxValue;

                foreach (Node n in root.children)
                {
                    v = Math.Min(v, alphabeta(n, alpha, beta, false));
                    beta = Math.Min(beta, v);
                    if (beta <= alpha)
                    {
                        break;
                    }
                }
                return v;
            }
        }

        public IStep getNextStep()
        {
            int maxValue = alphabeta(root, Int32.MinValue, Int32.MaxValue, true);
            IStep result = root.children.Find(x => x.value == maxValue).lastStep;
            return result;
        }
    }
}
