using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jabaII : MonoBehaviour
{
    public Transform groundcheckL;
    public Transform groundcheckR;
    public GameObject wallcheckR;
    private iswall isR;
    public bool wallR;
    public int speed = 5;
    public float damage = 20.0f;
    public int rot = -1;
    public bool isChecking = false;
    public bool tmer = true;
    public bool isChecking1 = false;
    public bool tmer1 = true;
    public GameObject charrecter;
    Rigidbody2D rb;
    public float s;
    public bool isAgred;
    public bool isAttacking;
    public bool alive = true;
    public GameObject pula;
    private GameObject pula1;
    public Transform gunpoint;
    private kislota k;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isR = wallcheckR.GetComponent<iswall>();
    }

    // Update is called once per frame
    void Update()
    {
        if (alive && !charrecter.GetComponent<hp>().ispaused)
        {
            wallR = isR.wall;
            if (Physics2D.Linecast(transform.position, groundcheckL.position, 1 << LayerMask.NameToLayer("Water")) || Physics2D.Linecast(transform.position, groundcheckR.position, 1 << LayerMask.NameToLayer("Water")))
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
            }
            else
            {
                rb.velocity = new Vector2(rb.velocity.x, -5);
            }
            if (isAttacking)
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
            else if (isAgred)
            {
                atackStart();
                if (wals())
                {
                    rb.velocity = new Vector2(0, rb.velocity.y);
                }
                else if (canWalk())
                {
                    rb.velocity = new Vector2(0, rb.velocity.y);
                }
                else
                {
                    rb.velocity = new Vector2(speed * rot, rb.velocity.y);
                }
            }
            else
            {
                speed = 5;
                if (wallR || !Physics2D.Linecast(transform.position, groundcheckR.position, 1 << LayerMask.NameToLayer("Water")))
                {
                    if (tmer)
                    {
                        tmer = false;
                        if (!isChecking)
                        {
                            StartCoroutine(ti());
                        }
                        transform.Rotate(0, 180, 0, Space.Self);
                        rot *= -1;
                    }
                }
                rb.velocity = new Vector2(speed * rot, rb.velocity.y);
                atackStart();
            }
        }
        else
        {
            rb.velocity = new Vector2(0, 0);
        }
    }
    private bool canWalk()
    {
        bool can;
        if (Physics2D.Linecast(transform.position, groundcheckL.position, 1 << LayerMask.NameToLayer("Water")) && Physics2D.Linecast(transform.position, groundcheckR.position, 1 << LayerMask.NameToLayer("Water")))
        {
            can = false;
        }
        else
        {
            can = true;
        }
        return can;
    }
    private bool wals()
    {
        bool can;
        if (!wallR)
        {
            can = false;
        }
        else
        {
            can = true;
        }
        return can;
    }
    private IEnumerator ti()
    {
        isChecking = true;
        yield return new WaitForSeconds(1);
        tmer = true;
        isChecking = false;
    }
    private IEnumerator ti1()
    {
        isChecking1 = true;
        yield return new WaitForSeconds(1);
        tmer1 = true;
        isChecking1 = false;
    }
    private void atackStart()
    {
        s = Mathf.Abs(transform.position.x - charrecter.transform.position.x) + Mathf.Abs(transform.position.y - charrecter.transform.position.y);
        if (s < 10)
        {
            isAgred = true;
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
            if (s < 10 && tmer1)
            {
                tmer1 = false;
                if (!isChecking1)
                {
                    StartCoroutine(ti1());
                }
                StartCoroutine(atackEnd());
            }
            else if (s < 6)
            {
                speed = -3;
            }
            else
            {
                speed = 0;
            }

        }
        else
        {
            isAgred = false;
            speed = 5;
        }
    }
    private IEnumerator atackEnd()
    {
        s = Mathf.Abs(gunpoint.transform.position.x - charrecter.transform.position.x) + (charrecter.transform.position.y - transform.position.y) * 0.8f;
        isAttacking = true;
        pula1 = Instantiate(pula);
        k = pula1.GetComponent<kislota>();
        k.speed = 20;
        var dir = charrecter.transform.position -gunpoint.transform.position;
        var angle = Mathf.Atan2(dir.y + 0.5f * s, dir.x) * Mathf.Rad2Deg;
        pula1.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        pula1.transform.position =gunpoint.transform.position;
        yield return new WaitForSeconds(0.5f);
        isAttacking = false;
    }
}
