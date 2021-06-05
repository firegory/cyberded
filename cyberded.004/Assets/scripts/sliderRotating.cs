using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sliderRotating : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject adult;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (adult.transform.rotation.y > 179)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, adult.transform.position.z + 0.1f);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, adult.transform.position.z - 0.1f);
        }
        if ((Mathf.Round(adult.transform.rotation.y) != 1 && Mathf.Round(adult.transform.rotation.y) != -1))
        {
            transform.Rotate(0, 0,  - transform.rotation.eulerAngles.z, Space.Self);
        }
        else
        {
            transform.Rotate(0, 0, 180 - transform.rotation.eulerAngles.z, Space.Self);
        }
    }
}
