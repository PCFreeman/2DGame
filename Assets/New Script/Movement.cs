using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public bool IsMoving;
    public bool IsJumping;

    private Rigidbody2D myRigidbody;
    public void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }
    public void move(float speed)
    {
        Vector3 direction = Vector3.zero;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.localScale = new Vector3(-1, 1, 1);
            direction.x = -1;
        }
        else if(Input.GetKey(KeyCode.RightArrow))
        {
            transform.localScale = new Vector3(1, 1, 1);
            direction.x = 1;
        }
        if (direction.x != 0)
        {
           IsMoving = true;
        }
        else
        {
           IsMoving = false;
        }
        transform.position += direction * Time.deltaTime * speed;
    }

    public void Jump(float JumpForce)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
          IsJumping = true;
          myRigidbody.AddForce(new Vector3(0, JumpForce, 0));
        }
    }

    // Update is called once per frame

}
