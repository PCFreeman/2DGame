using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Health : MonoBehaviour {

    public int MaxHealth;
    public int CurrentHealth;

    public float BlinkTime = 0.5f;
    public float BlinkAmount = 3;

    private float BlinkCounter = 0;

    public RectTransform HealthBar;
    public Text HealthBarText;

    public GameObject foot;
	
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
            SceneManager.LoadScene(2);
        }
        ++BlinkCounter;
    }

    public void Diactive()
    {
        foot.GetComponent<BoxCollider2D>().enabled = false;
    }
}
