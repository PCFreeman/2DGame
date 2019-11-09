using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionAction : MonoBehaviour
{
    Animator mAnimator;
    private Vector2 previousLocation;
    private Rigidbody2D myRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        mAnimator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("PlantForm"))
        {
            //Am.JumpAnimation(false);
            AnimationManager.instance.PlayAnimation(mAnimator, "Jump", false);
        }
    }
    //public void Shoot(Transform target, GameObject Bullet, GameObject WeaponPosition, Transform Characterrotation, float bulletSpeed, float attackspeed)
    //{
    //    GameObject bullet = Instantiate(Bullet, WeaponPosition.transform.position, Characterrotation.rotation) as GameObject;
    //    bullet.GetComponent<Rigidbody2D>().velocity = Vector3.right * bulletSpeed;
    //    if ((int)target.position.x - (int)transform.position.x > 0)
    //    {
    //        Debug.Log("right");
    //        bullet.transform.localScale = new Vector3(1, 1, 1);
    //        bullet.GetComponent<Rigidbody2D>().velocity = Vector3.right * bulletSpeed;
    //        if ((int)target.position.y - (int)transform.position.y < 0)
    //        {
    //            Debug.Log("Rdown");
    //            bullet.transform.eulerAngles = new Vector3(0, 0, -90);
    //            bullet.GetComponent<Rigidbody2D>().velocity = Vector3.down * bulletSpeed;
    //        }
    //        else if ((int)target.position.y - (int)transform.position.y > 0)
    //        {
    //            Debug.Log("Rup");
    //            bullet.transform.eulerAngles = new Vector3(0, 0, 90);
    //            bullet.GetComponent<Rigidbody2D>().velocity = Vector3.up * bulletSpeed;
    //        }
    //    }
    //    else if ((int)target.position.x - (int)transform.position.x <= 0)
    //    {
    //        Debug.Log("left");
    //        bullet.transform.localScale = new Vector3(-1, 1, 1);
    //        bullet.GetComponent<Rigidbody2D>().velocity = Vector3.left * bulletSpeed;
    //        if ((int)target.position.y - (int)transform.position.y < 0)
    //        {
    //            Debug.Log("Ldown");
    //            bullet.transform.eulerAngles = new Vector3(0, 0, 90);
    //            bullet.GetComponent<Rigidbody2D>().velocity = Vector3.down * bulletSpeed;
    //        }
    //        else if ((int)target.position.y - (int)transform.position.y > 0)
    //        {
    //            Debug.Log("Lup");
    //            bullet.transform.eulerAngles = new Vector3(0, 0, -90);
    //            bullet.GetComponent<Rigidbody2D>().velocity = Vector3.up * bulletSpeed;
    //        }
    //    }
    //    SoundManager.instance.PlaySingle(ShotsSound);
    //}

    public void Move(Transform target,float moveSpeed)
    {
            transform.position += (target.position - transform.position).normalized * moveSpeed * Time.deltaTime;
            if (previousLocation.x - transform.position.x >= 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                AnimationManager.instance.PlayAnimation(mAnimator, "Walk", true);

            }
            else if (previousLocation.x - transform.position.x <= 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
                AnimationManager.instance.PlayAnimation(mAnimator, "Walk", true);

            }
        }
    public void Jump(float JumpForce)
    {
      if (!mAnimator.GetBool("Jump"))
        {
                myRigidbody.AddForce(new Vector3(0, JumpForce, 0));
                //Am.JumpAnimation(true);
                AnimationManager.instance.PlayAnimation(mAnimator, "Jump", true);
        }
      
    }

    private void Update()
    {
        previousLocation = transform.position;
        if ((int)previousLocation.x - (int)transform.position.x == 0)
        {
            AnimationManager.instance.PlayAnimation(mAnimator, "Walk", false);
        }
    }
    // Update is called once per frame

    //IEnumerator ShootCoroutine()
    //{
    //    Shoot();
    //    yield return new WaitForSeconds(1);
    //    Shoot();
    //}

    //public void ShootStartCoroutine()
    //{
    //    StartCoroutine("ShootCoroutine");
    //}
}
