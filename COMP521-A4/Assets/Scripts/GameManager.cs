using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // World State Variables
    public bool treasureStolen = false;
    public GameObject currentTreasureCarrier;
    public Vector3 treasurePosition;
    public GameObject currentMinotaurAttacker;

    // Actors
    [SerializeField] public GameObject minotaur;
    [SerializeField] public GameObject melee1;
    [SerializeField] public GameObject melee2;
    [SerializeField] public GameObject range1;
    [SerializeField] public GameObject range2;

    // Prefabs
    [SerializeField] public GameObject treasure;

    // SFX
    [SerializeField] public AudioClip minotaurSmash;

    void Awake()
    {
        // Base position of the treasure
        GameObject treasureObject = GameObject.Instantiate(treasure);
        treasurePosition = treasureObject.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
