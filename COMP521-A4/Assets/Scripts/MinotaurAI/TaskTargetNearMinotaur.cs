using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public class TaskTargetNearMinotaur : TreeNode
{
    // Fields for searching around treasure
    GameObject minotaur;
    LayerMask adventurerLayer = 1 << 7;

    private TMPro.TMP_Text taskText;

    public TaskTargetNearMinotaur(GameObject minotaur, TMPro.TMP_Text taskText)
    {
        this.minotaur = minotaur;
        this.taskText = taskText;
    }

    public override NodeState Evaluate()
    {
        // Checking if there is a current target in our shared data system
        object target = GetData("target");

        // Lookout for new targets if none currently
        if (target == null)
        {
            // Looking at all adventurers close to minotaur
            Collider[] colliders = Physics.OverlapSphere(minotaur.transform.position,
                MinotaurBT.minotaurFOV, adventurerLayer);

            if (colliders.Length > 0)
            {
                // Targetting adventurer by storing gameobject in the shared data
                // Targets closest to minotaur
                parent.parent.parent.SetData("target", colliders[0].gameObject);

                // Change the text current task to moving
                taskText.text = "Moving";
                taskText.color = Color.blue;

                // Returning success for selector node
                state = NodeState.SUCCESS;
                return state;

            }

            // No target found
            state = NodeState.FAILURE;
            return state;

        }

        // Target already set
        state = NodeState.SUCCESS;
        return state;
    }
}
