using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public float Speed;
    public float JumpForce;
    public GameObject firepoint;
    public GameObject foot;
    public GameObject laser;
    public float BulletSpeed;
    PlayerActions mPlayerActions;
    AnimationManager Am;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("PlantForm"))
        {
            mPlayerActions.IsJumping = false;
            Am.JumpAnimation(false);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        mPlayerActions = GetComponent<PlayerActions>();
        Am = GetComponent<AnimationManager>();
    }
    void Move()
    {
        mPlayerActions.MoveAction(Speed);
        if (mPlayerActions.IsMoving)
        {
            Am.WalkAnimation(true);
        }
        else
        {
            Am.WalkAnimation(false);
        }
        mPlayerActions.JumpAction(JumpForce);
        if(mPlayerActions.IsJumping)
        {
            Am.JumpAnimation(true);
        }
        else
        {
            Am.JumpAnimation(false);
        }
    }
    void Kick()
    {
        Am.KickAnimation(true);
        mPlayerActions.KickAction(foot);

    }
    // Update is called once per frame
    void Update()
    {
        Move();
        mPlayerActions.ShootAction(laser,firepoint,transform,BulletSpeed);
        Kick();
    }
}
