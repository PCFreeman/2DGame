using UnityEngine;
using System.Collections;

public class Control : MonoBehaviour
{
    public GameObject firepoint;
    public GameObject laser;
    public float laserspeed = 3f;
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground")|| collision.gameObject.CompareTag("PlantForm"))
            {
            myAnimator.SetBool("IsJump",false);
            }
    }

    private Rigidbody2D myRigidbody;
    private Animator myAnimator;
    public float speed = 5f;
    public float moveForce = 10f;
    public float jump = 500f;
    // Use this for initialization

    public void Awake()
    {
        myAnimator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
    }
    void Start()
    {

    }
    void move()
    {
        float horiz = Input.GetAxisRaw("Horizontal");

        if (horiz < 0)
        {

            transform.localScale = new Vector3(-1, 1, 1);
            myAnimator.SetBool("IsWalk", true);
        }
        else if (horiz > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            myAnimator.SetBool("IsWalk", true);
        }
        else
        {
            myAnimator.SetBool("IsWalk", false);
        }
        transform.position += new Vector3(horiz, 0, 0) * Time.deltaTime * speed;

    }

    void FixedUpdate()
    {
        move();
    
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!myAnimator.GetBool("IsJump"))
            {
                myRigidbody.AddForce(new Vector3(0, jump, 0));
                myAnimator.SetBool("IsJump", true);
            }
        }

      
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.DownArrow))
        {
            myAnimator.SetBool("lookdown", true);
        }
        else
        {
            myAnimator.SetBool("lookdown", false);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            myAnimator.SetBool("kick",true);
        }
        else
        {
            myAnimator.SetBool("kick", false);
        }

            if (Input.GetKeyDown(KeyCode.Z))
        {
            GameObject bullet = Instantiate(laser, firepoint.transform.position, transform.rotation) as GameObject;
            if(transform.localScale.x>0)
            {
              bullet.GetComponent<Rigidbody2D>().velocity = Vector3.right* laserspeed;
                if (Input.GetKey(KeyCode.DownArrow)&&Input.GetKeyDown(KeyCode.Z))
                {
                    bullet.transform.eulerAngles = new Vector3(0, 0, -90);
                    bullet.GetComponent<Rigidbody2D>().velocity = Vector3.down * laserspeed;
                }
                if (Input.GetKey(KeyCode.UpArrow) && Input.GetKeyDown(KeyCode.Z))
                {
                    bullet.transform.eulerAngles = new Vector3(0, 0, 90);
                    bullet.GetComponent<Rigidbody2D>().velocity = Vector3.up * laserspeed;
                }

            }
            else if(transform.localScale.x<0)
            {
                bullet.transform.localScale= new Vector3(-1, 1, 1);
                bullet.GetComponent<Rigidbody2D>().velocity = Vector3.left * laserspeed;
                if (Input.GetKey(KeyCode.DownArrow) && Input.GetKeyDown(KeyCode.Z))
                {
                    bullet.transform.eulerAngles = new Vector3(0, 0, 90);
                    bullet.GetComponent<Rigidbody2D>().velocity = Vector3.down * laserspeed;
                }
                if (Input.GetKey(KeyCode.UpArrow) && Input.GetKeyDown(KeyCode.Z))
                {
                    bullet.transform.eulerAngles = new Vector3(0, 0, -90);
                    bullet.GetComponent<Rigidbody2D>().velocity = Vector3.up * laserspeed;
                }
            }
           
        }
        
    }
}
