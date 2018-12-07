using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Control : MonoBehaviour
{
    public GameObject firepoint;
    public GameObject foot;
    public GameObject laser;
    public float laserspeed = 3f;


    [SerializeField]
    private HudButton leftButton;
    [SerializeField]
    private HudButton RightButton;
    [SerializeField]
    private HudButton UpButton;
    [SerializeField]
    private HudButton DownButton;

    [SerializeField]
    private HudButton ShootButton;
    [SerializeField]
    private HudButton MeleeButton;
    [SerializeField]
    private HudButton JumpButton;

    [SerializeField]
    AudioSource audiosource;
    [SerializeField]
    AudioClip j;
    [SerializeField]
    AudioClip s;
    [SerializeField]
    public AudioClip steps;
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground")|| collision.gameObject.CompareTag("PlantForm"))
            {
            myAnimator.SetBool("IsJump",false);
            }
    }

    private Rigidbody2D myRigidbody;
    public Animator myAnimator;
    public float speed = 5f;
    public float moveForce = 10f;
    public float jump = 500f;
    // Use this for initialization

    public void Awake()
    {
        myAnimator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    void move()
    {
        float horiz = Input.GetAxisRaw("Horizontal");

        Vector3 direction = Vector3.zero;
        
        if (leftButton.IsPressed || horiz < 0)
        {

            transform.localScale = new Vector3(-1, 1, 1);
            myAnimator.SetBool("IsWalk", true);
            direction.x = -1;
        }
        else if (RightButton.IsPressed || horiz > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            myAnimator.SetBool("IsWalk", true);
            direction.x = 1;
        }
        else
        {
            myAnimator.SetBool("IsWalk", false);
        }

        transform.position += direction * Time.deltaTime * speed;
    }

    

    // Update is called once per frame
    void Update()
    {
        move();

        if (Input.GetKey(KeyCode.DownArrow)||DownButton.IsPressed )
        {
            myAnimator.SetBool("lookdown", true);
        }
        else
        {
            myAnimator.SetBool("lookdown", false);
        }

        if (Input.GetKeyDown(KeyCode.X)|| MeleeButton.IsPressed)
        {
            myAnimator.SetBool("kick",true);
            foot.GetComponent<BoxCollider2D>().enabled = true;
            Debug.Log("kick");
        }
        else
        {
            myAnimator.SetBool("kick", false);
        }

        if (Input.GetKeyDown(KeyCode.Space)||JumpButton.IsPressed)
        {
            if (!myAnimator.GetBool("IsJump"))
            {
                audiosource.clip = j;
                audiosource.Play();

                myRigidbody.AddForce(new Vector3(0, jump, 0));
                myAnimator.SetBool("IsJump", true);
            }
        }

        if (Input.GetKeyDown(KeyCode.Z)||ShootButton.IsPressed)
        {
            GameObject bullet = Instantiate(laser, firepoint.transform.position, transform.rotation) as GameObject;
            if(transform.localScale.x>0)
            {
              bullet.GetComponent<Rigidbody2D>().velocity = Vector3.right* laserspeed;
                if ( DownButton.IsPressed && ShootButton.IsPressed)
                {
                    bullet.transform.eulerAngles = new Vector3(0, 0, -90);
                    bullet.GetComponent<Rigidbody2D>().velocity = Vector3.down * laserspeed;
                }
                if (UpButton.IsPressed && ShootButton.IsPressed)
                {
                    bullet.transform.eulerAngles = new Vector3(0, 0, 90);
                    bullet.GetComponent<Rigidbody2D>().velocity = Vector3.up * laserspeed;
                }

            }
            else if(transform.localScale.x<0)
            {
                bullet.transform.localScale= new Vector3(-1, 1, 1);
                bullet.GetComponent<Rigidbody2D>().velocity = Vector3.left * laserspeed;
                if ( DownButton.IsPressed &&  ShootButton.IsPressed)
                {
                    bullet.transform.eulerAngles = new Vector3(0, 0, 90);
                    bullet.GetComponent<Rigidbody2D>().velocity = Vector3.down * laserspeed;
                }
                if ( UpButton.IsPressed &&  ShootButton.IsPressed)
                {
                    bullet.transform.eulerAngles = new Vector3(0, 0, -90);
                    bullet.GetComponent<Rigidbody2D>().velocity = Vector3.up * laserspeed;
                }
            }
            audiosource.clip = s;
            audiosource.Play();
        }
        
    }
}
