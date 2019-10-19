using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public float Speed;
    public float JumpForce;
    Movement m;
    AnimationManager Am;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("PlantForm"))
        {
            Am.Jump(false);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
       m = GetComponent<Movement>();
       Am = GetComponent<AnimationManager>();
    }
    void MovewithAnimation()
    {
        m.move(Speed);
        if (m.IsMoving)
        {
            Am.Walk(true);
        }
        else
        {
            Am.Walk(false);
        }
        m.Jump(JumpForce);
        if(m.IsJumping)
        {
            Am.Jump(true);
        }
        else
        {
            Am.Jump(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        MovewithAnimation();    
    }
}
