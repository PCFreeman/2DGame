using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class EndGameTurret : MonoBehaviour {


    public int MaxHealth;
    public int CurrentHealth;
    public GameObject boss;
    public GameObject player;
    public GameObject map;


    // Update is called once per frame

    public void Damage(int damage)
    {
        CurrentHealth -= damage;

        if (CurrentHealth <= 0)
        {
            Destroy(gameObject);
            boss.SetActive(true);
            player.SetActive(false);
            map.SetActive(true);
           // SceneManager.LoadScene(3);
        }
    }
}
