﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionAction : MonoBehaviour
{
    Animator mAnimator;
    private Vector2 previousLocation;
    private Rigidbody2D myRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        mAnimator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("PlantForm") || collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy"))
        {
            //Am.JumpAnimation(false);
            AnimationManager.instance.PlayAnimation(mAnimator, "Jump", false);
        }
    }

    public void Move(Transform target,float moveSpeed)
    {
        transform.position += (new Vector3(Mathf.RoundToInt(target.position.x), 0, 0) - new Vector3(Mathf.RoundToInt(transform.position.x), 0, 0)).normalized * moveSpeed * Time.deltaTime;
        //transform.Translate(new Vector3(target.position.x, 0, 0) * moveSpeed * Time.deltaTime);
        if (previousLocation.x - transform.position.x >= 0)
        {
           transform.localScale = new Vector3(-1, 1, 1);
           AnimationManager.instance.PlayAnimation(mAnimator, "Walk", true);

        }
        else if (previousLocation.x - transform.position.x <= 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            AnimationManager.instance.PlayAnimation(mAnimator, "Walk", true);
        }
    }

    public void Move(Vector3 direction)
    {
        transform.Translate(direction);
        if (previousLocation.x - transform.position.x >= 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            AnimationManager.instance.PlayAnimation(mAnimator, "Walk", true);

        }
        else if (previousLocation.x - transform.position.x <= 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            AnimationManager.instance.PlayAnimation(mAnimator, "Walk", true);
        }

    }
    public void Jump(float JumpForce)
    {
      if (!mAnimator.GetBool("Jump"))
        {
            myRigidbody.AddForce(new Vector3(0, JumpForce, 0));
            AnimationManager.instance.PlayAnimation(mAnimator, "Jump", true);
        }
      
    }

    public void SideShift(float force)
    {
        myRigidbody.AddForce(new Vector3(Random.Range(-2, 2) * force, 0, 0));

    }

    private void Update()
    {
        if (previousLocation.x == transform.position.x)
        {
            AnimationManager.instance.PlayAnimation(mAnimator, "Walk", false);
        }
        //print(previousLocation.x - transform.position.x);
        previousLocation = transform.position;

    }

}
