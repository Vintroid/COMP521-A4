using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree
{
    // Selector composite task
    public class Selector : TreeNode
    {
        // Using constructors from base class TreeNode
        public Selector() : base() { }
        public Selector(List<TreeNode> children) : base(children) { }

        public override NodeState Evaluate()
        {
            // Iterating through the states of all children nodes
            // to evaluate as a selector
            foreach (TreeNode node in children)
            {
                switch (node.Evaluate())
                {
                    // Selector fails only if all children fail
                    // returns running or success directly
                    case NodeState.FAILURE:
                        continue;
                    case NodeState.SUCCESS:
                        state = NodeState.SUCCESS;
                        return state;
                    case NodeState.RUNNING:
                        state = NodeState.RUNNING;
                        return state;
                    default:
                        continue;
                }
            }

            // If here, then all children nodes failed
            state = NodeState.FAILURE;
            return state;

        }
    }
}
