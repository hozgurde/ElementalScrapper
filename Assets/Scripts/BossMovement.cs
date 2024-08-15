using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossMovement : MonoBehaviour
{

    protected Boss curBoss;
    public BossAction actionType;
    
    [Header("Move Direction")]
    public Transform curPosition;
    public Transform destination;

    [Header("Trigger")]
    public bool triggerReachDestinationOn = false;
    public float radius = 1f;

    [Header("Move Times")]
    public float minMoveTime = 2.5f;
    public float maxMoveTime = 5f;

    [Header("Wait")]
    public float minWaitTime = 2f;
    public float maxWaitTime = 3f;

    private float waitTime;
    private float moveTime;
    private bool isWaiting;

    private Way curWay;
    private BossAction curAction;
    private BossAnimationController bossAnimationController;

    private List<Limb> limbs;


    // Start is called before the first frame update
    void Start()
    {
        curBoss = FindObjectOfType<Boss>();
        limbs = curBoss.AllLimbs();
        destination = FindObjectOfType<Player>().transform;
        bossAnimationController = FindObjectOfType<BossAnimationController>();
        moveTime = Random.Range(minMoveTime, maxMoveTime);
        waitTime = Random.Range(minWaitTime, maxWaitTime);
        isWaiting = false;
        curWay = Way.Up;
        curAction = BossAction.Moving;
    }

    // Update is called once per frame
    void Update()
    {
        MoveBoss();
    }

    protected virtual void MoveBoss()
    {

        Vector3 distanceVector = destination.position - curPosition.position;
        float distance = distanceVector.magnitude;

        Vector3 upVector = Vector3.up;
        Quaternion rotation = Quaternion.FromToRotation(upVector, destination.position - transform.position);
        transform.rotation = rotation;



        if (moveTime > 0 && !isWaiting)
        {
            moveTime -= Time.deltaTime;

            if (distance <= radius)
            {
                if (triggerReachDestinationOn)
                {
                    

                    GenerateAttacks();
                    isWaiting = true;
                }
            }
            else
            {
                bool changeOpen = false;
                var unit = distanceVector.normalized;
                if(Mathf.Abs(unit.x) > Mathf.Abs(unit.y))
                {
                    if(unit.x > 0 && curWay != Way.Right)
                    {
                        curWay = Way.Right;
                        changeOpen = true;
                    }
                    else if(unit.x <= 0 && curWay != Way.Left)
                    {
                        curWay = Way.Left;
                        changeOpen = true;
                    }
                }
                else
                {
                    if (unit.y > 0 && curWay != Way.Up)
                    {
                        curWay = Way.Up;
                        changeOpen = true;
                    }
                    else if (unit.y <= 0 && curWay != Way.Down)
                    {
                        curWay = Way.Down;
                        changeOpen = true;
                    }
                }

                foreach (var limb in limbs)
                {
                    if(changeOpen || bossAnimationController.IsAnimationStopped(limb.limbType))
                    {
                        bossAnimationController.ChangeState(limb.limbType, limb.elementType, curWay, curAction);
                    }
                        
                }
                
                curBoss.MoveDirection(distanceVector);
                transform.position = curBoss.transform.position;
            }

            if(moveTime <= 0)
            {
                GenerateAttacks();
                isWaiting = true;
            }
        }
        if (isWaiting)
        {
            waitTime -= Time.deltaTime;
            if(waitTime < 0)
            {
                curBoss.ChangeAttack();
            }
        }


        

    }

    private void GenerateAttacks()
    {
        curAction = actionType;

        foreach (var limb in limbs)
        {
                bossAnimationController.ChangeState(limb.limbType, limb.elementType, curWay, curAction);

        }

        var generateAttacks = GetComponents<GenerateAttack>();

        foreach (var generator in generateAttacks)
        {
            generator.TriggerGeneration();
        }
    }
}
