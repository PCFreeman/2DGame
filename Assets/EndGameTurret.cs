using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class EndGameTurret : MonoBehaviour {


    public int MaxHealth;
    public int CurrentHealth;



    // Update is called once per frame

    public void Damage(int damage)
    {
        CurrentHealth -= damage;

        if (CurrentHealth <= 0)
        {
            Destroy(gameObject);
            SceneManager.LoadScene(3);
        }
    }
}
