using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackGenerator : MonoBehaviour
{

    public Transform destination;
    public GameObject attack;


    //Test
    public bool generateAttack = false;


    private void Update()
    {
        if (generateAttack)
        {
            GenerateAttack();
            generateAttack = false;
        }

    }

    public void GenerateAttack()
    {
        var attackObject = Instantiate(attack);
        attackObject.transform.position = transform.position;
        attackObject.GetComponent<Attack>().destination = destination;
    }

}
