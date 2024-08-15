using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingControl : MonoBehaviour
{
    [Header("Icon Animators")]
    [SerializeField] Animator remnant1;
    [SerializeField] Animator remnant2;
    [SerializeField] Animator weapon;

    [Header("Slider Animators")]
    [SerializeField] Animator leftSlider1;
    [SerializeField] Animator leftSlider2;
    [SerializeField] Animator rightSlider1;
    [SerializeField] Animator rightSlider2;

    [Header("Slider Fills")]
    [SerializeField] Image leftFill;
    [SerializeField] Image rightFill;

    [Header("Element Colors for Slider Fills")]
    [SerializeField] Color[] colors;

    [Header("Weapon")]
    [SerializeField] Image weaponImage;



    

    float fillDelay;

    private void Start()
    {
        fillDelay = 0.0833f * 10;
    }

    // Fire:1, Water:2, Earth:3, Lightning:4, Air:5
    public void CreateElement1(int elementNo)
    {
        remnant1.SetInteger("CreateElement", elementNo);
    }

    public void CreateElement2(int elementNo)
    {
        remnant2.SetInteger("CreateElement", elementNo);
    }

    public void DestroyWeaponIcon()
    {
        weapon.SetTrigger("Destroy");
    }

    public IEnumerator TriggerCrafting(int element1, int element2, Sprite currentWeaponSprite)
    {
        leftFill.color = colors[element1-1];
        rightFill.color = colors[element2-1];
        remnant1.SetInteger("CreateElement", 0);
        remnant2.SetInteger("CreateElement", 0);
        remnant1.SetTrigger("Destroy");
        remnant2.SetTrigger("Destroy");
        leftSlider1.SetBool("Fill", true);
        rightSlider1.SetBool("Fill", true);
        yield return new WaitForSeconds(fillDelay);
        weaponImage.sprite = currentWeaponSprite;
        leftSlider2.SetBool("Fill", true);
        rightSlider2.SetBool("Fill", true);
        weapon.SetTrigger("Create");
        yield return new WaitForSeconds(fillDelay);
        leftSlider1.SetBool("Fill", false);
        rightSlider1.SetBool("Fill", false);
        leftSlider2.SetBool("Fill", false);
        rightSlider2.SetBool("Fill", false);

    }

}
