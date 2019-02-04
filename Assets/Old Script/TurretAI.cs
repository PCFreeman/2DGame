using UnityEngine;
using System.Collections;

public class TurretAI : MonoBehaviour {
    public GameObject TurretFirepoint;
    public GameObject TurretFirepoint2;
    public GameObject TurretFirepoint3;
    public GameObject TurretFirepoint4;
    public Transform target;
    public GameObject TurretBullet;
    private Animator myAnimator;
    public float BulletSpeed = 3f;
    public int MinDist = 10;
    private bool IsAttacking;
    public float AttackSpeed;
   
   

    void shoot()
    {
        GameObject bullet = Instantiate(TurretBullet, TurretFirepoint.transform.position, transform.rotation) as GameObject;
        GameObject bullet2 = Instantiate(TurretBullet, TurretFirepoint2.transform.position, transform.rotation) as GameObject;
        GameObject bullet3 = Instantiate(TurretBullet, TurretFirepoint3.transform.position, transform.rotation) as GameObject;
        GameObject bullet4 = Instantiate(TurretBullet, TurretFirepoint4.transform.position, transform.rotation) as GameObject;

        bullet.GetComponent<Rigidbody2D>().velocity = Vector3.right * BulletSpeed;
        bullet2.GetComponent<Rigidbody2D>().velocity = Vector3.right * BulletSpeed;
        bullet3.GetComponent<Rigidbody2D>().velocity = Vector3.right * BulletSpeed;
        bullet4.GetComponent<Rigidbody2D>().velocity = Vector3.right * BulletSpeed;
        if ((int)target.position.x - (int)transform.position.x > 0)
        {
            myAnimator.SetBool("IsShoot", true);
            transform.localScale = new Vector3(1, 1, 1);
            bullet.transform.localScale = new Vector3(1, 1, 1);
            bullet2.transform.localScale = new Vector3(1, 1, 1);
            bullet3.transform.localScale = new Vector3(1, 1, 1);
            bullet4.transform.localScale = new Vector3(1, 1, 1);
            bullet.GetComponent<Rigidbody2D>().velocity = Vector3.right * BulletSpeed;
            bullet2.GetComponent<Rigidbody2D>().velocity = Vector3.right * BulletSpeed;
            bullet3.GetComponent<Rigidbody2D>().velocity = Vector3.right * BulletSpeed;
            bullet4.GetComponent<Rigidbody2D>().velocity = Vector3.right * BulletSpeed;
           

        }
        else if ((int)target.position.x - (int)transform.position.x < 0)
        {
            myAnimator.SetBool("IsShoot", true);
            transform.localScale = new Vector3(-1, 1, 1);
            bullet.transform.localScale = new Vector3(-1, 1, 1);
            bullet2.transform.localScale = new Vector3(-1, 1, 1);
            bullet3.transform.localScale = new Vector3(-1, 1, 1);
            bullet4.transform.localScale = new Vector3(-1, 1, 1);
            bullet.GetComponent<Rigidbody2D>().velocity = Vector3.left * BulletSpeed;
            bullet2.GetComponent<Rigidbody2D>().velocity = Vector3.left * BulletSpeed;
            bullet3.GetComponent<Rigidbody2D>().velocity = Vector3.left * BulletSpeed;
            bullet4.GetComponent<Rigidbody2D>().velocity = Vector3.left * BulletSpeed;
       
        }
    }
        // Use this for initialization
        void Start()
        {
            GameObject go = GameObject.FindGameObjectWithTag("Player");

        target = go.transform;
        }

    void Awake()
    {
        myAnimator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, target.position) <= MinDist)
        {
           
            if (IsAttacking == false)
            {
                
                InvokeRepeating("shoot", 0, AttackSpeed);
                IsAttacking = true;
            }
        }
        else
        {
            myAnimator.SetBool("IsShoot", false);
            IsAttacking = false;
            CancelInvoke("shoot");
        }
    }
}
