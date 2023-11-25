using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviourTree;

public class MinotaurBT : BehaviourTree.Tree
{
    // Fields for characteristics

    // Serialized Fields for objects and text
    [SerializeField] TMPro.TMP_Text taskText;
    [SerializeField] GameObject meleeAdventurer1;
    [SerializeField] GameObject meleeAdventurer2;
    [SerializeField] GameObject rangeAdventurer1;
    [SerializeField] GameObject rangeAdventurer2;
    [SerializeField] GameManager gameManager;


    // Minotaur task tree setup
    protected override TreeNode SetupTree()
    {
        TreeNode root = new TaskIdle(this.gameObject, taskText);

        return root;
    }
}
