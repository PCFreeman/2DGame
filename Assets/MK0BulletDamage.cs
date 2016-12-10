using UnityEngine;
using System.Collections;

public class MK0BulletDamage : MonoBehaviour {

    public int Damage;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Control>() == null)
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.GetComponent<BOSSHP>())
        {
            BOSSHP Health = collision.gameObject.GetComponent<BOSSHP>();
            Health.Damage(Damage);
        }
        else if(collision.gameObject.GetComponent<TurretHP>())
        {
            TurretHP Health1 = collision.gameObject.GetComponent<TurretHP>();
             Health1.Damage(Damage);
        }
    }
}
