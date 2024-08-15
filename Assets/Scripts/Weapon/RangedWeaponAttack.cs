using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeaponAttack : WeaponAttack
{
    [SerializeField] WeaponData weaponData;

    [SerializeField] bool dontRotateProjectile;

    [SerializeField] bool hasAttackAnimation;


    PlayerWeapon playerWeapon;

    GameObject projectile;

    GameObject shotProjectile;

    Animator animator;

    AudioSource audioSource;
    float volumeMultiplier;

    AudioClip attackSFX;
    AudioClip impactSFX;


    void Start()
    {
        playerWeapon = FindObjectOfType<PlayerWeapon>();
        UpdateWeapon();
        animator = GetComponent<Animator>();

        audioSource = playerWeapon.GetPlayerAudioSource();
        volumeMultiplier = audioSource.volume;
        attackSFX = weaponData.attackSFX;
        impactSFX = weaponData.impactSFX;
    }

    public override void UpdateWeapon()
    {
        projectile = weaponData.projectile;
    }

    public override IEnumerator Attack()
    {
        if (attackSFX != null)
        {
            audioSource.PlayOneShot(attackSFX, weaponData.attackVolume * volumeMultiplier);
        }
        if (hasAttackAnimation)
        {
            animator.SetTrigger("Attack");
            yield return new WaitForSecondsRealtime(weaponData.animationDelay);
        }
        ShootProjectile();
        playerWeapon.DecreaseDurability();
        yield return new WaitForSecondsRealtime(weaponData.attackDelay);
        if (playerWeapon != null)
        {
            playerWeapon.SetCanAttack(true);
        }

    }

    void ShootProjectile()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        if (dontRotateProjectile)
        {
            shotProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
        }
        else
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            shotProjectile = Instantiate(projectile, transform.position, rotation);
        }
        
        Projectile projectileScript = shotProjectile.GetComponent<Projectile>();
        projectileScript.SetDamage(weaponData.damage);
        projectileScript.SetImpactSFX(impactSFX, weaponData.impactVolume * volumeMultiplier);
        Destroy(shotProjectile, weaponData.projectileLife);
        Rigidbody2D rb = shotProjectile.GetComponent<Rigidbody2D>();
        rb.velocity = direction.normalized * weaponData.projectileSpeed;
    }
}
