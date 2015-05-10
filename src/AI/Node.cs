using System;
using System.Collections.Generic;
using DomainModel.Model.AI;

namespace AI
{
    class Node : INode
    {
        public int value { get; set; }
        public IState state { get; set; }
        public IStep lastStep { get; private set; }
        public List<Node> children { get; private set; }

        public Node(IState x_, IStep lastStep_ = null)
        {
            state = x_;
            children = new List<Node>();
            lastStep = lastStep_;
            value = 0;
        }

        public void addChild(IState state, IStep step)
        {
            Node child = new Node(state, step);
            children.Add(child);
        }

        public bool isLeaf()
        {
            return (children.Count == 0);
        }
    }
}
