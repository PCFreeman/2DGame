using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionRotation : MonoBehaviour
{
   public GameObject parentobject;
   MinionAction mMinionAction;
   public bool dodged = false;
   float cd = 1;
    // Start is called before the first frame update
    void Start()
    {
        mMinionAction = GetComponentInParent<MinionAction>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerProjectile" && dodged == false)
        {
          if(collision.gameObject.transform.eulerAngles.z == 0.0f)
            {
                mMinionAction.Jump(300);
                dodged = true;
            }
            else 
            {
                mMinionAction.SideShift(2);
                dodged = true;
            }

        }
    }


    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(parentobject.transform.position, new Vector3(0, 0, 1), Time.deltaTime * 150);
        Collider2D[] hits = Physics2D.OverlapBoxAll(new Vector2(transform.position.x, transform.position.y), new Vector2(0.27f,0.63f), transform.position.z);
        foreach (Collider2D en in hits)
        {
            if (en.tag == "PlayerExplosive"&& dodged == false)
            {
                mMinionAction.SideShift(4);
                dodged = true;
            }
        }
        cd -= Time.deltaTime;
        if(cd<=0)
        {
            cd = 2;
            dodged = false;
        }
    }
}
