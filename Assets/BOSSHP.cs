using UnityEngine;
using System.Collections;

public class BOSSHP : MonoBehaviour {

    public int MaxHealth;
    public int CurrentHealth;
    [SerializeField]
    AudioSource die;
    

    // Update is called once per frame

    public void Damage(int damage)
    {
        CurrentHealth -= damage;
             die.Play();
            Debug.Log("die");
        if (CurrentHealth <= 0)
        {
            
            Destroy(gameObject);
            
        }
    }

   
}
