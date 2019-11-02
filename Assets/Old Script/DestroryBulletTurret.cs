using UnityEngine;
using System.Collections;

public class DestroryBulletTurret : MonoBehaviour {
    public int Damage;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collision" + collision.gameObject.name);
        if (collision.gameObject.GetComponent<TurretAI>() == null || collision.gameObject.CompareTag("Ground"))
        {

            Destroy(gameObject);

        }
        if (collision.gameObject.tag == "PlantForm")
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.GetComponent<Health>())
        {
            Health Health = collision.gameObject.GetComponent<Health>();
            Health.Damage(Damage);
        }
    }
}
