using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killingwater : MonoBehaviour
{
    // Start is called before the first frame update
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
            if (plauer !=null)
            {
                plauer.Hurt( 1000);
            }
        }
        else if (collision.gameObject.CompareTag("enemy"))
        {
            reactivetarget reactivetarget = collision.gameObject.GetComponent<reactivetarget>();
            jabaII jii = collision.gameObject.GetComponent<jabaII>();
            if (reactivetarget!=null && jii == null)
            {
                reactivetarget.Hurt(101);
            }
        }
    }
}
