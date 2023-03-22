using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attack : MonoBehaviour
{
    public int damage;
    public Transform destination;

    public virtual void HitPlayer()
    {
        //Decrease the health of the player
    }


}
