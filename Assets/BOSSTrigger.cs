using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOSSTrigger : MonoBehaviour
{
    public bool isStepedon = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag =="Player")
        {
            isStepedon = true;
            gameObject.SetActive(false);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(true);
        isStepedon = false;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
