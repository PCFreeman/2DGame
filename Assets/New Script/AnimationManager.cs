using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public Animator myAnimator;
    bool Trigger;

    public void Awake()
    {
        myAnimator = GetComponent<Animator>();
    }
    public void WalkAnimation(bool Trigger)
    {
        myAnimator.SetBool("IsWalk", Trigger);
    }
    public void JumpAnimation(bool Trigger)
    {
        myAnimator.SetBool("IsJump", Trigger);
    }
    public void KickAnimation(bool Trigger)
    {
        myAnimator.SetBool("kick", Trigger);
    }
}
