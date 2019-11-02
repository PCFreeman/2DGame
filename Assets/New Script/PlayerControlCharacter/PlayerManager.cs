using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public float Speed;
    public float JumpForce;
    public GameObject firepoint;
    public GameObject foot;
    public GameObject laser;
    public float BulletSpeed;
    public int MaxHealth;
    public int CurrentHealth;
    public AudioClip StepSound;
    float BlinkTime = 0.5f;
    float BlinkAmount = 3;
    private float BlinkCounter = 0;
    PlayerActions mPlayerActions;


    // Start is called before the first frame update
    void Start()
    {
        mPlayerActions = GetComponent<PlayerActions>();
    }

    public void Diactive()
    {
        foot.GetComponent<BoxCollider2D>().enabled = false;
    }
    public void footsteps()
    {
        SoundManager.instance.PlaySingle(StepSound);
    }
    private void BlinkThenDie()
    {
        gameObject.SetActive(!gameObject.activeSelf);

        if (BlinkCounter >= BlinkAmount)
        {
            CancelInvoke("BlinkThenDie");
            Destroy(gameObject);
            SceneManager.LoadScene(2);
        }
        ++BlinkCounter;
    }
    public void Damage(int damage)
    {
        CurrentHealth -= damage;

        if (CurrentHealth <= 0)
        {
            InvokeRepeating("BlinkThenDie", 0f, BlinkTime);
        }
    }

    void Update()
    {
        mPlayerActions.MoveAction(Speed);
        mPlayerActions.JumpAction(JumpForce);
        mPlayerActions.KickAction(foot);
        mPlayerActions.ShootAction(laser,firepoint,transform,BulletSpeed);
    }
}
