using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviourTree;

public class MinotaurBT : BehaviourTree.Tree
{
    // Fields for characteristics
    public static float treasureFOV = 2f;
    public static float minotaurFOV = 8f;
    public static float attackRange = 2f;
    public static float aoeDistance = 2f;
    public static float attackCooldown = 1f;
    public static float attackTimer = 0f;

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

    // Minotaur attack cooldown has be tracked
    private void Update()
    {
        if(attackTimer <= 1f)
        {
            attackTimer += Time.deltaTime;
        }
    }
}
