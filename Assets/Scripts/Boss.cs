using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Boss : MonoBehaviour
{
    [Header("BossStates")]
    public Way wayLooking;
    public LimbType lastlyAddedLimb;
    public LimbType attackingLimb;
    public BossAction bossAction;
    public int actionNumber = 0;

    public List<Limb> allLimbs;

    [Header("BossProperties")]
    public int health = 100;
    public List<int> healthList;
    public float speed = 5f;

    private GameObject player;

    public GameObject curAttack;

    public AttackList attackList;

    [Header("UI")]
    public HealthBar bossHealthBar;
    public Animator Explosion;

    public AudioSource Explode;




    // Joint the limbs with each other

    public void Init()
    {
        allLimbs = new List<Limb>();
        player = FindObjectOfType<Player>().gameObject;
        attackList = FindObjectOfType<AttackList>();
        health = healthList[0];
        bossHealthBar.SetMaxHealth(health);
    }
    protected virtual void CreateLimbs()
    {
        
    }

    protected virtual void UpdateAnimations(LimbType lastlyAddedLimb, LimbType attackingLimb, Way wayLooking, BossAction action)
    {
        foreach(Limb limb in allLimbs)
        {
            LimbAnimationController.ControlAnimations(limb.animator, lastlyAddedLimb, attackingLimb, wayLooking, action);
        }
    }

    public virtual void TakeHit(int hp)
    {
        health -= hp;
        bossHealthBar.SetHealth(health);
        if (health <= 0)
        {
            CheckLostLimb();

            //Trigger fill animation here
            bossHealthBar.SetMaxHealth(GetMaxHealth());
        }
    }

    public virtual void CheckLostLimb()
    {

    }

    public virtual void Move(Vector3 destination)
    {
        Vector3 direction = destination - transform.position;
        transform.position = transform.position + direction.normalized * speed * Time.deltaTime;
    }

    public virtual void MoveDirection(Vector3 direction)
    {

        transform.position = transform.position + direction.normalized * speed * Time.deltaTime;
    }

    public void SelectAttack()
    {
        Limb tempLimb = allLimbs[Random.Range(0, allLimbs.Count)];

        curAttack = Instantiate(attackList.GetAttack(tempLimb.limbType, tempLimb.elementType, ref actionNumber));
        foreach(var generator in curAttack.GetComponents<GenerateAttack>())
        {
            generator.elementType = tempLimb.elementType;
        }
    }

    public void ChangeAttack()
    {
        Destroy(curAttack);
        SelectAttack();
    }

    public List<Limb> AllLimbs()
    {
        return allLimbs;
    }

    public int GetMaxHealth()
    {
        return healthList[allLimbs.Count - 2];
    }

    
}
