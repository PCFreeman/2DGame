using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionManager : MonoBehaviour
{
    public MinionAction mMinionAction;
    public Transform target;
    public float laserspeed = 3f;
    public GameObject BossFirepoint;
    public GameObject BossLaser;
    public int MaxDist = 0;
    public int MinDist = 0;
    public float AttackSpeed;
    public int moveSpeed;
    public AudioClip ShotsSound;
    AudioSource mAudioSource;
    public bool IsAttacking;
    public int MaxHealth;
    public int CurrentHealth;
    public Transform GroundDetection;

    void Start()
    {
        mMinionAction = GetComponent<MinionAction>();
        mAudioSource = GetComponent<AudioSource>();


    }

    void Shoot()
    {
        mAudioSource.clip = ShotsSound;
        mAudioSource.Play();
        GameObject bullet = Instantiate(BossLaser, BossFirepoint.transform.position, transform.rotation) as GameObject;
        bullet.GetComponent<Rigidbody2D>().velocity = Vector3.right * laserspeed;
        if ((int)target.position.x - (int)transform.position.x > 0)
        {
            Debug.Log("right");
            bullet.transform.localScale = new Vector3(1, 1, 1);
            bullet.GetComponent<Rigidbody2D>().velocity = Vector3.right * laserspeed;
            if ((int)target.position.y - (int)transform.position.y < 0)
            {
                Debug.Log("Rdown");
                bullet.transform.eulerAngles = new Vector3(0, 0, -90);
                bullet.GetComponent<Rigidbody2D>().velocity = Vector3.down * laserspeed;
            }
            else if ((int)target.position.y - (int)transform.position.y > 0)
            {
                Debug.Log("Rup");
                bullet.transform.eulerAngles = new Vector3(0, 0, 90);
                bullet.GetComponent<Rigidbody2D>().velocity = Vector3.up * laserspeed;
            }
        }
        else if ((int)target.position.x - (int)transform.position.x <= 0)
        {
            Debug.Log("left");
            bullet.transform.localScale = new Vector3(-1, 1, 1);
            bullet.GetComponent<Rigidbody2D>().velocity = Vector3.left * laserspeed;
            if ((int)target.position.y - (int)transform.position.y < 0)
            {
                Debug.Log("Ldown");
                bullet.transform.eulerAngles = new Vector3(0, 0, 90);
                bullet.GetComponent<Rigidbody2D>().velocity = Vector3.down * laserspeed;
            }
            else if ((int)target.position.y - (int)transform.position.y > 0)
            {
                Debug.Log("Lup");
                bullet.transform.eulerAngles = new Vector3(0, 0, -90);
                bullet.GetComponent<Rigidbody2D>().velocity = Vector3.up * laserspeed;
            }
        }

    }
    void Detect()
    {
        if (Vector3.Distance(transform.position, target.position) <= MaxDist && Vector3.Distance(transform.position, target.position) > MinDist)
        {
            mMinionAction.Move(target, moveSpeed);
            if (IsAttacking == false)
            {
                //InvokeRepeating("Shoot", 0, AttackSpeed);
                IsAttacking = true;
            }
        }
        if (Vector3.Distance(transform.position, target.position) > MaxDist)
        {
            IsAttacking = false;
            //CancelInvoke("Shoot");
        }
        RaycastHit2D groundinfo = Physics2D.Raycast(GroundDetection.position, Vector2.down, 2f);

        if (groundinfo.collider == null)
        {
            mMinionAction.Jump(300);
        }
    }
    public void Damage(int damage)
    {
        CurrentHealth -= damage;


        if (CurrentHealth <= 0)
        {
            Destroy(gameObject);
        }

    }
    // Update is called once per frame
    void Update()
    {

        //mMinionAction.ShootStartCoroutine();
         Detect();
       
    }
}
