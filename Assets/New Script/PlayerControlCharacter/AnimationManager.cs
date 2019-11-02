using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
   //public Animator PlayerAnimator;

    public static AnimationManager instance = null;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void PlayAnimation(Animator an, string name,bool condition)
    {
        an.SetBool(name, condition);
    }




}
