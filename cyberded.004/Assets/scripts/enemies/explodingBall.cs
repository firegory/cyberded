using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explodingBall : MonoBehaviour
{
    public Rigidbody2D rb;
    public float tm = 0;
    public float damage = 10;
    public float speedx;
    public float speedy;
    public GameObject explode;
    public GameObject groundcheck;
    public isground isg;
    public int rot;
    // Start is called before the first frame update
    void Start()
    {
        isg = groundcheck.GetComponent<isground>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        tm += Time.deltaTime;
        if (tm < 4)
        {
            if (speedx > 0)
            {
                if (isg.grounded)
                {
                    speedx -= Time.deltaTime * 5;
                }
                speedx -= Time.deltaTime * 1;
            }
            if (speedy>-20&&!isg.grounded)
            {
                speedy -= Time.deltaTime * 10;
            }
            rb.velocity = new Vector2(speedx*rot*1.5f, speedy);
            //transform.Translate(Vector3.right * Time.deltaTime * speed);
        }
        else if (tm<4.3f)
        {
            explode.SetActive(true);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
