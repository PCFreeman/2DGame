using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public float Speed;
    public float JumpForce;
    public GameObject firepoint;
    public GameObject foot;
    public GameObject laser;
    public GameObject Explosive;
    public float BulletSpeed;
    public float ThrowPower;
    public int MaxHealth;
    public int CurrentHealth;
    public AudioClip StepSound;
    float BlinkTime = 0.5f;
    float BlinkAmount = 3;
    private float BlinkCounter = 0;
    PlayerActions mPlayerActions;
    public AudioClip Die;
    public float CDForAbility1;
    public float CDForAbility2;
    public float CDForAbility3;

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
            //-Destroy(gameObject);
            //SceneManager.LoadScene(2);
            UIManager.instance.Over.SetActive(true);
            Time.timeScale = 0;
        }
        ++BlinkCounter;
    }
    public void Damage(int damage)
    {
        CurrentHealth -= damage;

        if (CurrentHealth <= 0)
        {
            SoundManager.instance.PlaySingle(Die);
            InvokeRepeating("BlinkThenDie", 0f, BlinkTime);
        }
    }


    void Update()
    {
        mPlayerActions.MoveAction(Speed);
        mPlayerActions.JumpAction(JumpForce);
        mPlayerActions.KickAction(foot);
        mPlayerActions.ShootAction(laser, firepoint, transform, BulletSpeed);
        mPlayerActions.ThrowAction(Explosive,firepoint,transform, ThrowPower);

        if(!mPlayerActions.CanFire)
        {
            UIManager.instance.UpdateAbilityCD(UIManager.instance.Ability1, CDForAbility1, ref mPlayerActions.CanFire);
        }
        if (!mPlayerActions.CanKick)
        {
            UIManager.instance.UpdateAbilityCD(UIManager.instance.Ability2, CDForAbility2, ref mPlayerActions.CanKick);
        }
        if (!mPlayerActions.CanThrow)
        {
            UIManager.instance.UpdateAbilityCD(UIManager.instance.Ability3, CDForAbility3, ref mPlayerActions.CanThrow);
        }

        if(Input.GetKeyDown(KeyCode.F5))
        {
            Damage(100);
        }
        if(mPlayerActions.DeadZone ==true)
        {
            Damage(1000);
        }
    }
}
