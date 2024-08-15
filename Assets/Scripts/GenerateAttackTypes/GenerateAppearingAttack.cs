using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateAppearingAttack : GenerateAttack
{
    public Transform destination;

    private float attackTime;

    protected override void _start()
    {
        base._start();

        attackTime = bossAttackData.time;
    }

    public override void Generate()
    {
        base.Generate();

        FindObjectOfType<AttackList>().GenerateProjectile(elementType, bossAttackData.radius, bossAttackData.time, transform.position, destination.position);

        var curAttack = GameObject.Instantiate(bossAttackData.attack, destination.position, destination.rotation);
        curAttack.GetComponent<AppearingBossAttack>().bossAttackData = bossAttackData;
        curAttack.GetComponent<BossAttack>().elementType = elementType;
        //curAttack.transform.RotateAround(transform.position, Vector3.forward, curAttack.transform.rotation.eulerAngles.z);
        
        foreach(var generator in curAttack.GetComponents<GenerateAttack>())
        {
            generator.elementType = elementType;
        }
    }
}
