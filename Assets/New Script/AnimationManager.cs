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
    public void Walk(bool Trigger)
    {
        myAnimator.SetBool("IsWalk", Trigger);
    }
    public void Jump(bool Trigger)
    {
        myAnimator.SetBool("IsJump", Trigger);
    }
}
