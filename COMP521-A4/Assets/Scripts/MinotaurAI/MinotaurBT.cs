using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviourTree;

public class MinotaurBT : BehaviourTree.Tree
{
    // Fields for characteristics
    public static float treasureFOV = 7f;
    public static float minotaurFOV = 12f;
    public static float attackRange = 3f;
    public static float aoeDistance = 3f;
    public static float attackCooldown = 1f;
    public static float attackTimer = 0f;

    // Serialized Fields for objects and text
    [SerializeField] TMPro.TMP_Text taskText;
    [SerializeField] GameManager gameManager;

    // Minotaur task tree setup using selectors and sequences
    // See AI document for diagram & explanations.
    protected override TreeNode SetupTree()
    {
        TreeNode root = new Selector(new List<TreeNode>
        {
            new Sequence(new List<TreeNode>
            {
                new TaskCheckAttackRange(gameManager.minotaur,taskText),
                new TaskAttack(gameManager)
            }),

            new Sequence(new List<TreeNode>
            {
                new Selector(new List<TreeNode>
                {
                    new TaskTargetAttacker(gameManager,gameManager.minotaur, taskText),
                    new TaskTargetNearTreasure(gameManager, taskText),
                    new TaskTargetNearMinotaur(gameManager.minotaur, taskText)
                }),

                new TaskMoveTarget(gameManager.minotaur)
            }),

            new Selector(new List<TreeNode>
            {
                new Sequence(new List<TreeNode>
                { 
                    new TaskCheckTreasure(gameManager.minotaur,gameManager,taskText),
                    new TaskMoveTreasure(gameManager,gameManager.minotaur)
                }),

                new TaskIdle(gameManager.minotaur,taskText)
            })
            
        });

        return root;
    }

    // Minotaur attack cooldown has be tracked
    private void Update()
    {
        if (attackTimer <= 1f)
        {
            attackTimer += Time.deltaTime;
        }
    }
}
