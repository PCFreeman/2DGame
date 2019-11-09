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
        if (collision.gameObject.tag == "Sense")
        {
            MinionManager jump = collision.gameObject.GetComponentInParent<MinionManager>();
            jump.mMinionAction.Jump(300);
        }
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
            MinionManager Health = collision.gameObject.GetComponent<MinionManager>();
            Health.Damage(Damage);
        }
        if (collision.gameObject.GetComponent<BOSSHP>())
        {
            BOSSHP Health = collision.gameObject.GetComponent<BOSSHP>();
            Health.Damage(Damage);
        }
         if(collision.gameObject.GetComponent<TurretHP>())
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
        if (collision.gameObject.GetComponent<B1die>())
        {
            B1die Health = collision.gameObject.GetComponent<B1die>();
            Health.Damage(Damage);
        }
    }
}
