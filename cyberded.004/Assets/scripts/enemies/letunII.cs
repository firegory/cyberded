using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class letunII : MonoBehaviour
{
    private Rigidbody2D rb;
    public bool isalive = true;
    public bool isagred;
    public float speed1 = 3;
    public float rot;
    public bool isatacking;
    public GameObject charrecter;
    public float speed = 3;
    public float s;
    public GameObject pula;
    private GameObject pula1;
    private bool isChecking = false;
    private bool tmer = true;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isalive && !charrecter.GetComponent<hp>().ispaused)
        {
            if (!isatacking)
            {
                speed = speed1 * rot;
                if (transform.position.x - charrecter.transform.position.x >= 0)
                {
                    rot = -1;
                    transform.rotation = new Quaternion(transform.rotation.x, 0, transform.rotation.z, transform.rotation.w);
                }
                else
                {
                    rot = 1;
                    transform.rotation = new Quaternion(transform.rotation.x, 180, transform.rotation.z, transform.rotation.w);
                }
                atackStart();
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
        else
        {
            rb.velocity = new Vector2(0, 0);
        }
    }
    private IEnumerator atackEnd()
    {
        rb.velocity = new Vector2(0, 0);
        isatacking = true;
        pula1 = Instantiate(pula);
        var dir = charrecter.transform.position - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        pula1.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        pula1.transform.position = transform.position;
        yield return new WaitForSeconds(0.5f);
        isatacking = false;
    }
    private IEnumerator ti()
    {
        isChecking = true;
        yield return new WaitForSeconds(1);
        tmer = true;
        isChecking = false;
    }
}
