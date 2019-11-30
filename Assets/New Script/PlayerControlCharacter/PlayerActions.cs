using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    private Rigidbody2D myRigidbody;
    Animator mAnimator;
    AudioSource mAudioSource;
    public AudioClip ShootSound;
    public AudioClip jump;

    public bool CanFire = true;
    public bool CanKick = true;
    public bool CanThrow = true;
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
        myRigidbody = GetComponent<Rigidbody2D>();
        mAnimator = GetComponent<Animator>();
        mAudioSource = GetComponent<AudioSource>();
    }
    public void MoveAction(float speed)
    {
        Vector3 direction = Vector3.zero;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.localScale = new Vector3(-1, 1, 1);
            // Am.WalkAnimation(true);
            AnimationManager.instance.PlayAnimation(mAnimator, "Walk", true);
            direction.x = -1;
        }
        else if(Input.GetKey(KeyCode.RightArrow))
        {
            transform.localScale = new Vector3(1, 1, 1);
            // Am.WalkAnimation(true);
            AnimationManager.instance.PlayAnimation(mAnimator, "Walk", true);
            direction.x = 1;
        }
        else
        {
            //Am.WalkAnimation(false);
            AnimationManager.instance.PlayAnimation(mAnimator, "Walk", false);
        }

        transform.position += direction * Time.deltaTime * speed;

        if (Input.GetKey(KeyCode.DownArrow) )
        {
            AnimationManager.instance.PlayAnimation(mAnimator, "LookingDown", true);
        }
        else
        {
            AnimationManager.instance.PlayAnimation(mAnimator, "LookingDown", false);
        }
    }

    public void JumpAction(float JumpForce)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!mAnimator.GetBool("Jump"))
            {
                myRigidbody.AddForce(new Vector3(0, JumpForce, 0));
                //Am.JumpAnimation(true);
                AnimationManager.instance.PlayAnimation(mAnimator, "Jump", true);
                SoundManager.instance.PlaySingle(jump);
            }
        }
    }

    public void KickAction(GameObject footpoint)
    {
        if (Input.GetKeyDown(KeyCode.X) )
        {
            // Am.KickAnimation(true);
            if(CanKick)
            {
                CanKick = false;
                AnimationManager.instance.PlayAnimation(mAnimator, "Kick", true);
                footpoint.GetComponent<BoxCollider2D>().enabled = true;

            }
        }
        else
        {
            //  Am.KickAnimation(false);
            AnimationManager.instance.PlayAnimation(mAnimator, "Kick", false);
        }
    }

    public void ShootAction(GameObject Bullet, GameObject WeaponPosition, Transform Characterrotation,float bulletSpeed)
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if(CanFire)
            {
                CanFire = false;
                GameObject bullet = Instantiate(Bullet, WeaponPosition.transform.position, Characterrotation.rotation) as GameObject;
                if (transform.localScale.x > 0)
                {
                    bullet.GetComponent<Rigidbody2D>().velocity = Vector3.right * bulletSpeed;
                    if (Input.GetKey(KeyCode.DownArrow) && Input.GetKeyDown(KeyCode.Z))
                    {
                        bullet.transform.eulerAngles = new Vector3(0, 0, -90);
                        bullet.GetComponent<Rigidbody2D>().velocity = Vector3.down * bulletSpeed;
                    }
                    if (Input.GetKey(KeyCode.UpArrow) && Input.GetKeyDown(KeyCode.Z))
                    {
                        bullet.transform.eulerAngles = new Vector3(0, 0, 90);
                        bullet.GetComponent<Rigidbody2D>().velocity = Vector3.up * bulletSpeed;
                    }

                }
                else if (transform.localScale.x < 0)
                {
                    bullet.transform.localScale = new Vector3(-1, 1, 1);
                    bullet.GetComponent<Rigidbody2D>().velocity = Vector3.left * bulletSpeed;
                    if (Input.GetKey(KeyCode.DownArrow) && Input.GetKeyDown(KeyCode.Z))
                    {
                        bullet.transform.eulerAngles = new Vector3(0, 0, 90);
                        bullet.GetComponent<Rigidbody2D>().velocity = Vector3.down * bulletSpeed;
                    }
                    if (Input.GetKey(KeyCode.UpArrow) && Input.GetKeyDown(KeyCode.Z))
                    {
                        bullet.transform.eulerAngles = new Vector3(0, 0, -90);
                        bullet.GetComponent<Rigidbody2D>().velocity = Vector3.up * bulletSpeed;
                    }
                }
                mAudioSource.clip = ShootSound;
                mAudioSource.Play();
            }
        
        }
    }

    public void ThrowAction(GameObject Explosive, GameObject WeaponPosition, Transform Characterrotation, float ThrowPower)
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            if(CanThrow)
            {
                CanThrow = false;
                GameObject bullet = Instantiate(Explosive, WeaponPosition.transform.position, Characterrotation.rotation) as GameObject;
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

        }
    }
    // Update is called once per frame
}
