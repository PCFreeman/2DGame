using UnityEngine;
using System.Collections;

public class camera : MonoBehaviour {

    public GameObject player;       //Public variable to store a reference to the player game object
    
    // LateUpdate is called after Update each frame
    void LateUpdate () 
    {
        if (player.gameObject != null)
        {
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
        }
        
    }
}