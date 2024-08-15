using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int maxHealth = 100;
    [SerializeField] int currentHealth = 100;

    [SerializeField] float invincibilityTime = 1f;

    [SerializeField] HealthBar healthBar;

    [SerializeField] AudioClip hitSFX;
    [SerializeField] float hitVolume;

    SpriteRenderer spriteRenderer;
    AudioSource audioSource;
    bool isInvincible = false;

    float volumeMultiplier;
    

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        volumeMultiplier = audioSource.volume;
        healthBar.SetMaxHealth(maxHealth);
    }

    void ProcessDeath()
    {
        if (currentHealth <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            //Destroy(gameObject);
        }
    }

    void UpdateHealthBar()
    {
        healthBar.SetHealth(currentHealth);
    }


    IEnumerator FlashAfterDamage()
    {
        isInvincible = true;
        float flashDelay = 0.0833f;
        float timePassed = 0f;
        while (timePassed < invincibilityTime) 
        { 
            spriteRenderer.enabled= false;
            yield return new WaitForSeconds(flashDelay);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(flashDelay);
            timePassed += 2 * flashDelay;
        }
        isInvincible = false;
    }


    public void TakeDamage(int amount)
    {
        if (isInvincible) { return; }
        currentHealth -= amount;
        audioSource.PlayOneShot(hitSFX, hitVolume * volumeMultiplier);
        UpdateHealthBar();
        ProcessDeath();
        StartCoroutine(FlashAfterDamage());
    }

    public void SetInvincible(bool state)
    {
        isInvincible = state;
    }
    public int GetHealth()
    {
        return currentHealth;
    }

    public void FullyHeal()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }
}
