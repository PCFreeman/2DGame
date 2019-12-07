using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class BOSSManager : MonoBehaviour
{
    // Start is called before the first frame update
    public BOSSAction mBossAction;
    public Transform target;
    public GameObject BossFirepoint;
    public GameObject BossLaser;
    public GameObject Explosive;
    public int MaxDist = 0;
    public int MinDist = 0;
    public float laserspeed;
    public float AttackSpeed;
    public float ThrowPower;
    public int moveSpeed;
    public AudioClip ShotsSound;
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
    RaycastHit2D ShootOnSight;
    RaycastHit2D groundinfo;
    SpriteRenderer m_SpriteRenderer;
    float ThrowTimer = 1;
    bool Throwing = false;
    void Start()
    {
        mBossAction = GetComponent<BOSSAction>();
        mAudioSource = GetComponent<AudioSource>();
        randomX = Random.Range(-2, 2);
        Hostile = true;
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void footsteps()
    {
        //SoundManager.instance.PlaySingle(StepSound);
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
            if (target.position.x - transform.position.x < 1 && target.position.x - transform.position.x > -1)
            {
                if (Mathf.RoundToInt(target.position.y - transform.position.y) < 0)
                {
                    Debug.Log("Rdown");
                    bullet.transform.eulerAngles = new Vector3(0, 0, -90);
                    bullet.GetComponent<Rigidbody2D>().velocity = Vector3.down * laserspeed;
                }
                else if (Mathf.RoundToInt(target.position.y - transform.position.y) > 0)
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
                if (Mathf.RoundToInt(target.position.y - transform.position.y) < 0)
                {
                    Debug.Log("Ldown");
                    bullet.transform.eulerAngles = new Vector3(0, 0, 90);
                    bullet.GetComponent<Rigidbody2D>().velocity = Vector3.down * laserspeed;
                }
                else if (Mathf.RoundToInt(target.position.y - transform.position.y) > 0)
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
                mBossAction.Move(new Vector3(randomX, 0, 0) * Time.deltaTime);
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

    void Normal()
    {
        print(ShootOnSight.collider.tag );
        if (Hostile == true)
        {
            if (Vector3.Distance(transform.position, target.position) <= MaxDist && Vector3.Distance(transform.position, target.position) >= MinDist)
            {
                mBossAction.Move(target, moveSpeed);
            }
            if (Vector3.Distance(new Vector3(0, transform.position.y, 0), new Vector3(0, target.position.y, 0)) > 1.5f && target.position.y - transform.position.y > 0 && ShootOnSight.collider.tag == "Player")
            {
                mBossAction.Jump(100*(Vector3.Distance(transform.position,target.position)));
            }
            if (ShootOnSight.collider.tag == "Player" && IsAttacking == false)
            {
                InvokeRepeating("Shoot", 0, AttackSpeed);
                IsAttacking = true;
            }

        }
        if (groundinfo.collider == null)
        {
            mBossAction.Jump(300);
        }
    }

    void Anger()
    {
        if (Vector3.Distance(transform.position, target.position) <= MaxDist && Vector3.Distance(transform.position, target.position) >= MinDist)
        {
            mBossAction.Move(target, moveSpeed);
        }
        if (Vector3.Distance(new Vector3(0, transform.position.y, 0), new Vector3(0, target.position.y, 0)) > 1.5f)
        {
            mBossAction.Jump(100 * (Vector3.Distance(transform.position, target.position)));
        }

        if (ShootOnSight.collider.tag == "Player")
        {
            if (IsAttacking == false)
            {
                InvokeRepeating("Shoot", 0, AttackSpeed);
                IsAttacking = true;
            }
        }
        if (groundinfo.collider == null)
        {
            mBossAction.Jump(300);
        }

        if (Throwing == false)
        {
            mBossAction.ThrowAction(Explosive, BossFirepoint, transform, ThrowPower);
            Throwing = true;
        }
        else
        {
            ThrowTimer -= Time.deltaTime;
            if (ThrowTimer <= 0)
            {
                Throwing = false;
                ThrowTimer = 1;
            }
        }
    }

    public void Damage(int damage)
    {
        CurrentHealth -= damage;


        if (CurrentHealth <= 0)
        {
            Destroy(gameObject);
            SceneManager.LoadScene(3);
        }

    }
    // Update is called once per frame
    void Update()
    {
        ShootOnSight = Physics2D.Raycast(View.position + (new Vector3(0.25f, 0, 0) * transform.localScale.x), (target.position - transform.position).normalized, Vector3.Distance(target.position, transform.position));
        groundinfo = Physics2D.Raycast(GroundDetection.position, Vector2.down, 2f);
      
        if (CurrentHealth <= 60&& CurrentHealth > 30)
        {
            m_SpriteRenderer.color = Color.yellow;
            Anger();
        }
        else if(CurrentHealth <= 30)
        {
            m_SpriteRenderer.color = Color.red;
            GameEventManager.instance.Summon();
            Anger();
        }
        else
        {
            Normal();
            Debug.DrawRay(View.position + (new Vector3(0.25f, 0, 0) * transform.localScale.x), (target.position - transform.position).normalized, Color.red);

        }
    }

}
