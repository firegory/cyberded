using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gun : MonoBehaviour
{
    // Start is called before the first frame update
    public move3 move3;
    public GameObject cyberded;
    private bool isChecking = false;
    private bool tmer = true;
    private bool isChecking1 = false;
    private bool tmer1 = true;
    public GameObject pula;
    public GameObject shoutGunPula;
    private GameObject pula1;
    public SpriteRenderer spriteRenderer;
    public bool canShoot;
    public string gunTipe;
    public GameObject hpsetter;
    public outoffScene outoff;
    private int x;
    public Animator anim;
    void Start()
    {
        move3 = GetComponent<move3>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        hpsetter = GameObject.FindGameObjectWithTag("GameController");
        outoff = hpsetter.GetComponent<outoffScene>();
        gunTipe = outoff.typeGun;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gunTipe == "SimpleGun")
        {
            anim.SetInteger("guntype", 0);
        }
        else if (gunTipe == "Shotgun")
        {
            anim.SetInteger("guntype", 1);
        }
        if (Input.GetMouseButton(0)&&canShoot)
        {
            var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            if (transform.rotation.z<=-0.8f || transform.rotation.z >=0.8f)
            {
                spriteRenderer.flipY = true;
            }
            else if(transform.rotation.z >= -0.7f && transform.rotation.z <= 0.7f)
            {
                spriteRenderer.flipY = false;
            }
            if (gunTipe == "SimpleGun")
            {
                if (tmer)
                {
                    tmer = false;
                    if (!isChecking)
                    {
                        StartCoroutine(ti());
                    }
                    pula1 = Instantiate(pula);
                    pula1.transform.position = transform.position;
                    pula1.transform.rotation = transform.rotation;
                }
            }
            else if (gunTipe == "Shotgun")
            {
                if (tmer1)
                {
                    tmer1 = false;
                    if (!isChecking1)
                    {
                        StartCoroutine(ti1());
                    }
                    x = -16;
                    for (int i = 0; i < 9; i++)
                    {
                        x += 4;
                        pula1 = Instantiate(shoutGunPula);
                        pula1.transform.position = transform.position;
                        pula1.transform.rotation = transform.rotation;
                        pula1.transform.Rotate(0, 0, x, Space.Self);
                    }
                }
            }

        }
    }
    private IEnumerator ti()
    {
        isChecking = true;
        yield return new WaitForSeconds(0.2f);
        tmer = true;
        isChecking = false;
    }
    private IEnumerator ti1()
    {
        isChecking1 = true;
        yield return new WaitForSeconds(0.5f);
        tmer1 = true;
        isChecking1 = false;
    }
}
