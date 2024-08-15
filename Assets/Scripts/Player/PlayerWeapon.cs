using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerWeapon : MonoBehaviour
{

    [SerializeField] AudioSource playerAudioSource;
    [SerializeField] Transform weaponParent;

    [Header("UI")]
    [SerializeField] Cooldown attackIcon;

    [Header("SFX")]
    [SerializeField] AudioClip weaponDestroyedSFX;
    [SerializeField] float weaponDestroyedVolume;

    [Header("Weapon Dictionary")]
    [SerializeField] ElementType[] elementType1Array;
    [SerializeField] ElementType[] elementType2Array;
    [SerializeField] WeaponData[] weaponDataArray;

    

    Dictionary<String, WeaponData> weaponTypeDictionary = new Dictionary<String, WeaponData>();

    const string defaultKey = "NoneNone";

    public bool hasWeapon;

    WeaponData currentWeaponData;
    GameObject currentWeapon;
    WeaponAttack weaponAttack;
    Sprite currentWeaponSprite;

    bool canAttack = true;
    bool isAttacking = false;
    bool aimLocked = false;

    int durability;
    bool isHoldWeapon;

    
    Vector2 lookDirection;
    float lookAngle;
    Animator animator;
    Animator weaponParentAnimator;

    CraftingControl craftingControl;

    float volumeMultiplier;

    private void Start()
    {
        volumeMultiplier = playerAudioSource.volume;
        craftingControl = FindObjectOfType<CraftingControl>();
        PopulateWeaponTypeDictionary();
        //CreateWeapon(defaultKey);
        animator = GetComponent<Animator>();
        weaponParentAnimator = weaponParent.GetComponent<Animator>();
        hasWeapon = false;
    }


    public void CreateWeapon(string weaponKey)
    {
        if (!weaponTypeDictionary.ContainsKey(weaponKey))
        {
            weaponKey = defaultKey;
        }
        DestroyCurrentWeapon();

        currentWeaponData = weaponTypeDictionary[weaponKey];
        currentWeapon = currentWeaponData.weapon;
        currentWeaponSprite = currentWeapon.GetComponent<SpriteRenderer>().sprite;
        durability = currentWeaponData.durability;
        isHoldWeapon = currentWeaponData.isHoldWeapon;
        currentWeapon = Instantiate(currentWeapon, weaponParent);

        weaponParentAnimator.SetTrigger("Create");

        weaponAttack = currentWeapon.GetComponent<WeaponAttack>();
        weaponAttack.UpdateWeapon();
        canAttack = true;
        

        attackIcon.EnableIcon();
        attackIcon.SetCooldownTime(currentWeaponData.attackDelay);
        hasWeapon = true;

    }

    void DestroyCurrentWeapon()
    {
        if (currentWeapon != null)
        {
            Destroy(currentWeapon);
            playerAudioSource.PlayOneShot(weaponDestroyedSFX, weaponDestroyedVolume*volumeMultiplier);
            currentWeapon = null;
            weaponParentAnimator.SetTrigger("Destroy");
        }
        weaponAttack = null;
        canAttack = false;

        attackIcon.DisableIcon();
        hasWeapon = false;
    }


    void Update()
    {
        CalculateLookAngle();
        RotatePlayer();
        if (currentWeapon == null) { return; }
        AimMouse(currentWeapon);
    }
    void CalculateLookAngle()
    {
        lookDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
    }

    void RotatePlayer()
    {
        // LookDirection --> 0=Down, 1=Left, 2=Right, 3=Up
        if (-30 < lookAngle && lookAngle < 30) //Right
        {
            animator.SetInteger("LookDirection", 2);
        }
        else if (60 < lookAngle && lookAngle < 120) //Up
        {
            animator.SetInteger("LookDirection", 3);
        }
        else if (-120 < lookAngle && lookAngle < -60) //Down
        {
            animator.SetInteger("LookDirection", 0);
        }
        else if (lookAngle > 150 || lookAngle < -150) //Left
        {
            animator.SetInteger("LookDirection", 1);
        }
    }

    void AimMouse(GameObject weapon)
    {
        if (aimLocked) {return;}

        if (currentWeaponData.rotatable)
        {
            Quaternion rotation = Quaternion.AngleAxis(lookAngle - 90, Vector3.forward);
            weaponParent.transform.rotation = rotation;
        }
        else
        {
            weaponParent.transform.rotation = Quaternion.identity;
        }
        Vector2 position = lookDirection.normalized * currentWeaponData.holdRadius;
        weaponParent.transform.localPosition = position;
    }

    
    void OnAttack()
    {
        if (currentWeapon == null) { return; }

        if (!canAttack)
        {
            return;
        }

        canAttack = false;
        isAttacking = true;
        StartCoroutine(weaponAttack.Attack());
        if (isHoldWeapon) { return; }

        attackIcon.UseAbility();

    }

    void OnAttackRelease()
    {
        if (!isHoldWeapon || currentWeapon == null || !isAttacking) { return; }

        StartCoroutine(weaponAttack.CastAttack());
        attackIcon.UseAbility();
        Debug.Log("Attack released!");
    }

    public void SetCanAttack(bool state)
    {
        canAttack = state;
    }

    public void SetIsAttacking(bool state)
    {
        isAttacking = state;
    }


    bool PopulateWeaponTypeDictionary()
    {
        int type1Length = elementType1Array.Length;
        int type2Length = elementType1Array.Length;
        int weaponLength = weaponDataArray.Length;
        if (type1Length != weaponLength || type2Length != weaponLength)
        {
            return false;
        }
        else
        {
            for (int i = 0; i < type1Length; i++)
            {
                //Debug.Log(elementType1Array[i].ToString() + elementType2Array[i].ToString());
                String combination = elementType1Array[i].ToString() + elementType2Array[i].ToString();
                weaponTypeDictionary[combination] = weaponDataArray[i];
                //Debug.Log(weaponTypeDictionary[combination].name);
            }
            return true;
        }
        
        
    }

    

    public void SetAimLocked(bool state)
    {
        aimLocked = state;
    }

    public void DecreaseDurability()
    {
        durability--;
        if (durability <= 0)
        {
            craftingControl.DestroyWeaponIcon();
            DestroyCurrentWeapon();
        }
    }

    public AudioSource GetPlayerAudioSource()
    {
        return playerAudioSource;
    }

    public Sprite GetCurrentWeaponSprite()
    {
        return currentWeaponSprite;
    }
}
