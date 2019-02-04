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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
