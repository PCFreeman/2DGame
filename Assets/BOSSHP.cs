using UnityEngine;
using System.Collections;

public class BOSSHP : MonoBehaviour {

    public int MaxHealth;
    public int CurrentHealth;
    [SerializeField]
    AudioSource audio;
    [SerializeField]
    AudioClip die;


    // Update is called once per frame

    public void Damage(int damage)
    {
        CurrentHealth -= damage;
          
            Debug.Log("die");
        if (CurrentHealth <= 0)
        {
            audio.clip = die;
            audio.Play();
            Destroy(gameObject,1);
            GetComponent<BOSSAI>().enabled = false;
            
        }
    }

   
}
