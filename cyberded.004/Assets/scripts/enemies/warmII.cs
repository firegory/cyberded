using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class warmII : MonoBehaviour
{
    public float health;
    public float reload;
    public float damage;
    public bool isAtacking;
    public bool hasAtacked;
    private bool alive;
    public float tm;
    public GameObject charrecter;
    public bool hasToteleport;
    public bool hastomove;
    public GameObject wave;
    private GameObject wave1;
    private GameObject wave2;
    private float  x;
    public Transform point1;
    public Transform point2;
    public float speed;
    public bool isStarting;
    public bool isdieing;
    public Slider slider;
    public GameObject cube;
    public GameObject deadWorm;
    public GameObject aliveSetter;
    public outoffScene outoff;
    public move3 move;
    public GameObject pushka;
    public gun gun;
    void Start()
    {
        gun = pushka.GetComponent<gun>();
        move = charrecter.GetComponent<move3>();
        aliveSetter = GameObject.FindGameObjectWithTag("GameController");
        outoff = aliveSetter.GetComponent<outoffScene>();
        if (outoff.aliveWarm)
        {
            cube.SetActive(false);
            speed = 15;
            alive = true;
            health = 750;
            StartCoroutine(startt());
        }
        else
        {
            cube.SetActive(false);
            deadWorm.SetActive(true);
            Destroy(this.gameObject);
            Destroy(slider.gameObject);
        }
    }

    void Update()
    {
        slider.value = health / 750;
        if (!isStarting && !charrecter.GetComponent<hp>().ispaused)
        {
            tm += Time.deltaTime;
            if (alive)
            {
                if (health <0&&!isAtacking)
                {
                    alive = false;
                    StartCoroutine(death());
                }
                if (!isAtacking)
                {
                    if (tm > 1)
                    {
                        x = Random.Range(1, 4);
                        if (x == 1)
                        {
                            StartCoroutine(atack1());
                        }
                        else if (x == 2)
                        {
                            StartCoroutine(atack2());
                        }
                        else if (x == 3)
                        {
                            StartCoroutine(atack3());
                        }
                    }
                }
                else
                {
                    tm = 0;
                }
                if (hastomove)
                {
                    transform.Translate(Vector3.right * Time.deltaTime * speed);
                }

            }
            else
            {
                if (hastomove)
                {
                    transform.Translate(Vector3.right * Time.deltaTime * speed);
                }
            }
        }
        else
        {
            if (hastomove && !charrecter.GetComponent<hp>().ispaused)
            {
                transform.Translate(Vector3.right * Time.deltaTime * speed);
            }
        }
    }
    private IEnumerator atack1()
    {
        speed = 20;
        isAtacking = true;
        hasAtacked = false;
        damage = 10;
        yield return new WaitForSeconds(0.2f);
        if (charrecter.transform.position.x<0)
        {
            hastomove = true;
            transform.Rotate(0, 0, (180 - transform.rotation.eulerAngles.z), Space.Self);
            transform.position =new Vector2 (12,3.2f);
            hasToteleport = true;
            yield return new WaitForSeconds(0.1f);
            hasToteleport = false;
        }
        else
        {
            hastomove = true;
            transform.Rotate(0, 0, - transform.rotation.eulerAngles.z, Space.Self);
            transform.position = new Vector2(-12, 3.2f);
            hasToteleport = true;
            yield return new WaitForSeconds(0.1f);
            hasToteleport = false;
        }
        yield return new WaitForSeconds(3);
        isAtacking = false;
        hastomove = false;
    }
    private IEnumerator atack2()
    {
        speed = 15;
        isAtacking = true;
        hasAtacked = false;
        damage = 10;
        yield return new WaitForSeconds(0.2f);
        hastomove = true;
        transform.position = new Vector2(charrecter.transform.position.x, 13);
        transform.Rotate(0, 0,-90 -transform.rotation.eulerAngles.z, Space.Self);
        hasToteleport = true;
        yield return new WaitForSeconds(0.1f);
        hasToteleport = false;
        yield return new WaitForSeconds(1.5f);
        transform.position = new Vector2(charrecter.transform.position.x, -3);
        hasAtacked = false;
        transform.Rotate(0, 0, 90 - transform.rotation.eulerAngles.z, Space.Self);
        hasToteleport = true;
        yield return new WaitForSeconds(0.1f);
        hasToteleport = false;
        yield return new WaitForSeconds(1.5f);
        transform.position = new Vector2(charrecter.transform.position.x, 13);
        hasAtacked = false;
        transform.Rotate(0, 0, -90 - transform.rotation.eulerAngles.z, Space.Self);
        hasToteleport = true;
        yield return new WaitForSeconds(0.1f);
        hasToteleport = false;
        yield return new WaitForSeconds(2);
        isAtacking = false;
        hastomove = false;
    }
    private IEnumerator atack3()
    {
        speed = 10;
        isAtacking = true;
        hasAtacked = false;
        damage = 10;
        hasToteleport = true;
        yield return new WaitForSeconds(0.2f);
        transform.position = new Vector2(charrecter.transform.position.x, 13);
        transform.Rotate(0, 0, -90 - transform.rotation.eulerAngles.z, Space.Self);
        yield return new WaitForSeconds(0.1f);
        hasToteleport = false;
        hastomove = true;
        yield return new WaitForSeconds(0.9f);
        wave1 = Instantiate(wave);
        wave1.transform.position = new Vector2(transform.position.x, 3);
        wave1.transform.rotation = point1.transform.rotation;
        wave2 = Instantiate(wave);
        wave2.transform.position = new Vector2(transform.position.x, 3);
        wave2.transform.rotation = point1.transform.rotation;
        wave2.GetComponent<kislota>().speed = -8;
        wave2.GetComponent<SpriteRenderer>().flipX = true;
        yield return new WaitForSeconds(2);
        isAtacking = false;
        hastomove = false;

    }
    private void OnTriggerEnter2D(Collider2D collission)
    {
        if (collission.CompareTag("pula")&&!isStarting)
        {
            pula pula1 = collission.GetComponent<pula>();
            if (pula1 != null)
            {
                health -= pula1.damage;
                Destroy(collission.gameObject);

            }
        }
        if (isAtacking && !hasAtacked)
        {
            if (collission.gameObject.CompareTag("Player"))
            {
                plauerCharrecter plauer = collission.gameObject.GetComponent<plauerCharrecter>();
                if (plauer != null)
                {
                    plauer.Hurt(damage);
                    hasAtacked = true;
                }
            }
        }
    }
    private IEnumerator startt()
    {
        outoff.canBePaused = false;
        isStarting = true;
        hastomove = false;
        transform.position = new Vector2(-10, 0);
        hasToteleport = true;
        yield return new WaitForSeconds(1);
        cube.SetActive(true);
        hasToteleport = false;
        hastomove = true;
        transform.Rotate(0, 0, (90 - transform.rotation.eulerAngles.z), Space.Self);
        for (int i = 0; i < 45; i++)
        {
            transform.Rotate(0, 0, -2, Space.Self);
            yield return new WaitForSeconds(0.01f);
        }
        move.canMove = false;
        gun.canShoot = false;
        hastomove = false;
        yield return new WaitForSeconds(2);
        hastomove = true;
        move.canMove = true;
        gun.canShoot = true;
        for (int i = 0; i < 45; i++)
        {
            transform.Rotate(0, 0, -2, Space.Self);
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(1);
        hastomove = false;
        isStarting = false;
        outoff.canBePaused = true;
    }
    private IEnumerator death()
    {
        outoff.canBePaused = false;
        hastomove = false;
        speed = 15;
        isAtacking = false;
        transform.position = new Vector2(0, -5);
        transform.Rotate(0, 0, (90 - transform.rotation.eulerAngles.z), Space.Self);
        hasToteleport = true;
        yield return new WaitForSeconds(1);
        hasToteleport = false;
        hastomove = true;
        yield return new WaitForSeconds(0.7f);
        hastomove = false;
        isdieing = true;
        move.canMove = false;
        gun.canShoot = false;
        if (charrecter.transform.position.x<=1f&& charrecter.transform.position.x > -3)
        {
            x = -1*(3+charrecter.transform.position.x)/65-0.01f;
        }
        if (charrecter.transform.position.x>1f&&charrecter.transform.position.x<5)
        {
            x =(5 -charrecter.transform.position.x)/65+0.01f;
        }
        for (int i = 0; i < 20; i++)
        {
            charrecter.transform.position = new Vector2(charrecter.transform.position.x + x, charrecter.transform.position.y);
            transform.Translate(Vector3.up*0.1f);
            yield return new WaitForSeconds(0.01f);
            transform.Translate(Vector3.down * 0.1f);
            yield return new WaitForSeconds(0.01f);
            transform.Translate(Vector3.left * 0.1f);
            yield return new WaitForSeconds(0.01f);
            transform.Translate(Vector3.right * 0.1f);
            yield return new WaitForSeconds(0.01f);
        }
        for (int i = 0; i < 45; i++)
        {
            charrecter.transform.position = new Vector2(charrecter.transform.position.x + x, charrecter.transform.position.y);
            transform.position = new Vector2(transform.position.x + 0.03f, transform.position.y + 0.0213f);
            transform.Rotate(0, 0, 0, Space.Self);
            yield return new WaitForSeconds(0.01f);
        }
        outoff.aliveWarm = false;
        gun.canShoot = true;
        deadWorm.SetActive(true);
        cube.SetActive(false);
        move.canMove = true;
        Destroy(slider.gameObject);
        Destroy(this.gameObject);
        outoff.canBePaused = true;
    }
}
