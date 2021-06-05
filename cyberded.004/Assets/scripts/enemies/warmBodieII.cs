using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class warmBodieII : MonoBehaviour
{
    public GameObject warmHead;
    public warmII warm;
    public GameObject lastPart;
    public float s;
    public bool hasdone;
    public float gox;
    public float goy;
    public float rot;
    public int x;
    public GameObject aliveSetter;
    public outoffScene outoff;
    public GameObject charrecter;
    void Start()
    {
        aliveSetter = GameObject.FindGameObjectWithTag("GameController");
        outoff = aliveSetter.GetComponent<outoffScene>();
        warm = warmHead.GetComponent<warmII>();
        if (outoff.aliveWarm)
        {
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    void Update()
    {
        if (!warm.isdieing && !charrecter.GetComponent<hp>().ispaused)
        {
            var dir = lastPart.transform.position - transform.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            s = Mathf.Abs(transform.position.x - lastPart.transform.position.x) + Mathf.Abs(transform.position.y - lastPart.transform.position.y);
            if (warm.hasToteleport)
            {
                transform.position = warmHead.transform.position;
            }
            else if (s > 2 && warm.hastomove)
            {
                transform.Translate(Vector3.right * Time.deltaTime * warm.speed);
            }
        }
        else if (!hasdone&&warm.isdieing)
        {
            hasdone = true;
            StartCoroutine(death());
        }
    }
    private void OnTriggerEnter2D(Collider2D collission)
    {
        if (collission.CompareTag("pula")&&!warm.isStarting)
        {
            pula pula1 = collission.GetComponent<pula>();
            if (pula1 != null)
            {
                warm.health -= pula1.damage;
                Destroy(collission.gameObject);

            }
        }
        if (warm.isAtacking && !warm.hasAtacked)
        {
            if (collission.gameObject.CompareTag("Player"))
            {
                plauerCharrecter plauer = collission.gameObject.GetComponent<plauerCharrecter>();
                if (plauer != null)
                {
                    plauer.Hurt(warm.damage);
                    warm.hasAtacked = true;
                }
            }
        }
    }
    private IEnumerator death()
    {
        for (int i = 0; i < 20; i++)
        {
            transform.Translate(Vector3.up * 0.1f);
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
            transform.position = new Vector2(transform.position.x + gox, transform.position.y + goy);
            transform.Rotate(0, 0, rot, Space.Self);
            yield return new WaitForSeconds(0.01f);
        }
        Destroy(this.gameObject);
    }

}
