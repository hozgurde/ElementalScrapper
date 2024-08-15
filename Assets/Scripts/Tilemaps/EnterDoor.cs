using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterDoor : MonoBehaviour
{
    [SerializeField] Transform playerAppearPoint;
    [SerializeField] Transform playerTransform;
    [SerializeField] GameObject boss;
    [SerializeField] GameObject tutorialCanvas;
    [SerializeField] GameObject bossHealthBar;
    [SerializeField] float transitionTime = .1f;
    Camera camera;

    bool hasPassed = false;


    void Start()
    {
        camera = Camera.main;
        StartCoroutine(CheckIfPassed());
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(transitionCamera());
            playerTransform.position = playerAppearPoint.position;
            hasPassed = true;
            tutorialCanvas.SetActive(false);
            
        }
    }

    IEnumerator transitionCamera()
    {
        float transitionFrame = 0.01f;
        float timePassed = 0f;
        float fractionOfJourney = 0f;
        Vector3 startPosition = camera.transform.position;
        Vector3 endPosition = new Vector3(0, 0, -10);
        while (timePassed < transitionTime)
        {
            fractionOfJourney = timePassed / transitionTime;
            camera.transform.position = Vector3.Lerp(startPosition, endPosition, fractionOfJourney);
            yield return new WaitForSecondsRealtime(transitionFrame);
            timePassed += transitionFrame;
        }
        camera.transform.position = endPosition;

        bossHealthBar.SetActive(true);
        boss.SetActive(true);
    }

    IEnumerator CheckIfPassed()
    {
        yield return new WaitForSecondsRealtime(10);
        if (!playerTransform.GetComponent<PlayerWeapon>().hasWeapon)
        {
            ReloadScene();
        }
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
