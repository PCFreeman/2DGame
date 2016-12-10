using UnityEngine;
using System.Collections;

public class BOSSAI : MonoBehaviour
{
    public Transform target;
    public int moveSpeed;
    private Animator myAnimator;
    private Vector2 previousLocation;
    public int MaxDist = 10;
    public int MinDist = 10;
    public float laserspeed = 3f;
    public GameObject BossFirepoint;
    public GameObject BossLaser;
    public float AttackSpeed;
    private bool IsAttacking;
    void Awake()
    {
        myAnimator = GetComponent<Animator>();
    }

    void Start()
    {
        GameObject go = GameObject.FindGameObjectWithTag("Player");

        target = go.transform;
      
         
        
      

    }
    void move()
    {
        if (Vector3.Distance(transform.position, target.position) <= MinDist)
        {
            if (IsAttacking == false)
            {
                InvokeRepeating("shoot", 0, AttackSpeed);
                IsAttacking = true;
            }
            transform.position += (target.position - transform.position).normalized * moveSpeed * Time.deltaTime;
            if (previousLocation.x - transform.position.x >= 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                myAnimator.SetBool("IsWalk", true);
            }
            else if (previousLocation.x - transform.position.x <= 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
                myAnimator.SetBool("IsWalk", true);
            }
            if ((int)previousLocation.y != (int)transform.position.y)
            {
                myAnimator.SetBool("IsJump", true);
            }
        }
        else
        {
            myAnimator.SetBool("IsWalk", false);
            IsAttacking = false;
            CancelInvoke("shoot");
        }
}

    void shoot()
    {
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
        else if((int)target.position.x - (int)transform.position.x <= 0)
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
    void Update()
    {
       

        previousLocation = transform.position;

            move();
        // shoot();
       
       



    }
   
}