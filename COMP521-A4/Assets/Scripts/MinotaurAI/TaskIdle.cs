using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviourTree;

public class TaskIdle : TreeNode
{
    private GameObject minotaur;
    private TMPro.TMP_Text taskText;
    public TaskIdle(GameObject minotaur, TMPro.TMP_Text taskText)
    {
        this.minotaur = minotaur;
        this.taskText = taskText;
    }

    // Immobilizing the minotaur when Idle
    public override NodeState Evaluate()
    {
        // Changing the textmesh above to Idle
        taskText.text = "Idle";
        taskText.color = Color.green;

        minotaur.GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination(minotaur.transform.position);

        state = NodeState.RUNNING;
        return state;
    }
}
