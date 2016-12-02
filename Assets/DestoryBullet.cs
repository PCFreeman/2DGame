using UnityEngine;
using System.Collections;

public class DestoryBullet : MonoBehaviour
{

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Control>() == null)
        {
            Destroy(gameObject);
        }
    }

}
