﻿using System;
using System.Collections.Generic;
using DomainModel.Model.AI;

namespace AI
{
    class MinimaxAI : GeneralAI
    {
        private IStep minimax(Node root)
        {
            int maxValue = max(root);
            IStep result = root.children.Find(x => x.value == maxValue).lastStep;
            return result;
        }

        private int min(Node root)
        {
            if (root.isLeaf())
            {
                return root.value;
            }
            else
            {
                int lowestValue = Int32.MaxValue;

                foreach (Node n in root.children)
                {
                    int value = max(n);
                    if (value < lowestValue)
                    {
                        lowestValue = value;
                        n.value = value;
                    }
                }

                return lowestValue;
            }
        }

        private int max(Node root)
        {
            if (root.isLeaf())
            {
                return root.value;
            }
            else
            {
                int highestValue = Int32.MinValue;

                foreach (Node n in root.children)
                {
                    int value = min(n);
                    if (value > highestValue)
                    {
                        highestValue = value;
                        n.value = value;
                    }
                }

                return highestValue;
            }
        }

        public IStep getNextStep()
        {
            return minimax(root);
        }
    }
}
