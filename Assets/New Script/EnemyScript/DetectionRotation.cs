using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionRotation : MonoBehaviour
{
   public GameObject parentobject;
   MinionAction mMinionAction;

    // Start is called before the first frame update
    void Start()
    {
        mMinionAction = GetComponentInParent<MinionAction>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerProjectile")
        {
          if(collision.gameObject.transform.eulerAngles.z == 0.0f)
            {
                mMinionAction.Jump(300);
                print(collision.transform.eulerAngles);
            }
            else 
            {
                mMinionAction.SideShift(150);
                print(collision.transform.eulerAngles);
            }

        }
      
    }


    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(parentobject.transform.position, new Vector3(0, 0, 1), Time.deltaTime * 150);

      
    }
}
