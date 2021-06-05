using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fastshootingzombieII : MonoBehaviour
{
    public Transform groundcheckL;
    public Transform groundcheckR;
    public GameObject wallcheckL;
    public GameObject wallcheckR;
    private iswall isL;
    private iswall isR;
    public int ammo;
    public bool wallR;
    public bool wallL;
    public int speed = 3;
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
    public bool reloading;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isR = wallcheckR.GetComponent<iswall>();
        isL = wallcheckL.GetComponent<iswall>();
    }

    // Update is called once per frame
    void Update()
    {
        if (alive && !charrecter.GetComponent<hp>().ispaused)
        {
            wallL = isL.wall;
            wallR = isR.wall;
            if (Physics2D.Linecast(transform.position, groundcheckL.position, 1 << LayerMask.NameToLayer("Ground")) || Physics2D.Linecast(transform.position, groundcheckR.position, 1 << LayerMask.NameToLayer("Ground")) || Physics2D.Linecast(transform.position, groundcheckL.position, 1 << LayerMask.NameToLayer("flyingGround")) || Physics2D.Linecast(transform.position, groundcheckR.position, 1 << LayerMask.NameToLayer("flyingGround")))
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
                if (canWalk())
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
                speed = 3;
                if (!Physics2D.Linecast(transform.position, groundcheckR.position, 1 << LayerMask.NameToLayer("Ground"))&& !Physics2D.Linecast(transform.position, groundcheckR.position, 1 << LayerMask.NameToLayer("flyingGround")))
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
        if ((Physics2D.Linecast(transform.position, groundcheckL.position, 1 << LayerMask.NameToLayer("Ground"))|| Physics2D.Linecast(transform.position, groundcheckL.position, 1 << LayerMask.NameToLayer("flyingGround"))) && (Physics2D.Linecast(transform.position, groundcheckR.position, 1 << LayerMask.NameToLayer("Ground"))|| Physics2D.Linecast(transform.position, groundcheckR.position, 1 << LayerMask.NameToLayer("flyingGround"))))
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
        if (!wallR && !wallL)
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
        yield return new WaitForSeconds(0.15f);
        tmer1 = true;
        isChecking1 = false;
    }
    private void atackStart()
    {
        s = Mathf.Abs(transform.position.x - charrecter.transform.position.x) + Mathf.Abs(transform.position.y - charrecter.transform.position.y);
        if (s < 9)
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
            if (s < 8 && tmer1)
            {
                if (ammo>0)
                {
                    tmer1 = false;
                    if (!isChecking1)
                    {
                        StartCoroutine(ti1());
                    }
                    StartCoroutine(atackEnd());
                    ammo--;
                }
                else if(!reloading)
                {
                    StartCoroutine(reload());
                    reloading = true;
                }
            }
            else if (s < 5)
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
            speed = 3;
        }
    }
    private IEnumerator atackEnd()
    {
        isAttacking = true;
        pula1 = Instantiate(pula);
        var dir = charrecter.transform.position - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        pula1.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        pula1.transform.position = transform.position;
        yield return new WaitForSeconds(0.1f);
        isAttacking = false;
    }
    private IEnumerator reload()
    {
        yield return new WaitForSeconds(1.5f);
        ammo = 6;
        reloading = false;
    }
}
