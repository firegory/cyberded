using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kislota : MonoBehaviour
{
    public Rigidbody2D rb;
    public float tm = 0;
    public float damage = 10;
    public float speed = 15;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * speed);
        tm += Time.deltaTime;
        if (tm > 10)
        {
            Destroy(this.gameObject);
        }
        if (Physics2D.Linecast(transform.position, transform.position, 1 << LayerMask.NameToLayer("Ground")))
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            plauerCharrecter plauer = collision.gameObject.GetComponent<plauerCharrecter>();
            if (plauer != null)
            {
                plauer.Hurt(damage);
                Destroy(this.gameObject);
            }
        }
    }

}
