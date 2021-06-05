using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iswall : MonoBehaviour
{
    // Start is called before the first frame update
    private float tm = 0;
    public bool wall;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        tm += Time.deltaTime;
        if (tm>1.0f)
        {
            wall = false;
            tm = 0;
        }
    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            wall = true;
            tm = 0.97f;
        }
        else
        {
            wall = false;
        }
    }

}
