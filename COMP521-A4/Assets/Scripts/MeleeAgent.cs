using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAgent : Agent
{
    // GameManager to access world space
    [SerializeField] GameManager gameManager;
    [SerializeField] TMPro.TMP_Text infoText;

    void Awake()
    {
        health = 4;
    }

    // Taking hit from the minotaur
    // Returns whether the adventurer died
    // after the attack or not.
    public override bool TakeHit()
    {
        health -= 1;
        bool isDead = false;

        if (gameManager.treasureStolen &&
            gameObject == gameManager.currentTreasureCarrier)
        {
            DropTreasure();
        }
        // Checking if adventurer is dead
        if(health <= 0)
        {
            isDead = true;
            Death();
        }

        // Updating the adventurer's health in the text above
        int index = infoText.text.LastIndexOf(",");
        string substring = infoText.text.Substring(0, index + 1);
        infoText.text = substring + " HP:" + health;

        return isDead;
    }

    // Dropping the treasure is carrying it after taking a hit
    private void DropTreasure()
    {
        gameManager.currentTreasureCarrier = null;
        gameManager.treasureStolen = false;

        // Instatiating a new treasure under the adventurer
        Vector3 pos = gameObject.transform.position;
        GameObject newTreasure = GameObject.Instantiate(gameManager.treasure,
            new Vector3(pos.x, 0.6f, pos.z), gameManager.treasure.transform.rotation);
        gameManager.treasurePosition = newTreasure.transform.position;
    }
}
