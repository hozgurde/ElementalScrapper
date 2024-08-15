using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] bool dontDestroy;
    [SerializeField] AudioSource projectileAudioSource;

    AudioClip impactSFX;

    int damage;
    bool alreadyHit = false;


    
    float impactVolume = 1;


    public void SetDamage(int damage)
    {
        this.damage = damage;
    }

    public void SetImpactSFX(AudioClip impactSFX)
    {
        this.impactSFX = impactSFX;
    }

    public void SetImpactSFX(AudioClip impactSFX, float impactVolume)
    {
        this.impactSFX = impactSFX;
        this.impactVolume = impactVolume;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(gameObject.name + " collided with " + collision.gameObject.name);
        if ((collision.gameObject.tag == "Boss" || collision.gameObject.tag == "Barrel") && !alreadyHit)
        {
            alreadyHit = true;
            projectileAudioSource.PlayOneShot(impactSFX, impactVolume);
            Boss boss = collision.gameObject.GetComponent<Boss>();
            boss.TakeHit(damage);
            if (!dontDestroy)
            {
                Destroy(gameObject);
            }
        }
    }
}
