using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponAttack : MonoBehaviour
{
    void Start()
    {
        
    }

    public virtual void UpdateWeapon()
    {
        return;
    }

    public virtual IEnumerator Attack()
    {
        yield return new Null();
    }

    public virtual IEnumerator CastAttack()
    {
        yield return new Null();
    }


}
