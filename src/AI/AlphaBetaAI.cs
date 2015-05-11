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

        public override IStep getNextStep()
        {
            generateGametree(10, root);
            int maxValue = alphabeta(root, Int32.MinValue, Int32.MaxValue, true);
            System.Diagnostics.Debug.WriteLine(maxValue);
            IStep result = root.children.Find(x => x.value == maxValue).lastStep;
            return result;
        }
    }
}
