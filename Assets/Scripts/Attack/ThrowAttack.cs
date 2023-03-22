using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowAttack : Attack
{
    //public Transform destination;
    public float duration;

    private float dx;
    private float dz;

    private float vy;

    private float g;

    private float curTime;



    private void Start()
    {
        dx = (destination.position.x - transform.position.x) / duration;
        dz = (destination.position.z - transform.position.z) / duration;

        g = Physics.gravity.y;

        vy = (destination.position.y - transform.position.y) - (g * duration / 2);

        curTime = 0;

        print(g);
    }

    // Update is called once per frame
    void Update()
    {
        if(curTime < duration)
        {
            curTime += Time.deltaTime;

            float newvy = vy + Time.deltaTime * g;

            float dy = ((vy + newvy) / 2) * Time.deltaTime;

            vy = newvy;

            transform.position += new Vector3(dx * Time.deltaTime, dy, dz* Time.deltaTime);
            

        }
    }
}
