using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExplosive : MonoBehaviour
{
    float timer = 1.0f;
    bool collide = false;
    public float blastradius;
    public AudioClip clip;
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("PlantForm") || collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("BOSS"))
        {
            collide = true;
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(this.transform.position, blastradius);
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        if (collide == true)
        {
            timer -= Time.deltaTime;
        }
        if(timer<=0)
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), blastradius);
            foreach (Collider2D en in hits)
            {
                if(en.tag=="Enemy")
                {
                    MinionManager min = en.GetComponent<MinionManager>();
                    min.Damage(100);
                }
                if (en.tag == "BOSS")
                {
                    BOSSManager boss = en.GetComponent<BOSSManager>();
                    boss.Damage(100);
                }
            }
            SoundManager.instance.PlaySingleNew(clip);
            Destroy(gameObject);
        }
    }
}
