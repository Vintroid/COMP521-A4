using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;
using TMPro;

public class TaskCheckTreasure : TreeNode
{
    // Fields for world space analysis
    GameObject minotaur;
    GameManager gameManager;
    TMPro.TMP_Text taskText;

    public TaskCheckTreasure(GameObject minotaur, GameManager gameManager,
        TMPro.TMP_Text taskText)
    {
        this.minotaur = minotaur;
        this.gameManager = gameManager;
        this.taskText = taskText;
    }

    public override NodeState Evaluate()
    {
        // Check if the minotaur is far from treasure when no targets are available
        // if so, the minotaur will come back to treasure.
        // First check if the treasure is still on the map and not stolen
        if (!gameManager.treasureStolen && Vector3.Distance(minotaur.transform.position,
            gameManager.treasurePosition) > 2f)
        {
            // Change minotaur current task text
            taskText.text = "Defend";
            taskText.color = Color.yellow;

            state = NodeState.SUCCESS;
            return state;
        }

        // Treasure close enough
        state = NodeState.FAILURE;
        return state;
    }
}
