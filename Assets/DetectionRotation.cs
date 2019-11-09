using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionRotation : MonoBehaviour
{
   public GameObject parentobject;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(parentobject.transform.position, new Vector3(0, 0, 1), Time.deltaTime * 80);
    }
}
