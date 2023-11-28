using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public class TaskMoveTreasure : TreeNode
{
    GameObject minotaur;
    GameManager gameManager;

    public TaskMoveTreasure(GameManager gameManager,GameObject minotaur)
    {
        this.minotaur = minotaur;
        this.gameManager = gameManager;
    }

    public override NodeState Evaluate()
    {
        // Closing distance between minotaur and treasure if not right next to it
        if (Vector3.Distance(minotaur.transform.position,
            gameManager.treasurePosition) > 0.5f)
        {
            // Move the minotaur navmesh agent in the navmesh
            minotaur.GetComponent<UnityEngine.AI.NavMeshAgent>().
                SetDestination(gameManager.treasurePosition);

            minotaur.transform.LookAt(gameManager.treasurePosition);
        }

        // Return a continuous running state so that sequence keeps running
        state = NodeState.RUNNING;
        return state;
    }
}
