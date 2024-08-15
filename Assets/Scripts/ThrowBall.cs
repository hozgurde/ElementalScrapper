using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBall : MonoBehaviour
{

    private bool throwing;
    float time;
    Vector3 departure;
    Vector3 destination;
    float curTime;


    // Start is called before the first frame update
    void Awake()
    {
        curTime = 0f;
        throwing = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (throwing && curTime < time)
        {
            var yHeight = 1 - Mathf.Pow( (((time / 2) - curTime) / (time / 2)), 2);
            var expPos = (curTime / time) * destination + (1 - curTime / time) * departure;
            transform.position = new Vector3(expPos.x, expPos.y + yHeight, expPos.z);
            curTime += Time.deltaTime;
        }

        if(curTime >= time)
        {
            var anim = GetComponent<Animator>();
            anim.SetTrigger("Splash");
            Destroy(gameObject, anim.GetCurrentAnimatorStateInfo(0).length);
        }
    }

    public void Throw(float _time, Vector3 _departure, Vector3 _destination)
    {
        throwing = true;
        time = _time;
        departure = _departure;
        destination = _destination;
    }
}
