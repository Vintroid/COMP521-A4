using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree
{
    // Sequence composite task
    public class Sequence : TreeNode
    {
        // Using constructors from base class TreeNode
        public Sequence() : base() { }
        public Sequence(List<TreeNode> children) : base(children) { }

        public override NodeState Evaluate()
        {
            bool isAnyChildRunning = false;

            // Iterating through the states of all children nodes
            // to evaluate as a sequence
            foreach(TreeNode node in children)
            {
                switch (node.Evaluate())
                {
                    // Sequence automatically fails when one children fails
                    case NodeState.FAILURE:
                        state = NodeState.FAILURE;
                        return state;
                    case NodeState.SUCCESS:
                        continue;
                    case NodeState.RUNNING:
                        isAnyChildRunning = true;
                        continue;
                    default:
                        state = NodeState.SUCCESS;
                        return state;
                }
            }

            state = isAnyChildRunning ? NodeState.RUNNING : NodeState.SUCCESS;
            return state;

        }
    }
}
