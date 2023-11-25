using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree
{
    public abstract class Tree : MonoBehaviour
    {
        private TreeNode _root = null;

        protected void Start()
        {
            // Starting at the root, the tree will be setup
            _root = SetupTree();
        }

        private void Update()
        {
            if( _root != null )
            {
                _root.Evaluate();
            }
        }

        // Setup abstract function that will depends on children
        // implementation
        protected abstract TreeNode SetupTree();
    }
}

