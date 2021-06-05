using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kislZombieII : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform groundcheckL;
    public Transform groundcheckR;
    public GameObject wallcheckL;
    public GameObject wallcheckR;
    private iswall isL;
    private iswall isR;
    public bool wallR;
    public bool wallL;
    public int speed = 1;
    public float damage = 0.3f;
    public int rot = -1;
    public bool isChecking = false;
    public bool tmer = true;
    public GameObject charrecter;
    Rigidbody2D rb;
    public float s;
    public bool isAgred;
    public bool isAttacking;
    public bool alive = true;
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
            s = Mathf.Abs(transform.position.x - charrecter.transform.position.x) + Mathf.Abs(transform.position.y - charrecter.transform.position.y);
            if (Physics2D.Linecast(transform.position, groundcheckL.position, 1 << LayerMask.NameToLayer("Ground")) || Physics2D.Linecast(transform.position, groundcheckR.position, 1 << LayerMask.NameToLayer("Ground"))||Physics2D.Linecast(transform.position, groundcheckL.position, 1 << LayerMask.NameToLayer("flyingGround")) || Physics2D.Linecast(transform.position, groundcheckR.position, 1 << LayerMask.NameToLayer("flyingGround")))
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
            rb.velocity = new Vector2(speed * rot, rb.velocity.y);
            if (s<3)
            {
                plauerCharrecter plauer = charrecter.GetComponent<plauerCharrecter>();
                if (plauer != null)
                {
                    plauer.Hurt(damage*Time.deltaTime);
                }
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
        if ((Physics2D.Linecast(transform.position, groundcheckL.position, 1 << LayerMask.NameToLayer("Ground")) || Physics2D.Linecast(transform.position, groundcheckL.position, 1 << LayerMask.NameToLayer("flyingGround"))) && (Physics2D.Linecast(transform.position, groundcheckR.position, 1 << LayerMask.NameToLayer("Ground"))|| Physics2D.Linecast(transform.position, groundcheckR.position, 1 << LayerMask.NameToLayer("flyingGround"))))
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
        if (!wallL &&!wallR)
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
   
}
