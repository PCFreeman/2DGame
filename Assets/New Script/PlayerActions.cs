using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    public bool IsMoving;
    public bool IsJumping;
    public bool IsKicking;
    private Rigidbody2D myRigidbody;
    public void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }
    public void MoveAction(float speed)
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

    public void JumpAction(float JumpForce)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
          IsJumping = true;
          myRigidbody.AddForce(new Vector3(0, JumpForce, 0));
        }
    }
    public void KickAction(GameObject footpoint)
    {
        if (Input.GetKeyDown(KeyCode.X) )
        {
            IsKicking = true;
            footpoint.GetComponent<BoxCollider2D>().enabled = true;
            Debug.Log("kick");
        }

    }

    public void ShootAction(GameObject Bullet, GameObject WeaponPosition, Transform Characterrotation,float bulletSpeed)
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            GameObject bullet = Instantiate(Bullet, WeaponPosition.transform.position, Characterrotation.rotation) as GameObject;
            if (transform.localScale.x > 0)
            {
                bullet.GetComponent<Rigidbody2D>().velocity = Vector3.right * bulletSpeed;
                if (Input.GetKey(KeyCode.DownArrow) && Input.GetKeyDown(KeyCode.Z))
                {
                    bullet.transform.eulerAngles = new Vector3(0, 0, -90);
                    bullet.GetComponent<Rigidbody2D>().velocity = Vector3.down * bulletSpeed;
                }
                if (Input.GetKey(KeyCode.UpArrow) && Input.GetKeyDown(KeyCode.Z))
                {
                    bullet.transform.eulerAngles = new Vector3(0, 0, 90);
                    bullet.GetComponent<Rigidbody2D>().velocity = Vector3.up * bulletSpeed;
                }

            }
            else if (transform.localScale.x < 0)
            {
                bullet.transform.localScale = new Vector3(-1, 1, 1);
                bullet.GetComponent<Rigidbody2D>().velocity = Vector3.left * bulletSpeed;
                if (Input.GetKey(KeyCode.DownArrow) && Input.GetKeyDown(KeyCode.Z))
                {
                    bullet.transform.eulerAngles = new Vector3(0, 0, 90);
                    bullet.GetComponent<Rigidbody2D>().velocity = Vector3.down * bulletSpeed;
                }
                if (Input.GetKey(KeyCode.UpArrow) && Input.GetKeyDown(KeyCode.Z))
                {
                    bullet.transform.eulerAngles = new Vector3(0, 0, -90);
                    bullet.GetComponent<Rigidbody2D>().velocity = Vector3.up * bulletSpeed;
                }
            }
        }
    }
    // Update is called once per frame

}
