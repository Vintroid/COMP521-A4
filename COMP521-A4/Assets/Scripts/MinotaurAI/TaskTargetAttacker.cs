using BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskTargetAttacker : TreeNode
{
    // Fields for target searching
    GameManager gameManager;
    GameObject minotaur;
    LayerMask adventurerLayer = 1 << 7;

    private TMPro.TMP_Text taskText;

    public TaskTargetAttacker(GameManager gameManager, GameObject minotaur)
    {
        this.gameManager = gameManager;
        this.minotaur = minotaur;
    }

    public override NodeState Evaluate()
    {
        // Checking if there is a current target in our shared data system
        object target = GetData("target");

        // Lookout for new targets if none currently
        if (target == null)
        {
            // Looking if last attacker still exists
            if(gameManager.currentMinotaurAttacker != null)
            {
                // Looking at all adventurers within attack range
                Collider[] colliders = Physics.OverlapSphere(minotaur.transform.position,
                    MinotaurBT.attackRange,adventurerLayer);

                if(colliders.Length > 0)
                {
                    // Looking for the minotaur attacker
                    foreach(Collider c in colliders) {
                        if(c.gameObject == gameManager.currentMinotaurAttacker)
                        {
                            // Targetting attacker by storing gameobject in the shared data
                            parent.parent.SetData("target", c.gameObject);

                            // Returning success for sequence node
                            state = NodeState.SUCCESS;
                            return state;
                        }
                    }
                }

                // No target found
                state = NodeState.FAILURE;
                return state;

            }
        }

        // Target already set
        state = NodeState.SUCCESS;
        return state;
    }

}
