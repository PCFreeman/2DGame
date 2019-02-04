using UnityEngine;
using System.Collections;

public class Foot : MonoBehaviour
{
    public int Damage;
  
    public void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.gameObject.GetComponent<BOSSHP>())
        {
            BOSSHP Health = collision.gameObject.GetComponent<BOSSHP>();
            Health.Damage(Damage);
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

