using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionRotation : MonoBehaviour
{
   public GameObject parentobject;
   BOSSAction mBossAction;
   bool dodged = false;
   float cd = 1;
    // Start is called before the first frame update
    void Start()
    {
        mBossAction = GetComponentInParent<BOSSAction>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerProjectile" && dodged == false)
        {
          if(collision.gameObject.transform.eulerAngles.z == 0.0f)
            {
                mBossAction.Jump(300);
                dodged = true;
            }
            else 
            {
                mBossAction.SideShift(200);
                dodged = true;
            }

        }
    }


    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(parentobject.transform.position, new Vector3(0, 0, 1), Time.deltaTime * 250);
        Collider2D[] hits = Physics2D.OverlapBoxAll(new Vector2(transform.position.x, transform.position.y), new Vector2(0.27f,0.63f), transform.position.z);
        foreach (Collider2D en in hits)
        {
            if (en.tag == "PlayerExplosive"&& dodged == false)
            {
                mBossAction.SideShift(250);
                dodged = true;
            }
        }
        if(dodged==true)
        {
        cd -= Time.deltaTime;
        }
        if(cd<=0)
        {
            cd = 2;
            dodged = false;
        }
    }
}
