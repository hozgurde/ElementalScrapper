using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
public class WeaponData : ScriptableObject
{
    public new string name;
    public ElementType[] type = new ElementType[2];
    public int damage;
    public int durability;
    public GameObject weapon;
    public float animationDelay;
    public float attackDelay;
    public float holdRadius;
    public bool isHoldWeapon;
    public bool rotatable;

    [Header("Ranged-Hold")]
    public float projectileSpeed;
    public GameObject projectile;
    public float projectileLife;

    [Header("SFX")]
    public AudioClip attackSFX;
    [Range(0,1)] public float attackVolume = 1f;
    public AudioClip impactSFX;
    [Range(0, 1)] public float impactVolume = 1f;

    [Header("UI")]
    Sprite weaponIcon;




}
