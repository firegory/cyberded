using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotating : MonoBehaviour
{
    public GameObject adult;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (adult.transform.rotation.y>179)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y,adult.transform.position.z + 0.1f);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y,adult.transform.position.z -0.1f);
        }
    }
}
