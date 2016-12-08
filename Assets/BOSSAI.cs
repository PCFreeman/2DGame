using UnityEngine;
using System.Collections;

public class BOSSAI : MonoBehaviour
{
    private Rigidbody2D myRigidbody;
    public Transform target;
    public int moveSpeed;
    private Animator myAnimator;
    private Vector2 previousLocation;
    public int MaxDist = 10;
    public int MinDist = 10;
    public float laserspeed = 3f;
    public GameObject BossFirepoint;
    public GameObject BossLaser;

    void Awake()
    {
        myAnimator = GetComponent<Animator>();
    }

    void Start()
    {
        GameObject go = GameObject.FindGameObjectWithTag("Player");

        target = go.transform;
    }

    void Update()
    {
        Vector3 dir = target.position - transform.position;
        dir.z = 0.0f; // Only needed if objects don't share 'z' value

        previousLocation = transform.position;

        //Move Towards Target
        if (Vector3.Distance(transform.position, target.position) <= MinDist)
        {

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
            GameObject bullet = Instantiate(BossLaser, BossFirepoint.transform.position, transform.rotation) as GameObject;
            bullet.GetComponent<Rigidbody2D>().velocity = Vector3.right * laserspeed;
            if (target.position.y-transform.position.y<0)
            {
                bullet.transform.eulerAngles = new Vector3(0, 0, -90);
                bullet.GetComponent<Rigidbody2D>().velocity = Vector3.down * laserspeed;
            }
            if (Input.GetKey(KeyCode.UpArrow) && Input.GetKeyDown(KeyCode.Z))
            {
                bullet.transform.eulerAngles = new Vector3(0, 0, 90);
                bullet.GetComponent<Rigidbody2D>().velocity = Vector3.up * laserspeed;
            }
       if (transform.localScale.x < 0)
        {
            bullet.transform.localScale = new Vector3(-1, 1, 1);
            bullet.GetComponent<Rigidbody2D>().velocity = Vector3.left * laserspeed;
            if (Input.GetKey(KeyCode.DownArrow) && Input.GetKeyDown(KeyCode.Z))
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
        else
        { myAnimator.SetBool("IsWalk", false); }

        }
      
    }
   
}