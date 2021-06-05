using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpingzombieII : MonoBehaviour
{
    public GameObject wallcheckR;
    private iswall isR;
    public bool wallR;
    public bool canwalk= true;
    public int speed = 3;
    public float damage = 10.0f;
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
    public bool isjumping;
    public bool alive = true;
    public bool hasAttacked;
    public bool isgrounded;
    public Transform groundcheckR;
    public Transform groundcheckL;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isR = wallcheckR.GetComponent<iswall>();
        canwalk = true;
    }

    // Update is called once per frame
    void Update()
    {
        wallR = isR.wall;
        if (alive && !charrecter.GetComponent<hp>().ispaused)
        {
            if (!isAttacking&&!isjumping&&canwalk)
            {
                atackStart();
                if (wals()  ||(!Physics2D.Linecast(transform.position, groundcheckR.position, 1 << LayerMask.NameToLayer("Ground")) && !Physics2D.Linecast(transform.position, groundcheckR.position, 1 << LayerMask.NameToLayer("flyingGround"))) && !isAgred)
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
                    tmer = false;
                    if (!isChecking)
                    {
                        StartCoroutine(ti());
                    }
                }
                else
                {
                    rb.velocity = new Vector2(speed * rot, rb.velocity.y);
                }
            }
            else
            {
                speed = 5;
                if (isAttacking)
                {
                    rb.velocity = new Vector2(0, rb.velocity.y);
                }
                if (isjumping&&isgrounded)
                {
                    StartCoroutine(end());
                }
            }
            if (Physics2D.Linecast(transform.position, groundcheckL.position, 1 << LayerMask.NameToLayer("Ground")) || Physics2D.Linecast(transform.position, groundcheckR.position, 1 << LayerMask.NameToLayer("Ground"))|| Physics2D.Linecast(transform.position, groundcheckL.position, 1 << LayerMask.NameToLayer("flyingGround")) || Physics2D.Linecast(transform.position, groundcheckR.position, 1 << LayerMask.NameToLayer("flyingGround")))
            {
                isgrounded = true;
            }
            else
            {
                isgrounded = false;
            }
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
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
    private IEnumerator ti1()
    {
        isChecking1 = true;
        yield return new WaitForSeconds(2.0f);
        tmer1 = true;
        isChecking1 = false;
    }
    private void atackStart()
    {
        s = Mathf.Abs(transform.position.x - charrecter.transform.position.x) + Mathf.Abs(transform.position.y - charrecter.transform.position.y);
        if (Mathf.Abs(transform.position.x - charrecter.transform.position.x) <= 2f && Mathf.Abs(transform.position.y - charrecter.transform.position.y) >= 0.7f)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            isAgred = true;
            speed = 5;
        }
        else if (s < 4 && tmer1)
        {
            tmer1 = false;
            if (!isChecking1)
            {
                StartCoroutine(ti1());
            }
            StartCoroutine(atackEnd());

        }
        else if (s < 7 && !isAttacking)
        {
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
            speed = 5;
            isAgred = true;
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
        hasAttacked = false;
        speed = 5;
        canwalk = false;
        yield return new WaitForSeconds(0.5f);
        speed = 5;
        isAttacking = false;
        rb.velocity = new Vector2((transform.position.x - charrecter.transform.position.x)*-1, 8);
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
        yield return new WaitForSeconds(0.03f);
        isjumping = true;
    }
    private IEnumerator end()
    {
        yield return new WaitForSeconds(0.1f);
        isjumping = false;
        canwalk = true;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (isjumping && !hasAttacked)
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
