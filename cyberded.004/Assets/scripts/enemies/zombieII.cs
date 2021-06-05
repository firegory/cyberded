using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombieII : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform groundcheckL;
    public Transform groundcheckR;
    public GameObject wallcheckR;
    private iswall isR;
    public bool wallR;
    public float speed1;
    public float speed2;
    public float speed = 3;
    public float damage = 20.0f;
    public int rot = -1;
    public bool isChecking = false;
    public bool tmer = true;
    public GameObject charrecter;
    Rigidbody2D rb;
    public float s;
    public bool isAgred;
    public bool isAttacking;
    public bool alive = true;
    public atack at;
    public GameObject atac;
    public float atackDistance;
    public float center;
    public float atackTime;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isR = wallcheckR.GetComponent<iswall>();
        at = atac.GetComponent<atack>();
    }

    // Update is called once per frame
    void Update()
    {
        wallR = isR.wall;
        if (alive&&!charrecter.GetComponent<hp>().ispaused)
        {
            if (!isAttacking)
            {
                atackStart();
                if (Physics2D.Linecast(transform.position, groundcheckL.position, 1 << LayerMask.NameToLayer("Ground")) || Physics2D.Linecast(transform.position, groundcheckR.position, 1 << LayerMask.NameToLayer("Ground")) || Physics2D.Linecast(transform.position, groundcheckL.position, 1 << LayerMask.NameToLayer("flyingGround")) || Physics2D.Linecast(transform.position, groundcheckR.position, 1 << LayerMask.NameToLayer("flyingGround")))
                {
                    rb.velocity = new Vector2(rb.velocity.x, 0);
                }
                else
                {
                    rb.velocity = new Vector2(rb.velocity.x, -5);
                }
                if (canWalk() || wals() && !isAgred)
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
                if (isAgred && wallR)
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
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
            if (Physics2D.Linecast(transform.position, groundcheckL.position, 1 << LayerMask.NameToLayer("Ground")) || Physics2D.Linecast(transform.position, groundcheckR.position, 1 << LayerMask.NameToLayer("Ground")) || Physics2D.Linecast(transform.position, groundcheckL.position, 1 << LayerMask.NameToLayer("flyingGround")) || Physics2D.Linecast(transform.position, groundcheckR.position, 1 << LayerMask.NameToLayer("flyingGround")))
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
            }
            else
            {
                rb.velocity = new Vector2(rb.velocity.x, -5);
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
        if ((Physics2D.Linecast(transform.position, groundcheckL.position, 1 << LayerMask.NameToLayer("Ground")) || Physics2D.Linecast(transform.position, groundcheckL.position, 1 << LayerMask.NameToLayer("flyingGround"))) && (Physics2D.Linecast(transform.position, groundcheckR.position, 1 << LayerMask.NameToLayer("Ground")) || Physics2D.Linecast(transform.position, groundcheckR.position, 1 << LayerMask.NameToLayer("flyingGround"))))
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
        yield return new WaitForSeconds(0.5f);
        tmer = true;
        isChecking = false;
    }
    private void atackStart()
    {
        s = Mathf.Abs(transform.position.x-charrecter.transform.position.x)+ Mathf.Abs(transform.position.y - charrecter.transform.position.y);
        if (s<atackDistance)
        {
            StartCoroutine(atackEnd());
        }
        else if (Mathf.Abs(transform.position.x - charrecter.transform.position.x) <= 0.5f)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            isAgred = true;
            speed = speed1;
        }
        else if (s < 7)
        {
            if (transform.position.x - charrecter.transform.position.x+ center*rot >= 0)
            {
                rot = -1;
                transform.rotation = new Quaternion(transform.rotation.x, 0, transform.rotation.z, transform.rotation.w);
            }
            else
            {
                rot = 1;
                transform.rotation = new Quaternion(transform.rotation.x, 180, transform.rotation.z, transform.rotation.w);
            }
            speed = speed1;
            isAgred = true;
        }
        else
        {
            isAgred = false;
            speed = speed2;
        }
    }
    private IEnumerator atackEnd()
    {
        isAttacking = true;
        at.hasAtacked = false;
        if (transform.position.x - charrecter.transform.position.x +center*rot>= 0)
        {
            rot = -1;
            transform.rotation = new Quaternion(transform.rotation.x, 0, transform.rotation.z, transform.rotation.w);
        }
        else
        {
            rot = 1;
            transform.rotation = new Quaternion(transform.rotation.x, 180, transform.rotation.z, transform.rotation.w);
        }
        yield return new WaitForSeconds(atackTime);
        isAttacking = false;
    }
}
