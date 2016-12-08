using UnityEngine;
using System.Collections;

public class BOSSHP : MonoBehaviour {

    public int MaxHealth;
    public int CurrentHealth;

    public float BlinkTime = 0.5f;
    public float BlinkAmount = 3;

    private float BlinkCounter = 0;


    // Update is called once per frame

    public void Damage(int damage)
    {
        CurrentHealth -= damage;

        if (CurrentHealth <= 0)
        {
            InvokeRepeating("BlinkThenDie", 0f, BlinkTime);
        }
    }

    private void BlinkThenDie()
    {
        gameObject.SetActive(!gameObject.activeSelf);

        if (BlinkCounter >= BlinkAmount)
        {
            CancelInvoke("BlinkThenDie");
            Destroy(gameObject);
        }
        ++BlinkCounter;
    }
}
