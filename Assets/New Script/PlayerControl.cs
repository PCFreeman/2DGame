using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Rigidbody2D myRigidbody;
    public float speed = 5f;
    public float moveForce = 10f;
    public float jump = 500f;

    public void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }
    void move()
    {
        float horiz = Input.GetAxisRaw("Horizontal");

        Vector3 direction = Vector3.zero;
        
        if (horiz < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            //BodyMove.Walk(true); 
            direction.x = -1;
        }
        else if (horiz > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            //BodyMove.Walk(true);
            direction.x = 1;
        }
        else
        {
            //BodyMove.Walk(false);
        }

        transform.position += direction * Time.deltaTime * speed;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        move();

        if (Input.GetKeyDown(KeyCode.Space) )
        {
            //if (!myAnimator.GetBool("IsJump"))
            //{
                myRigidbody.AddForce(new Vector3(0, jump, 0));
                //myAnimator.SetBool("IsJump", true);
            //}
        }
    }
}
