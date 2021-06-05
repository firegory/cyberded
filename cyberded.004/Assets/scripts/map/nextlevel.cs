using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class nextlevel : MonoBehaviour
{
    public float health;
    public GameObject charrecter;
    public string scen;
    public GameObject outoffScenes;
    public outoffScene outoff;
    public Vector3 posit;
    void Start()
    {
        outoffScenes = GameObject.FindGameObjectWithTag("GameController");
        outoff = outoffScenes.GetComponent<outoffScene>();
    }

    // Update is called once per frame
    void Update()
    {
        health = charrecter.GetComponent<plauerCharrecter>().health;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            outoff.hp = health;
            outoff.pos = posit;
            SceneManager.LoadScene(scen);
        }
    }
}
