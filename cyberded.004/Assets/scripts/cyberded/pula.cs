using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pula : MonoBehaviour
{
    public Rigidbody2D rb;
    public float tm = 0;
    public float damage = 10;
    public float lifeTime;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics2D.Linecast(transform.position, transform.position, 1 << LayerMask.NameToLayer("Ground")))
        {
            Destroy(this.gameObject);
        }
        transform.Translate(Vector3.right*Time.deltaTime*speed);
        tm += Time.deltaTime;
        if (tm>lifeTime)
        {
            Destroy(this.gameObject);
        }
    }
}
