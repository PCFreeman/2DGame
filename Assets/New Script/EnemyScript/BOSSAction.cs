using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOSSAction : MonoBehaviour
{
    private Rigidbody2D myRigidbody;
    Animator mAnimator;
    AudioSource mAudioSource;
    public bool CanFire = true;
    public bool CanKick = true;
    public bool CanThrow = true;
    private Vector2 previousLocation;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("PlantForm") || collision.gameObject.CompareTag("Enemy"))
        {
            //Am.JumpAnimation(false);
            AnimationManager.instance.PlayAnimation(mAnimator, "Jump", false);
        }
    }

    public void Awake()
    {

    }
    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        mAnimator = GetComponent<Animator>();
        mAudioSource = GetComponent<AudioSource>();
    }
    public void Move(Transform target, float moveSpeed)
    {
        transform.position += (new Vector3(Mathf.RoundToInt(target.position.x), 0, 0) - new Vector3(Mathf.RoundToInt(transform.position.x), 0, 0)).normalized * moveSpeed * Time.deltaTime;
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

    public void Kick(GameObject footpoint)
    {
        // Am.KickAnimation(true);
        if (CanKick)
        {
            CanKick = false;
            AnimationManager.instance.PlayAnimation(mAnimator, "Kick", true);
            footpoint.GetComponent<BoxCollider2D>().enabled = true;

        }
        //  Am.KickAnimation(false);
        AnimationManager.instance.PlayAnimation(mAnimator, "Kick", false);

    }
    public void ThrowAction(GameObject Explosive,GameObject FirePoint,Transform Trans,float ThrowPower)
    {

       GameObject bullet = Instantiate(Explosive, FirePoint.transform.position, Trans.rotation) as GameObject;
       if (transform.localScale.x > 0)
       {
           bullet.GetComponent<Rigidbody2D>().AddForce(new Vector3(1, 1, 0) * ThrowPower);
       }
       else if (transform.localScale.x < 0)
       {
           bullet.transform.localScale = new Vector3(-1, 1, 1);
           bullet.GetComponent<Rigidbody2D>().AddForce(new Vector3(-1, 1, 0) * ThrowPower);
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
