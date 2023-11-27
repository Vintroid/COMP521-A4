using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;


public class TaskAttack : TreeNode
{
    GameManager gameManager;

    // Target info
    Agent agent;

    TaskAttack(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    public override NodeState Evaluate()
    {
        GameObject target = (GameObject)GetData("target");

        // Checking if target is still present
        if (target != null)
        {
            // Checking if attack is on cooldown or not
            if (MinotaurBT.attackCooldown <= MinotaurBT.attackTimer)
            {
                // Make the agent take a hit
                agent = target.GetComponent<Agent>();
                agent.TakeHit();
                AudioSource.PlayClipAtPoint(gameManager.minotaurSmash, Vector3.zero, 0.5f);

                // Resetting attack timer
                MinotaurBT.attackTimer = 0f;

                // Clearing target for next plan
                ClearData("target");

                state = NodeState.SUCCESS;
                return state;
            }
        }
        
        state = NodeState.FAILURE;
        return state;
    }
}
