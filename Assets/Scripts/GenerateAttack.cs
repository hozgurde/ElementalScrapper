using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateAttack : MonoBehaviour
{
    bool triggered = false;
    public float waitTime = 1f;
    public BossAttackData bossAttackData;
    bool generated = false;
    public ElementType elementType = ElementType.None;



    protected virtual void _start()
    {
        triggered = false;
        generated = false;
    }

    protected virtual void _update()
    {
        if (triggered && !generated)
        {
            if(waitTime > 0)
            {
                waitTime -= Time.deltaTime;
            }
            else
            {
                Generate();
            }
        }
    }

    private void Start()
    {
        _start();
    }

    private void Update()
    {
        _update();
    }

    public virtual void TriggerGeneration()
    {
        triggered = true;
    }

    public virtual void Generate()
    {
        generated = true;
    }

}
