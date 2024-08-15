using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimationController : MonoBehaviour
{

    public List<Animator> BossLimbAnimators;
    public List<SpriteRenderer> BossLimbSpriteRenderers;

    private void Awake()
    {
    }

    public void ChangeState(LimbType limbType, ElementType elementType, Way way, BossAction bossAction)
    {
        var curSpriteRenderer = BossLimbSpriteRenderers[(int)limbType];

        switch (limbType)
        {
            case LimbType.Head:
                switch (way)
                {
                    case Way.Up:
                        curSpriteRenderer.sortingOrder = 6;
                        break;
                    case Way.Right:
                        curSpriteRenderer.sortingOrder = 6;
                        break;
                    case Way.Down:
                        curSpriteRenderer.sortingOrder = 3;
                        break;
                    case Way.Left:
                        curSpriteRenderer.sortingOrder = 6;
                        break;
                }
                break;
            case LimbType.Torso:
                switch (way)
                {
                    case Way.Up:
                        curSpriteRenderer.sortingOrder = 5;
                        break;
                    case Way.Right:
                        curSpriteRenderer.sortingOrder = 3;
                        break;
                    case Way.Down:
                        curSpriteRenderer.sortingOrder = 2;
                        break;
                    case Way.Left:
                        curSpriteRenderer.sortingOrder = 3;
                        break;
                }
                break;
            case LimbType.Legs:
                switch (way)
                {
                    case Way.Up:
                        curSpriteRenderer.sortingOrder = 4;
                        break;
                    case Way.Right:
                        curSpriteRenderer.sortingOrder = 2;
                        break;
                    case Way.Down:
                        curSpriteRenderer.sortingOrder = 1;
                        break;
                    case Way.Left:
                        curSpriteRenderer.sortingOrder = 2;
                        break;
                }
                break;
            case LimbType.RightArm:
                switch (way)
                {
                    case Way.Up:
                        curSpriteRenderer.sortingOrder = 3;
                        break;
                    case Way.Right:
                        curSpriteRenderer.sortingOrder = 5;
                        break;
                    case Way.Down:
                        curSpriteRenderer.sortingOrder = 6;
                        break;
                    case Way.Left:
                        curSpriteRenderer.sortingOrder = 1;
                        break;
                }
                break;
            case LimbType.LeftArm:
                switch (way)
                {
                    case Way.Up:
                        curSpriteRenderer.sortingOrder = 2;
                        break;
                    case Way.Right:
                        curSpriteRenderer.sortingOrder = 1;
                        break;
                    case Way.Down:
                        curSpriteRenderer.sortingOrder = 5;
                        break;
                    case Way.Left:
                        curSpriteRenderer.sortingOrder = 5;
                        break;
                }
                break;
        }


   



        Animator curAnimator;
        curAnimator = BossLimbAnimators[(int)limbType];

        string limbName = "";

        switch (limbType)
        {
            case LimbType.Head:
                limbName = "Head";
                break;
            case LimbType.Torso:
                limbName = "Torso";
                break;
            case LimbType.Legs:
                limbName = "Legs";
                break;
            case LimbType.RightArm:
                limbName = "RightArm";
                break;
            case LimbType.LeftArm:
                limbName = "LeftArm";
                break;
        }

        string elementName = "";

        switch (elementType)
        {
            case ElementType.Fire:
                elementName = "Fire";
                break;
            case ElementType.Earth:
                elementName = "Earth";
                break;
            case ElementType.Water:
                elementName = "Water";
                break;
            case ElementType.Air:
                elementName = "Air";
                break;
            case ElementType.Lightning:
                elementName = "Lightning";
                break;
            default:
                break;
        }

        string wayName = "";

        switch (way)
        {
            case Way.Up:
                wayName = "Back";
                break;
            case Way.Right:
                wayName = "Right";
                break;
            case Way.Down:
                wayName = "Front";
                break;
            case Way.Left:
                wayName = "Left";
                break;
        }

        string bossActionName = "Walk";

        switch (bossAction)
        {
            case BossAction.Moving:
                bossActionName = "Walk";
                break;
            case BossAction.HeadAttack:
                if(limbType == LimbType.Head)
                {
                    bossActionName = "Attack";
                }
                break;
            case BossAction.TorsoAttack:
                if(limbType == LimbType.Torso)
                {
                    bossActionName = "Attack";
                }
                else
                {
                    bossActionName = "TorsoAttack";
                }
                break;
            case BossAction.LegsAttack:
                if (limbType == LimbType.Legs)
                {
                    bossActionName = "Attack";
                }
                break;
            case BossAction.RightArmAttack:
                if (limbType == LimbType.RightArm)
                {
                    bossActionName = "Attack";
                }
                break;
            case BossAction.LeftArmAttack:
                if (limbType == LimbType.LeftArm)
                {
                    bossActionName = "Attack";
                }
                break;
        }

        curAnimator.speed = 0.1f;
        print(elementName + "_" + limbName + "_" + wayName + "_" + bossActionName);
        curAnimator.Play(elementName + "_" + limbName + "_" + wayName + "_" + bossActionName);
    }

    public bool IsAnimationStopped(LimbType limbType)
    {
        return BossLimbAnimators[(int)limbType].GetCurrentAnimatorStateInfo(0).normalizedTime >= 1;
    }
}
