using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Health : MonoBehaviour {

    public int MaxHealth;
    public int CurrentHealth;

    public float BlinkTime = 0.5f;
    public float BlinkAmount = 3;

    private float BlinkCounter = 0;

    public RectTransform HealthBar;
    public Text HealthBarText;
	
	// Update is called once per frame
	void Update () {
        HealthBarText.text = CurrentHealth + "/" + MaxHealth;
        HealthBar.sizeDelta = new Vector2(CurrentHealth*3, HealthBar.sizeDelta.y);
    }

    public void Damage(int damage)
    {
        CurrentHealth -= damage;

        if(CurrentHealth <= 0)
        {
            InvokeRepeating("BlinkThenDie", 0f, BlinkTime);
        }
    }

    private void BlinkThenDie()
    {
        gameObject.SetActive(!gameObject.activeSelf);

        if(BlinkCounter >= BlinkAmount)
        {
            CancelInvoke("BlinkThenDie");
            Destroy(gameObject);
        }
        ++BlinkCounter;
    }
}
