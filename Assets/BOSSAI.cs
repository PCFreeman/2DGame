using UnityEngine;
using System.Collections;

public class BOSSAI : MonoBehaviour
{
    private Rigidbody2D myRigidbody;
    public Transform target;
    public int moveSpeed;
    private Animator myAnimator;
    private Vector2 previousLocation;
   
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
        transform.position += (target.position - transform.position).normalized * moveSpeed * Time.deltaTime;

        if(previousLocation.x - transform.position.x>=0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            myAnimator.SetBool("IsWalk", true);
        }
        else if(previousLocation.x - transform.position.x <= 0)
        {
              transform.localScale = new Vector3(1, 1, 1);
            myAnimator.SetBool("IsWalk", true);
        }
        if ((int) previousLocation.y != (int) transform.position.y)
        {
            myAnimator.SetBool("IsJump", true);
        }
        else
        {
            myAnimator.SetBool("IsJump", false);
        }
    }
    round
}