using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldWeaponAttack : WeaponAttack
{
    [SerializeField] AudioSource weaponAudioSource;
    [SerializeField] WeaponData weaponData;
    [SerializeField] GameObject aimSymbol;

    PlayerWeapon playerWeapon;

    GameObject projectile;

    GameObject castProjectile;

    GameObject currentAimSymbol;

    Animator animator;

    Player player;

    AudioSource audioSource;
    float volumeMultiplier;

    AudioClip impactSFX;
    AudioClip attackSFX;


    void Start()
    {
        playerWeapon = FindObjectOfType<PlayerWeapon>();
        UpdateWeapon();
        animator = GetComponent<Animator>();
        player = playerWeapon.GetComponent<Player>();
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
        Aim();
        yield return new WaitForSecondsRealtime(weaponData.attackDelay);
    }

    void Aim()
    {
        
        playerWeapon.SetAimLocked(true);
        //player.SetCanMove(false);
        animator.SetTrigger("StartAttack");
        if (weaponAudioSource != null)
        {
            weaponAudioSource.Stop();
        }
        audioSource.PlayOneShot(attackSFX, weaponData.attackVolume*volumeMultiplier);

        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - playerWeapon.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        currentAimSymbol = Instantiate(aimSymbol, playerWeapon.transform.position, Quaternion.identity);
        Rigidbody2D rb = currentAimSymbol.GetComponent<Rigidbody2D>();
        rb.velocity = direction.normalized * weaponData.projectileSpeed;
    }

    public override IEnumerator CastAttack()
    {
        if (currentAimSymbol != null)
        {
            Vector3 aimPosition = currentAimSymbol.transform.position;
            castProjectile = Instantiate(projectile, aimPosition, Quaternion.identity);
            Destroy(currentAimSymbol);

            


            Projectile projectileScript = castProjectile.GetComponent<Projectile>();
            projectileScript.SetDamage(weaponData.damage);
            audioSource.PlayOneShot(impactSFX, weaponData.impactVolume * volumeMultiplier);
            Destroy(castProjectile, weaponData.projectileLife);
            yield return new WaitForSecondsRealtime(weaponData.projectileLife);
            //player.SetCanMove(true);
            if (weaponAudioSource != null)
            {
                weaponAudioSource.Play();
            }
            playerWeapon.SetAimLocked(false);
            playerWeapon.SetIsAttacking(false);
            playerWeapon.DecreaseDurability();
            if (animator != null)
            {
                animator.SetTrigger("FinishAttack");
            }
            yield return new WaitForSecondsRealtime(weaponData.attackDelay - weaponData.projectileLife);
            playerWeapon.SetCanAttack(true);
        }
        

        
        
    }

}
