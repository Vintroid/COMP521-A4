using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Agent : MonoBehaviour
{
    // Fields
    protected float health;

    public abstract bool TakeHit(); 

    // No more health means adventurer disappears from the map
    protected void Death()
    {
        Destroy(gameObject);
    }
}
