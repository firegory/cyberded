using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class atack : MonoBehaviour
{
    public float damage;
    public bool hasAtacked;
    public zombieII zii;
    public GameObject zombie;
    void Start()
    {
        zii = zombie.GetComponent<zombieII>();
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (zii.isAttacking && !hasAtacked)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                plauerCharrecter plauer = collision.gameObject.GetComponent<plauerCharrecter>();
                if (plauer != null)
                {
                    plauer.Hurt(damage);
                    hasAtacked = true;
                }
            }
        }
    }
}
