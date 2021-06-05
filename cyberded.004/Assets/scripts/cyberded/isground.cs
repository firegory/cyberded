using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isground : MonoBehaviour
{
    // Start is called before the first frame update
    private float tm = 0;
    public bool grounded;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        tm += Time.deltaTime;
        if (tm > 1.0f)
        {
            grounded = false;
            tm = 0;
        }
    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8 || collision.gameObject.layer == 11)
        {
            grounded = true;
            tm = 0.97f;
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8 || collision.gameObject.layer == 11)
        {
            grounded = true;
            tm = 0.97f;
        }
    }
}
