using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponAttack : WeaponAttack
{
    [SerializeField] WeaponData weaponData;

    Collider2D attackArea;

    PlayerWeapon playerWeapon;

    Animator animator;

    AudioSource audioSource;
    float volumeMultiplier;

    AudioClip attackSFX;
    AudioClip impactSFX;


    bool alreadyHit = false;
    void Start()
    {
        playerWeapon = FindObjectOfType<PlayerWeapon>();
        UpdateWeapon();
        animator= GetComponent<Animator>();
        audioSource = playerWeapon.GetPlayerAudioSource();
        volumeMultiplier = audioSource.volume;
        attackSFX = weaponData.attackSFX;
        impactSFX = weaponData.impactSFX;
    }

    public override void UpdateWeapon()
    {
        //attackArea = GetComponentInChildren<PolygonCollider2D>();
        attackArea = GetComponent<Collider2D>();
        attackArea.enabled = false;
    }

    public override IEnumerator Attack()
    {
        attackArea.enabled = true;

        if (attackSFX!= null) 
        { 
            audioSource.PlayOneShot(attackSFX, weaponData.attackVolume * volumeMultiplier);
        }

        if (animator != null)
        {
            animator.SetTrigger("Attack");
            yield return new WaitForSeconds(weaponData.animationDelay);
        }
        playerWeapon.DecreaseDurability();

        yield return new WaitForSeconds(Mathf.Max(weaponData.attackDelay - weaponData.animationDelay,0));
        if (attackArea != null && playerWeapon != null )
        {
            attackArea.enabled = false;
            playerWeapon.SetCanAttack(true);
            alreadyHit = false;
        }
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Barrel")
        {
            audioSource.PlayOneShot(impactSFX, weaponData.impactVolume * volumeMultiplier);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "Boss" && !alreadyHit)
        {
            audioSource.PlayOneShot(impactSFX, weaponData.impactVolume * volumeMultiplier);
            Boss boss = collision.gameObject.GetComponent<Boss>();
            boss.TakeHit(weaponData.damage);
            alreadyHit = true;
        }
    }
}
