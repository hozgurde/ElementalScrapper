
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HumanoidBoss : Boss
{
    
    public Limb head, torso, legs, rightArm, leftArm;
    public SpriteRenderer headSprite, torsoSprite, legsSprite, rightArmSprite, leftArmSprite;
    public SimpleFlash[] limbFlashes;
    ArrayList elementTypes;
    //Will Be Changed
    string spritePath = "BossLimbs/HumanoidBoss/Limb";
    string animationPath = "AnimationControllers/HumanoidBoss/Limb";

    // Start is called before the first frame update
    public void Start()
    {
        Init();

        elementTypes = new ArrayList((ElementType[])System.Enum.GetValues(typeof(ElementType)));
        elementTypes.RemoveAt(elementTypes.IndexOf(ElementType.None));
        AddHead();
        AddTorso();
        SelectAttack();
    }

    private void AddTorso()
    {
        torso.elementType = (ElementType)elementTypes[Random.Range(0, elementTypes.Count)];
        elementTypes.RemoveAt(elementTypes.IndexOf(torso.elementType));
        torso.limbType = LimbType.Torso;
        torsoSprite.enabled = true;
        //torso.gameObject = new GameObject("Torso");
        //torso.gameObject.transform.position = transform.position;
        //torso.gameObject.transform.SetParent(transform);

        //torso.gameObject.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(spritePath + "_L" + (int)torso.limbType + "_E" + (int)torso.elementType);
        //torso.animator = torso.gameObject.AddComponent<Animator>();
        //torso.animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(animationPath + "_L" + (int)torso.limbType + "_E" + (int)torso.elementType);

        allLimbs.Add(torso);
        lastlyAddedLimb = LimbType.Torso;
    }

    private void AddHead()
    {
        head.elementType = (ElementType)elementTypes[Random.Range(0, elementTypes.Count)];
        elementTypes.RemoveAt(elementTypes.IndexOf(head.elementType));
        head.limbType = LimbType.Head;

        headSprite.enabled = true;
        //head.gameObject = new GameObject("Head");
        //head.gameObject.transform.position = transform.position;
        //head.gameObject.transform.SetParent(transform);
        
        //head.gameObject.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(spritePath + "_L" + (int)head.limbType + "_E" + (int)head.elementType);
        //head.animator = head.gameObject.AddComponent<Animator>();
        //head.animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(animationPath + "_L" + (int)head.limbType + "_E" + (int)head.elementType);

        allLimbs.Add(head);
        lastlyAddedLimb = LimbType.Head;
    }

    private void AddLegs()
    {
        legs.elementType = (ElementType)elementTypes[Random.Range(0, elementTypes.Count)];
        elementTypes.RemoveAt(elementTypes.IndexOf(legs.elementType));
        legs.limbType = LimbType.Legs;

        legsSprite.enabled = true;
        //legs.gameObject = new GameObject("Legs");
        //legs.gameObject.transform.position = transform.position;
        //legs.gameObject.transform.SetParent(transform);

        //legs.gameObject.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(spritePath + "_L" + (int)legs.limbType + "_E" + (int)legs.elementType);
        //legs.animator = legs.gameObject.AddComponent<Animator>();
       //legs.animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(animationPath + "_L" + (int)legs.limbType + "_E" + (int)legs.elementType);

        allLimbs.Add(legs);
        lastlyAddedLimb = LimbType.Legs;
    }

    private void AddRightArm()
    {
        rightArm.elementType = (ElementType)elementTypes[Random.Range(0, elementTypes.Count)];
        elementTypes.RemoveAt(elementTypes.IndexOf(rightArm.elementType));
        rightArm.limbType = LimbType.RightArm;

        rightArmSprite.enabled = true;
        //rightArm.gameObject = new GameObject("RightArm");
        //rightArm.gameObject.transform.position = transform.position;
        //rightArm.gameObject.transform.SetParent(transform);

        //rightArm.gameObject.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(spritePath + "_L" + (int)rightArm.limbType + "_E" + (int)rightArm.elementType);
        //rightArm.animator = rightArm.gameObject.AddComponent<Animator>();
        //rightArm.animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(animationPath + "_L" + (int)rightArm.limbType + "_E" + (int)rightArm.elementType);

        allLimbs.Add(rightArm);
        lastlyAddedLimb = LimbType.RightArm;
    }

    private void AddLeftArm()
    {
        leftArm.elementType = (ElementType)elementTypes[Random.Range(0, elementTypes.Count)];
        elementTypes.RemoveAt(elementTypes.IndexOf(leftArm.elementType));
        leftArm.limbType = LimbType.LeftArm;

        leftArmSprite.enabled = true;
        //leftArm.gameObject = new GameObject("LeftArm");
        //leftArm.gameObject.transform.position = transform.position;
        //leftArm.gameObject.transform.SetParent(transform);

        //leftArm.gameObject.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(spritePath + "_L" + (int)leftArm.limbType + "_E" + (int)leftArm.elementType);
        //leftArm.animator = leftArm.gameObject.AddComponent<Animator>();
        //leftArm.animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(animationPath + "_L" + (int)leftArm.limbType + "_E" + (int)leftArm.elementType);

        allLimbs.Add(leftArm);
        lastlyAddedLimb = LimbType.LeftArm;
    }

    public override void CheckLostLimb()
    {
        Explosion.SetTrigger("Explode");
        Explode.Play();

        if(elementTypes.Count > 0)
        {
            if(lastlyAddedLimb == LimbType.Torso)
            {
                AddLegs();
                health = healthList[1];

            }else if(lastlyAddedLimb == LimbType.Legs)
            {
                AddRightArm();
                health = healthList[2];
            }
            else if(lastlyAddedLimb == LimbType.RightArm)
            {
                AddLeftArm();
                health = healthList[3];
            }
        }
        else
        {
            //Death Script Here
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public override void TakeHit(int hp)
    {
        Debug.Log("Takehit!");
        health -= hp;
        bossHealthBar.SetHealth(health);
        //Trigger flashing here
        foreach ( SimpleFlash limbFlash in limbFlashes)
        {
            limbFlash.Flash();
        }

        if (health <= 0)
        {
            CheckLostLimb();

            //Trigger fill animation here
            bossHealthBar.SetMaxHealth(GetMaxHealth());

            

        }
    }
}
