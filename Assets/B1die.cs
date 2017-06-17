using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class B1die : MonoBehaviour
{

    public int MaxHealth;
    public int CurrentHealth;



    // Update is called once per frame

    public void Damage(int damage)
    {
        CurrentHealth -= damage;


        if (CurrentHealth <= 0)
        {
            SceneManager.LoadScene(3);
        }

    }


}
