using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cooldown : MonoBehaviour
{
    [SerializeField] Image cooldownImage;

    float cooldownTime;

    float cooldownTimer;
    bool onCooldown;

    private void Start()
    {
        cooldownImage.fillAmount = 0f;
    }

    private void Update()
    {
        if (onCooldown)
        {
            ApplyCooldown();
        }
    }

    void ApplyCooldown()
    {
        cooldownTimer -= Time.deltaTime;

        if (cooldownTimer < 0f) 
        {
            onCooldown = false;
            cooldownImage.fillAmount = 0f;
        }
        else
        {
            cooldownImage.fillAmount = cooldownTimer / cooldownTime;
        }
    }
    public bool UseAbility()
    {
        if (onCooldown)
        {
            return false;
        }
        else
        {
            onCooldown = true;
            cooldownImage.fillAmount = 0f;
            cooldownTimer = cooldownTime;
            return true;
        }
    }

    public void SetCooldownTime(float time)
    {
        cooldownTime = time;
    }

    public void DisableIcon()
    {
        cooldownImage.fillAmount = 1f;
    }

    public void EnableIcon()
    {
        cooldownImage.fillAmount = 0f;
    }
}
