using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class reactivetarget : MonoBehaviour
{
    // Start is called before the first frame update
    public float hp = 100;
    public zombieII ii;
    public shootingZombieII sii;
    public bool alive = true;
    public kislZombieII kii;
    public ryvzombieII rii;
    public jabaII jii;
    public letunII lii;
    public ryvLetunII rlii;
    public jumpingzombieII jzii;
    public fastshootingzombieII fsii;
    public float health;
    public Slider slider;
    void Start()
    {
        ii = GetComponent<zombieII>();
        sii = GetComponent<shootingZombieII>();
        kii = GetComponent<kislZombieII>();
        rii = GetComponent<ryvzombieII>();
        jii = GetComponent<jabaII>();
        lii = GetComponent<letunII>();
        rlii = GetComponent<ryvLetunII>();
        jzii = GetComponent<jumpingzombieII>();
        fsii = GetComponent<fastshootingzombieII>();
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = hp / health;
        if (hp <= 0)
        {
            StartCoroutine(death());
        }
    }
    private void OnTriggerEnter2D(Collider2D collission)
    {
        if (collission.CompareTag("pula"))
        {
            pula pula1 = collission.GetComponent<pula>();
            if (pula1!=null)
            {
                hp -= pula1.damage;
                Destroy(collission.gameObject);
               
            }
        }
    }
    public void Hurt(float damag)
    {
        hp -=damag;
    }
    IEnumerator death()
    {
        if (alive)
        {
            alive = false;
            if (ii != null)
            {
                ii.alive = false;
            }
            else if (sii != null)
            {
                sii.alive = false;
            }
            else if (kii != null)
            {
                kii.alive = false;
            }
            else if (rii != null)
            {
                rii.alive = false;
            }
            else if (jii!=null)
            {
                jii.alive = false;
            }
            else if (lii != null)
            {
                lii.isalive = false;
            }
            else if (rlii != null)
            {
                rlii.isalive = false;
            }
            else if (jzii !=null)
            {
                jzii.alive = false;
            }
            else if (fsii != null)
            {
                fsii.alive = false;
            }
            transform.Rotate(0, 0, -45, Space.Self);
            yield return new WaitForSeconds(0.5f);
            Destroy(this.gameObject);
        }
    }
}
