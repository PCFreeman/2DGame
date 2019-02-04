using UnityEngine;
using System.Collections;

public class TurretHP : MonoBehaviour {

    public int MaxHealth;
    public int CurrentHealth;




    // Update is called once per frame

    public void Damage(int damage)
    {
        CurrentHealth -= damage;

    
        if (CurrentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
