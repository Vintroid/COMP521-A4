using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public class TaskMoveTarget : TreeNode
{
    GameObject minotaur;
    
    public TaskMoveTarget(GameObject minotaur)
    {
        this.minotaur = minotaur;
    }

    public override NodeState Evaluate()
    {
        // Get target's position from shared data
        GameObject targetObj = (GameObject)GetData("target");
        Transform target = targetObj.transform;

        // Closing distance between minotaur and target if not right next to it
        if(Vector3.Distance(minotaur.transform.position,target.position) > 0.5f)
        {
            // Move the minotaur navmesh agent in the navmesh
            minotaur.GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination(target.position);
            minotaur.transform.LookAt(target.position);
        }

        // Return a continuous running state so that sequence keeps running
        state = NodeState.RUNNING;
        return state;
    }
}
