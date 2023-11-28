using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;


public class TaskAttack : TreeNode
{
    GameManager gameManager;
    LayerMask adventurerLayer = 1 << 7;

    // Target info
    Agent agent;

    public TaskAttack(GameManager gameManager)
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
                // Make all agents in AOE take the hit
                // Looking at all adventurers within minotaur smash AOE
                Collider[] colliders = Physics.OverlapSphere(target.transform.position,
                    MinotaurBT.aoeDistance, adventurerLayer);

                foreach(Collider c in colliders)
                {
                    agent = c.gameObject.GetComponent<Agent>();
                    agent.TakeHit();
                }
                
                AudioSource.PlayClipAtPoint(gameManager.minotaurSmash, Vector3.zero, 1f);


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
