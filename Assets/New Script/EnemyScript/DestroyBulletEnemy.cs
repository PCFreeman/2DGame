using UnityEngine;
using System.Collections;

public class DestroyBulletEnemy : MonoBehaviour
{

    public int Damage;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag=="Player")
        {
            Destroy(gameObject);
            PlayerManager Health = collision.gameObject.GetComponent<PlayerManager>();
            Health.Damage(Damage);
        }

    }

}
