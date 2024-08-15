using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickup : MonoBehaviour
{

    [Header("Picked Up Remnants")]
    [SerializeField] ElementType slot1 = ElementType.None;
    [SerializeField] ElementType slot2 = ElementType.None;
    public bool pickUpSuccessful;


    [Header("SFX")]
    [SerializeField] AudioClip pickupSFX;
    [SerializeField] float pickupVolume;
    [SerializeField] AudioClip createWeaponSFX;
    [SerializeField] float createWeaponVolume;





    PlayerWeapon playerWeapon;
    CraftingControl craftingControl;
    AudioSource audioSource;
    float volumeMultiplier;

    static readonly Dictionary<ElementType, int> elementNos = new Dictionary<ElementType, int>() { { ElementType.Fire, 1 }, { ElementType.Water, 2 }, { ElementType.Earth, 3 }, { ElementType.Lightning, 4 }, { ElementType.Air,5 } };
    void Start()
    {
        playerWeapon = GetComponent<PlayerWeapon>();
        audioSource = GetComponent<AudioSource>();
        volumeMultiplier = audioSource.volume;
        craftingControl = FindObjectOfType<CraftingControl>();
    }

    public void PickupRemnant(ElementType type)
    {
        if (playerWeapon.hasWeapon)
        {
            pickUpSuccessful = false;
            return;
        }
        if (slot1 == ElementType.None)
        {
            slot1 = type;
            craftingControl.CreateElement1(elementNos[slot1]);
            pickUpSuccessful = true;
            audioSource.PlayOneShot(pickupSFX, pickupVolume * volumeMultiplier);
        }
        else if (slot2 == ElementType.None)
        {
            if (type == slot1)
            {
                Debug.Log("Already have this element");
                pickUpSuccessful = false;
            }
            else
            {
                slot2 = type;
                craftingControl.CreateElement2(elementNos[slot2]);
                StartCoroutine(CreateWeapon());
                pickUpSuccessful = true;
                audioSource.PlayOneShot(pickupSFX, pickupVolume * volumeMultiplier);
            }
            
        }
    }

    public IEnumerator CreateWeapon()
    {
        if (slot1 == ElementType.None || slot2 == ElementType.None)
        {
            Debug.Log("Not enough materials");
            yield return null;
        }
        else
        {
            yield return new WaitForEndOfFrame();
            Debug.Log("Weapon created from " + slot1.ToString() + " and " + slot2.ToString());
            string weaponKey = slot1.ToString() + slot2.ToString();
            playerWeapon.CreateWeapon(weaponKey);
            Sprite currentWeaponSprite = playerWeapon.GetCurrentWeaponSprite();
            StartCoroutine(craftingControl.TriggerCrafting(elementNos[slot1], elementNos[slot2], currentWeaponSprite));
            audioSource.PlayOneShot(createWeaponSFX, createWeaponVolume * volumeMultiplier);
            slot1 = ElementType.None;
            slot2 = ElementType.None;
        }
    }
}
