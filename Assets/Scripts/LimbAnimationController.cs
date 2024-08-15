using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LimbAnimationController
{
    public static void ControlAnimations(Animator animator, LimbType lastlyAddedLimb, LimbType attackingLimb, Way wayLooking, BossAction action)
    {
        // Set the animator parameters based on the input variables
        animator.SetInteger("LastlyAddedLimb", (int)lastlyAddedLimb);
        animator.SetInteger("AttackingLimb", (int)attackingLimb);
        animator.SetInteger("WayLooking", (int)wayLooking);
        animator.SetInteger("BossAction", (int)action);
    }
}