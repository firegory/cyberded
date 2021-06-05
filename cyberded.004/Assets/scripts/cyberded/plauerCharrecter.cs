using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class plauerCharrecter : MonoBehaviour
{
    public float health;
    public bool isAlive;
    public GameObject hpsetter;
    public outoffScene outoff;
    // Start is called before the first frame update
    void Start()
    {
        isAlive = true;
        hpsetter = GameObject.FindGameObjectWithTag("GameController");
        outoff = hpsetter.GetComponent<outoffScene>();
        if (outoff.hp!=0)
        {
            health = outoff.hp;
        }
        if (outoff.pos != new Vector3(0,0,0))
        {
            transform.position = outoff.pos;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            if (Input.GetKey(KeyCode.Q))
            {
                health += 10;
            }
            if (health <= 0)
            {
                StartCoroutine(Death());
                isAlive = false;
            }
        }
    }
    public void Hurt(float damage)
    {
        health -= damage;
    }
    private IEnumerator Death()
    {
        transform.Rotate(0, 0, 45, Space.Self);
        yield return new WaitForSeconds(0.3f);
        health = 100;
        isAlive = true;
        outoff.pos = new Vector3(-17.33f, 13.92f,-2);
        outoff.hp = 100;
        SceneManager.LoadScene("SampleScene");
    }
    void setHealth(float hp)
    {
        health = hp;
        print(hp);
    }
}
