using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosion : MonoBehaviour
{
    public float damage;
    public bool hasAttacked;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
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
