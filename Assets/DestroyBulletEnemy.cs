﻿using UnityEngine;
using System.Collections;

public class DestroyBulletEnemy : MonoBehaviour
{

    public int Damage;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<BOSSAI>()== null|| collision.gameObject.CompareTag("Ground"))
        {
           
                Destroy(gameObject);
           
        }
        if(collision.gameObject.GetComponent<Health>())
        {
            Health Health = collision.gameObject.GetComponent<Health>();
            Health.Damage(Damage);
        }
    }
}
