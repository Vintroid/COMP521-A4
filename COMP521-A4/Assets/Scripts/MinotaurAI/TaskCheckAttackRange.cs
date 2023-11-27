using BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskCheckAttackRange : TreeNode
{
    // Fields for target searching
    GameObject minotaur;
    LayerMask adventurerLayer = 1 << 7;

    TMPro.TMP_Text taskText;

    public TaskCheckAttackRange(GameObject minotaur, TMPro.TMP_Text taskText)
    {
        this.minotaur = minotaur;
        this.taskText = taskText;
    }

    public override NodeState Evaluate()
    {
        // Checking if there is a current target in our shared data system
        object target = GetData("target");

        // No attack targets fails the sequence
        if (target == null)
        {
            state = NodeState.FAILURE;
            return state;
        }

        // Looking if target is in current attack range
        Transform targetPosition = (Transform)target;
        if (Vector3.Distance(minotaur.transform.position, targetPosition.position) <= MinotaurBT.attackRange)
        {
            taskText.text = "Attacking";
            taskText.color = Color.red;
            state = NodeState.SUCCESS;
            return state;
        }

        // Target out of range
        state = NodeState.FAILURE;
        return state;

    }

}
