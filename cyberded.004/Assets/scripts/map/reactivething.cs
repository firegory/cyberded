using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reactivething : MonoBehaviour
{
    // Start is called before the first frame update
    public float hp;
    public bool alive;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0)
        {
            StartCoroutine(death());
        }
    }
    private void OnTriggerEnter2D(Collider2D collission)
    { 
        if (collission.gameObject.CompareTag("pula"))
        {
            pula pula1 = collission.GetComponent<pula>();
            if (pula1 != null)
            {
                hp -= pula1.damage;
                Destroy(collission.gameObject);

            }
        }
    }
    public void Hurt(float damag)
    {
        hp -= damag;
    }
    IEnumerator death()
    {
        if (alive)
        {
            yield return new WaitForSeconds(0.1f);
            Destroy(this.gameObject);
        }
    }
}
