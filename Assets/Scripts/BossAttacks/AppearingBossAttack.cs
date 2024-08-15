using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearingBossAttack : BossAttack
{

    float appearTime;
    SpriteRenderer sprite;

    public override void _start()
    {
        base._start();
        sprite = GetComponent<SpriteRenderer>();
        switch (elementType)
        {
            case ElementType.Fire:
                sprite.color = new Color(1, 0, 0, 0);
                break;
            case ElementType.Earth:
                sprite.color = new Color(1, 0.5f, 0, 0);
                break;
            case ElementType.Water:
                sprite.color = new Color(0, 0, 1, 0);
                break;
            case ElementType.Air:
                sprite.color = new Color(0.5f, 0.5f, 1, 0);
                break;
            case ElementType.Lightning:
                sprite.color = new Color(1, 1, 0, 0);
                break;
            default:
                sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0);
                break;
        }
        
        appearTime = bossAttackData.time;

        transform.localScale *= bossAttackData.radius;
    }

    public override void _update()
    {
        base._update();
        if(appearTime > 0)
        {
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1f - appearTime / bossAttackData.time);
            appearTime -= Time.deltaTime;
            
        }
        else
        {
            TryHit();
        }
    }
}
