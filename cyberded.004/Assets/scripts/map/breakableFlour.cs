using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakableFlour : MonoBehaviour
{
    private ryvLetunII rii;
    public GameObject aliveSetter;
    public outoffScene outoff;
    public jumpingzombieII jii;
    void Start()
    {
        aliveSetter = GameObject.FindGameObjectWithTag("GameController");
        outoff = aliveSetter.GetComponent<outoffScene>();
        if (!outoff.WallONLevel3Alive)
        {
            StartCoroutine(death());
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            rii = collision.gameObject.GetComponent<ryvLetunII>();
            jii = collision.gameObject.GetComponent<jumpingzombieII>();
            if (rii!=null)
            {
                if (rii.isatacking1)
                {
                    outoff.WallONLevel3Alive = false;
                    StartCoroutine(death());
                }
            }
            else if (jii!=null)
            {
                if (jii.isjumping)
                {
                    outoff.WallONLevel3Alive = false;
                    StartCoroutine(death());
                }
            }
        }
    }
    IEnumerator death()
    {
            yield return new WaitForSeconds(0.1f);
            Destroy(this.gameObject);
    }
}
