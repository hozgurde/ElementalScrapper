using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Boss Attack", menuName = "Boss Attack")]

public class BossAttackData : ScriptableObject
{
    public int damage;
    public GameObject attack;

    [Header("Appearing Attack")]
    public float time;
    public float radius;

}
