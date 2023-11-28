using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public class TaskTargetNearTreasure : TreeNode
{
    // Fields for searching around treasure
    GameManager gameManager;
    LayerMask adventurerLayer = 1 << 7;

    private TMPro.TMP_Text taskText;

    public TaskTargetNearTreasure(GameManager gameManager, TMPro.TMP_Text taskText)
    {
        this.gameManager = gameManager;
        this.taskText = taskText;
    }

    public override NodeState Evaluate()
    {
        // Checking if there is a current target in our shared data system
        object target = GetData("target");

        // Lookout for new targets if none currently
        if (target == null)
        {
            // Prioritizing the treasure thief
            if(gameManager.currentTreasureCarrier != null)
            {
                parent.parent.parent.SetData("target", gameManager.currentTreasureCarrier);

                // Change the text current task to moving
                taskText.text = "Moving";
                taskText.color = Color.blue;

                // Returning success for selector node
                state = NodeState.SUCCESS;
                return state;
            }
            
            // Looking at all adventurers close to treasure
            Collider[] colliders = Physics.OverlapSphere(gameManager.treasurePosition,
                MinotaurBT.treasureFOV, adventurerLayer);

            if (colliders.Length > 0)
            {
                // Targetting adventurer by storing gameobject in the shared data
                // Targets closest to treasure
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
