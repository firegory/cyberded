using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ryvLetunII : MonoBehaviour
{
    public GameObject wallcheckR;
    private iswall isR;
    private Animator anim;
    private bool wallR;
    private Rigidbody2D rb;
    public bool isalive = true;
    public bool isagred;
    public bool isatacking;
    public GameObject charrecter;
    public float speed = 3;
    public float s;
    private bool isChecking = false;
    private bool tmer = true;
    public int damage = 15;
    public float tim = 0;
    public bool isatacking1 = false;
    public bool hasAttacked;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isR = wallcheckR.GetComponent<iswall>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        wallR = isR.wall;
        if (isalive && !charrecter.GetComponent<hp>().ispaused)
        {
            if (!isatacking)
            {
                if (transform.position.x - charrecter.transform.position.x >= 0)
                {
                    anim.SetBool("rot", false);
                }
                else
                {
                    anim.SetBool("rot", true);
                }

                atackStart();
            }
            else
            {
                tim += Time.deltaTime;
                if (wallR||tim >=5)
                {
                    isatacking = false;
                    isatacking1 = false;
                    transform.rotation = new Quaternion(0, 0, 0, 0);
                }
                else if (isatacking1 == false)
                {
                    rb.velocity = new Vector2(0, 0);
                }
                else
                {
                        transform.Translate(Vector3.right * Time.deltaTime * 10);
                }
            }
        }
        else
        {
            rb.velocity = new Vector2(0, 0);
        }
    }
    public void atackStart()
    {
        s = Mathf.Abs(transform.position.x - charrecter.transform.position.x) + Mathf.Abs(transform.position.y - charrecter.transform.position.y);
        if (s < 4)
        {
            if (charrecter.transform.position.y < transform.position.y)
            {
                rb.velocity = new Vector2(rb.velocity.x, speed);
            }
            else if (charrecter.transform.position.y > transform.position.y)
            {
                rb.velocity = new Vector2(rb.velocity.x, -speed);
            }
            if (charrecter.transform.position.x < transform.position.x)
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
            }
            else if (charrecter.transform.position.x > transform.position.x)
            {
                rb.velocity = new Vector2(-speed, rb.velocity.y);
            }
            if (tmer)
            {
                tmer = false;
                if (!isChecking)
                {
                    StartCoroutine(ti());
                }
                StartCoroutine(atackEnd());
            }
        }
        else if (s < 12)
        {
            if (charrecter.transform.position.y > transform.position.y)
            {
                rb.velocity = new Vector2(rb.velocity.x, speed);
            }
            else if (charrecter.transform.position.y < transform.position.y)
            {
                rb.velocity = new Vector2(rb.velocity.x, -speed);
            }
            if (charrecter.transform.position.x > transform.position.x)
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
            }
            else if (charrecter.transform.position.x < transform.position.x)
            {
                rb.velocity = new Vector2(-speed, rb.velocity.y);
            }
            if (tmer&&s<9&&!wallR)
            {
                tmer = false;
                if (!isChecking)
                {
                    StartCoroutine(ti());
                }
                StartCoroutine(atackEnd());
            }
        }
        else
        {
            rb.velocity = new Vector2(0, 0);
        }
    }
    private IEnumerator atackEnd()
    {
        if (!isatacking)
        {
            rb.velocity = new Vector2(0, 0);
            hasAttacked = false;
            tim = 0;
            isatacking = true;
            isatacking1 = false;
            yield return new WaitForSeconds(0.7f);
            anim.SetBool("rot", true);
            rb.velocity = new Vector2(0, 0);
            isatacking1 = true;
            var dir = charrecter.transform.position - transform.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.Translate(Vector3.right * Time.deltaTime * 20);
        }
    }
    private IEnumerator ti()
    {
        isChecking = true;
        yield return new WaitForSeconds(5);
        tmer = true;
        isChecking = false;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (isatacking&&!hasAttacked)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                plauerCharrecter plauer = collision.gameObject.GetComponent<plauerCharrecter>();
                if (plauer != null)
                {
                    plauer.Hurt(damage);
                    hasAttacked = true;
                }
            }
        }
    }
}
