using UnityEngine;
using System.Collections;

public class BOSSHP : MonoBehaviour {

    public int MaxHealth;
    public int CurrentHealth;
    [SerializeField]
    AudioSource baudio;
    [SerializeField]
    AudioClip die;


    // Update is called once per frame

    public void Damage(int damage)
    {
        CurrentHealth -= damage;
            baudio.clip = die;
            baudio.Play();
           
        if (CurrentHealth <= 0)
        {
            Destroy(gameObject,0.5f);
            GetComponent<BOSSAI>().enabled = false;
            
        }
    }

   
}
