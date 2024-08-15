using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Remnant : MonoBehaviour
{
    [SerializeField] ElementType elementType;

    
    static float lifeTime = 9f;
    static float slowFlashTime = 3f;
    static float fastFlashTime = 1.5f;
    static float flashAlpha = .5f;

    SpriteRenderer spriteRenderer;

    PlayerPickup playerPickup;
    RemnantGenerator remnantGenerator;

    Color flashColor;

    void Start()
    {
        playerPickup = FindObjectOfType<PlayerPickup>();
        remnantGenerator = FindObjectOfType<RemnantGenerator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        flashColor = new Color(1, 1, 1, flashAlpha);
        StartCoroutine(Flash());
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerPickup.PickupRemnant(elementType);
            if (!playerPickup.pickUpSuccessful) { return; }
            Destroy(gameObject);
            //StartCoroutine(playerPickup.CreateWeapon());
            remnantGenerator.RemoveRemnant(elementType);
        }
    }

    IEnumerator Flash()
    {
        float waitTime = Mathf.Max(lifeTime-slowFlashTime-fastFlashTime, 0);
        yield return new WaitForSecondsRealtime(waitTime);
        float flashDelay = 0.0833f * 3;
        float timePassed = 0f;
        while (timePassed < slowFlashTime)
        {
            spriteRenderer.color = flashColor;
            yield return new WaitForSecondsRealtime(flashDelay);
            spriteRenderer.color = Color.white;
            yield return new WaitForSecondsRealtime(flashDelay);
            timePassed += 2 * flashDelay;
        }
        flashDelay = 0.0833f;
        timePassed = 0f;
        while (timePassed < slowFlashTime)
        {
            spriteRenderer.color = flashColor;
            yield return new WaitForSecondsRealtime(flashDelay);
            spriteRenderer.color = Color.white;
            yield return new WaitForSecondsRealtime(flashDelay);
            timePassed += 2 * flashDelay;
        }
        Destroy(gameObject);
        remnantGenerator.RemoveRemnant(elementType);
    }
    public ElementType GetElementType()
    {
        return elementType;
    }

}
