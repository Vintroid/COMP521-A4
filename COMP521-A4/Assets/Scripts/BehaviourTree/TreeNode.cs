using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree
{
    // For our tree and decision-making process to know what is the current
    // node doing
    public enum NodeState
    {
        RUNNING,
        SUCCESS,
        FAILURE
    }

    public class TreeNode
    {
        protected NodeState state;

        public TreeNode parent;
        protected List<TreeNode> children = new List<TreeNode>();

        // Dictionary for shared data between nodes
        private Dictionary<string,object> dataContext = new Dictionary<string,object>();

        public TreeNode()
        {
            parent = null;
        }

        public TreeNode(List<TreeNode> children)
        {
            foreach(TreeNode child in children) {

                AttachNode(child);
            }
        }

        // Attaching a node as a children to this node
        private void AttachNode(TreeNode node)
        {
            node.parent = this;
            children.Add(node);
        }

        // Letting this function getting overidden for children classes
        public virtual NodeState Evaluate() => NodeState.FAILURE;

        // Setter, Getter, and clear functions for manipulating the data dictionary
        public void SetData(string key, object obj)
        {
            dataContext[key] = obj;
        }

        // Recursively goes up the tree in search of a key
        public object GetData(string key)
        {
            object value = null;

            // Looking if we can get a value as output from our key
            // in the dictionary
            if(dataContext.TryGetValue(key, out value))
            {
                return value;
            }

            TreeNode node = parent;

            // iterating recursively upwards through parents
            while(node != null)
            {
                // Recursive call to parent node
                value = node.GetData(key);
                if(value != null)
                {
                    return value;
                }
                node = node.parent;

            }
            return null;
        }

        // Recursively goes upwards to erase a key,value pair
        public bool ClearData(string key)
        {
            if (dataContext.ContainsKey(key))
            {
                dataContext.Remove(key);
                return true;
            }

            TreeNode node = parent;

            // Looking upwards recursively if parent nodes can clear data
            while(node != null)
            {
                bool cleared = node.ClearData(key);
                if (cleared)
                {
                    return true;
                }

                node = node.parent;
            }

            return false;
        }


    }
}


