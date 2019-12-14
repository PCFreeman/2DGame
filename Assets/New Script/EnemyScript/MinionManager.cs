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
    public AudioClip Death;
    AudioSource mAudioSource;
    public bool IsAttacking;
    public int MaxHealth;
    public int CurrentHealth;
    public Transform GroundDetection;
    public Transform View;
    public float duration;    //the max time of a walking session (set to ten)
    float elapsedTime = 0f; //time since started walk
    float wait = 0f; //wait this much time
    float waitTime = 0f; //waited this much time
    bool move = true; //start moving
    float randomX;  //randomly go this X direction
    bool Hostile = false;
    float HostileTime = 3;
    RaycastHit2D ShootOnSight;
    RaycastHit2D groundinfo;
    void Start()
    {
        mMinionAction = GetComponent<MinionAction>();
        mAudioSource = GetComponent<AudioSource>();
        randomX = Random.Range(-2, 2);
        target = GameObject.Find("NewBorn").transform;

    }

    void Shoot()
    {
        mAudioSource.clip = ShotsSound;
        mAudioSource.Play();
        GameObject bullet = Instantiate(BossLaser, BossFirepoint.transform.position, transform.rotation) as GameObject;
        bullet.GetComponent<Rigidbody2D>().velocity = Vector3.right * laserspeed;
        if (Mathf.RoundToInt(target.position.x - transform.position.x) > 0)
        {
            Debug.Log("right");
            bullet.transform.localScale = new Vector3(1, 1, 1);
            transform.localScale = bullet.transform.localScale;
            bullet.GetComponent<Rigidbody2D>().velocity = Vector3.right * laserspeed;
            if( target.position.x - transform.position.x < 1 && target.position.x - transform.position.x>-1)
            {
                if (Mathf.RoundToInt(target.position.y - transform.position.y) < 0)
                {
                    Debug.Log("Rdown");
                    bullet.transform.eulerAngles = new Vector3(0, 0, -90);
                    bullet.GetComponent<Rigidbody2D>().velocity = Vector3.down * laserspeed;
                }
                else if (Mathf.RoundToInt(target.position.y - transform.position.y) > 0 )
                {
                    Debug.Log("Rup");
                    bullet.transform.eulerAngles = new Vector3(0, 0, 90);
                    bullet.GetComponent<Rigidbody2D>().velocity = Vector3.up * laserspeed;
                }
            }
            
        }
        else if (Mathf.RoundToInt(target.position.x - transform.position.x) <= 0)
        {
            Debug.Log("left");
            bullet.transform.localScale = new Vector3(-1, 1, 1);
            transform.localScale = bullet.transform.localScale;
            bullet.GetComponent<Rigidbody2D>().velocity = Vector3.left * laserspeed;
            if (target.position.x - transform.position.x < 1 && target.position.x - transform.position.x > -1)
            {
                if (Mathf.RoundToInt(target.position.y - transform.position.y) < 0 )
                {
                    Debug.Log("Ldown");
                    bullet.transform.eulerAngles = new Vector3(0, 0, 90);
                    bullet.GetComponent<Rigidbody2D>().velocity = Vector3.down * laserspeed;
                }
                else if (Mathf.RoundToInt(target.position.y - transform.position.y) > 0 )
                {
                    Debug.Log("Lup");
                    bullet.transform.eulerAngles = new Vector3(0, 0, -90);
                    bullet.GetComponent<Rigidbody2D>().velocity = Vector3.up * laserspeed;
                }
            }    
        }

    }

    void Roaming()
    {
        if (move)
        {
            if (elapsedTime < duration)
            {
                mMinionAction.Move(new Vector3(randomX, 0, 0) * Time.deltaTime);
                elapsedTime += Time.deltaTime;
            }
            else
            {
                move = false;
                wait = Random.Range(2, 5);
                waitTime = 0f;
            }
        }
        else
        {
            if (waitTime < wait)
            {
                waitTime += Time.deltaTime;
            }
            else
            {
                move = true;
                elapsedTime = 0f;
                randomX = Random.Range(-3, 3);

            }
        }
    }


    void Detection()
    {
        if (Vector3.Distance(transform.position, target.position) > MaxDist)
        {
            IsAttacking = false;
            CancelInvoke("Shoot");
        }
        else if (Vector3.Distance(transform.position, target.position) <= MaxDist && ShootOnSight.collider.tag == ("Player"))
        {
            Hostile = true;
        }
        Debug.DrawRay(View.position + (new Vector3(0.25f, 0, 0) * transform.localScale.x), (target.position - transform.position).normalized, Color.red);
    }

    void Act()
    {
        //print(ShootOnSight.collider.tag == "Player");
        if (Hostile == true)
        {
            if (Vector3.Distance(transform.position, target.position) <= MaxDist && Vector3.Distance(transform.position, target.position) >= MinDist)
            {
                mMinionAction.Move(target, moveSpeed);
            }
            if (Vector3.Distance(new Vector3(0, transform.position.y, 0), new Vector3(0, target.position.y, 0)) > 1.5f && target.position.y - transform.position.y > 0 && ShootOnSight.collider.tag == "Player")
            {
                mMinionAction.Jump(300);
            }
            if (ShootOnSight.collider.tag == "Player"&&IsAttacking==false)
            {
               InvokeRepeating("Shoot", 0, AttackSpeed);
               IsAttacking = true;
            }
            if (ShootOnSight.collider.tag != "Player" && Hostile == true)
            {
                HostileTime -= Time.deltaTime;
                if (HostileTime <= 0)
                {
                    Hostile = false;
                    CancelInvoke("Shoot");
                }
            }
            else
            {
                HostileTime = 3;
            }
        }
        else
        {
            Roaming();
        }
        if (groundinfo.collider == null)
        {
            mMinionAction.Jump(300);
        }
    }

    public void Damage(int damage)
    {
        Hostile = true;
        CurrentHealth -= damage;

        if (CurrentHealth <= 0)
        {
            SoundManager.instance.PlaySingleNew(Death);
            Destroy(gameObject);
        }

    }
    // Update is called once per frame
    void Update()
    {
        ShootOnSight = Physics2D.Raycast(View.position + (new Vector3(0.25f, 0, 0) * transform.localScale.x), (target.position - transform.position).normalized, Vector3.Distance(target.position, transform.position));
        groundinfo = Physics2D.Raycast(GroundDetection.position, Vector2.down, 2f);

        if(Vector3.Distance(transform.position,target.position)>=300)
        {
            Destroy(gameObject);
        }
    }

    private void LateUpdate()
    {
        Detection();
        Act();
    }
}
