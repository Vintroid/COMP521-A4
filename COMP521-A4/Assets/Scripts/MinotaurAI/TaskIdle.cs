using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviourTree;

public class TaskIdle : TreeNode
{
    private GameObject minotaur;
    private TMPro.TMP_Text taskText;
    public TaskIdle(GameObject obj, TMPro.TMP_Text taskText)
    {
        this.minotaur = obj;
        this.taskText = taskText;
    }

    // Immobilizing the minotaur when Idle
    public override NodeState Evaluate()
    {
        // Changing the textmesh above to Idle
        taskText.text = "Idle";
        taskText.color = Color.green;

        minotaur.GetComponent<UnityEngine.AI.NavMeshAgent>().velocity = Vector3.zero;
        minotaur.GetComponent<UnityEngine.AI.NavMeshAgent>().isStopped = true;

        state = NodeState.RUNNING;
        return state;
    }
}
