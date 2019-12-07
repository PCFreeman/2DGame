using UnityEngine;
using System.Collections;

public class MK0BulletDamage : MonoBehaviour {

    public int Damage;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag =="PlantForm")
        {
            Destroy(gameObject);

        }
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
            MinionManager Health = collision.gameObject.GetComponent<MinionManager>();
            Health.Damage(Damage);
        }
        if (collision.gameObject.tag == "BOSS")
        {
           Destroy(gameObject);
           BOSSManager BHealth = collision.gameObject.GetComponent<BOSSManager>();
           BHealth.Damage(Damage);

        }
        if (collision.gameObject.GetComponent<TurretHP>())
        {
            Debug.Log("hit");
            TurretHP Health = collision.gameObject.GetComponent<TurretHP>();
             Health.Damage(Damage);
        }
        if (collision.gameObject.GetComponent<EndGameTurret>())
        {
            EndGameTurret Health = collision.gameObject.GetComponent<EndGameTurret>();
            Health.Damage(Damage);
        }
    }
}
