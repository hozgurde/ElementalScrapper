using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Security;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Move")]
    [SerializeField] float moveSpeed = 6f;

    [Header("Dash")]
    [SerializeField] float dashMultiplier = 2f;
    [SerializeField] float dashTime = 1f;
    [SerializeField] float dashDelay = 2f;
    [SerializeField, Range(0f, 1f)] float dashTransparency = .5f;

    [Header("UI")]
    [SerializeField] Cooldown dashIcon;

    [Header("SFX")]
    [SerializeField] AudioClip dashSFX;
    [SerializeField] float dashVolume;

    bool isDashing = false;
    bool canDash = true;

    bool canMove = true;

    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    PlayerHealth playerHealth;
    Animator animator;
    AudioSource audioSource;
    float volumeMultiplier;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        playerHealth = GetComponent<PlayerHealth>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        volumeMultiplier = audioSource.volume;
        dashIcon.SetCooldownTime(dashDelay);
        dashIcon.EnableIcon();
    }

    void Update()
    {
        AnimateIdleWalk();
    }

    void AnimateIdleWalk()
    {
        bool isWalking = rb.velocity.magnitude > Mathf.Epsilon;
        animator.SetBool("IsWalking", isWalking);
    }

    void OnMove(InputValue value)
    {
        if (isDashing || !canMove) { return; }

        rb.velocity = value.Get<Vector2>() * moveSpeed;
    }

    void OnDash(InputValue value)
    {
        if (!canDash) { return; }
        StartCoroutine(Dash());
        audioSource.PlayOneShot(dashSFX, dashVolume*volumeMultiplier);
        dashIcon.UseAbility();
    }

    IEnumerator Dash()
    {
        Vector2 startVelocity = rb.velocity;
        isDashing = true;
        canDash = false;
        rb.velocity *= dashMultiplier;
        UnityEngine.Color spriteColor = spriteRenderer.color;
        ChangeTransparency(spriteColor, dashTransparency);

        playerHealth.SetInvincible(true);

        yield return new WaitForSeconds(dashTime);

        playerHealth.SetInvincible(false);

        ChangeTransparency(spriteColor, 1);

        rb.velocity = startVelocity;
        isDashing = false;
        yield return new WaitForSeconds(dashDelay);
        canDash = true;

    }
    
    private void ChangeTransparency(UnityEngine.Color color,float alpha)
    {
        color.a = alpha;
        spriteRenderer.color = color;
    }

    public void SetCanMove(bool state)
    {
        rb.velocity = Vector2.zero;
        canMove = state;
    }
}
