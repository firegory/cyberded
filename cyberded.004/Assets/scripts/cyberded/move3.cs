using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move3 : MonoBehaviour
{
    // Start is called before the first frame update
    public int speed = 10;
    public int jump = 5;
    public bool tmer = true;
    public bool isChecking = false;
    public bool canMove = true;
    public bool isGrounded = false;
    public bool isjumping;
    public bool issittinng;
    public int rot;
    public GameObject gun;
    Rigidbody2D rb;
    public GameObject wallcheckR;
    private iswall isR;
    public bool wallR;
    public GameObject wallcheckL;
    private iswall isL;
    public bool wallL;
    public GameObject groundcheck;
    public isground isg;
    public PlatformEffector2D platformEffector;
    public GameObject groundTarget;
    public bool isGowingUp;

    void Start()
    {
        isR = wallcheckR.GetComponent<iswall>();
        isL = wallcheckL.GetComponent<iswall>();
        isg = groundcheck.GetComponent<isground>();
        rb = GetComponent<Rigidbody2D>();
        platformEffector =groundTarget.GetComponent<PlatformEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
        wallR = isR.wall;
        wallL = isL.wall;
        rb.velocity = new Vector2(0, rb.velocity.y);
        if (!isGowingUp&&!GetComponent<hp>().ispaused)
        {
            rb.gravityScale = 2;
        }
        if (canMove && !GetComponent<hp>().ispaused && !GetComponent<hp>().isInMenu)
        {
            if (!Input.GetKeyUp(KeyCode.S))
            {
                platformEffector.useColliderMask = false;
            }
            if (isg.grounded)
            {
                    isGrounded = true;
            }
            else
            {
                isGrounded = false;
            }
            if (Input.GetKey(KeyCode.S)&& isg.grounded&&!isGowingUp)
            {
                issittinng = true;
            }
            else
            {
                issittinng = false;

            }
            if (!issittinng && !isGowingUp)
            {

                if (Input.GetKey(KeyCode.A))
                {
                    if ((Mathf.Round(transform.rotation.y) != 1 && !wallR && Mathf.Round(transform.rotation.y) != -1) || (Mathf.Round(transform.rotation.y) != 0 && !wallL))
                    {
                        rb.velocity = new Vector2(-speed, rb.velocity.y);
                        rot = -1;
                        if (Mathf.Round(transform.rotation.y) != 1 && Mathf.Round(transform.rotation.y) != -1 && !Input.GetMouseButton(0))
                        {
                            transform.Rotate(0, 180, 0, Space.Self);
                        }
                    }
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    if ((Mathf.Round(transform.rotation.y) != 0 && !wallR) || (Mathf.Round(transform.rotation.y) != 1 && !wallL && !wallL && Mathf.Round(transform.rotation.y) != -1))
                    {
                        rb.velocity = new Vector2(speed, rb.velocity.y);
                        rot = 1;
                        if (Mathf.Round(transform.rotation.y) != 0 && !Input.GetMouseButton(0))
                        {
                            transform.Rotate(0, 180, 0, Space.Self);
                        }
                    }
                }
            }
            if (Input.GetKey(KeyCode.Space)&& isg.grounded)
            {
                if (issittinng)
                {
                    platformEffector.useColliderMask = true;
                }
                else
                {
                    isjumping = true;
                    isGowingUp = false;
                    rb.gravityScale = 2;
                    rb.velocity = new Vector2(rb.velocity.x, jump);
                    StartCoroutine(jumpwait());
                }
            }
            else if (Input.GetKey(KeyCode.Space) &&isGowingUp&&!isjumping)
            {
                isjumping = true;
                isGowingUp = false;
                rb.gravityScale = 2;
                rb.velocity = new Vector2(rb.velocity.x, jump/3);
                StartCoroutine(jumpwait());
            }
            if (Input.GetMouseButton(0))
            {
                if (gun.transform.rotation.z <= -0.8f || gun.transform.rotation.z >= 0.8f)
                {
                    transform.rotation = new Quaternion(gun.transform.rotation.x, 180, 0, gun.transform.rotation.w);
                }
                else if (gun.transform.rotation.z >= -0.7f && gun.transform.rotation.z <= 0.7f)
                {
                    transform.rotation = new Quaternion(gun.transform.rotation.x, 0, 0, gun.transform.rotation.w);
                }
            }
        }
        else if (GetComponent<hp>().ispaused)
        {
            rb.velocity = new Vector2(0, 0);
            rb.gravityScale = 0;
        }
    }
    IEnumerator jumpwait()
    {
        isjumping = true;
        yield return new WaitForSeconds(0.2f);
        isjumping = false;
    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 12)
        {
            if (!isjumping&&canMove)
            {
                if (Input.GetKey(KeyCode.W))
                {
                    transform.position = new Vector2(collision.gameObject.transform.position.x, transform.position.y);
                    isGowingUp = true;
                    rb.gravityScale = 0;
                    rb.velocity =new Vector2 (0, 90 * Time.deltaTime);
                }
                else if (Input.GetKey(KeyCode.S)&&!isg.grounded)
                {
                    transform.position = new Vector2(collision.gameObject.transform.position.x, transform.position.y);
                    isGowingUp = true;
                    rb.gravityScale = 0;
                    rb.velocity = new Vector2(0, -90 * Time.deltaTime);
                }
                else if (!isg.grounded)
                {
                    transform.position = new Vector2(collision.gameObject.transform.position.x, transform.position.y);
                    isGowingUp = true;
                    rb.gravityScale = 0;
                    rb.velocity = new Vector2(rb.velocity.x, 0);
                }
                else
                {
                    rb.gravityScale = 2;
                    isGowingUp = false;
                }
            }
        }
        else
        {
            rb.gravityScale = 2;
        }
    }
}
