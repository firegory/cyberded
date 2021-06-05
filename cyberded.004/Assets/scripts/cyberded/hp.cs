using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hp : MonoBehaviour
{
    // Start is called before the first frame update
    public Text hp1;
    public plauerCharrecter plauerCharrecter;
    public GameObject panel;
    public move3 move3;
    public GameObject pushka;
    public gun gun;
    public bool ispaused;
    public GameObject aliveSetter;
    public outoffScene outoff;
    public bool isInMenu;
    public GameObject panelMap;
    public GameObject panelDed;
    public GameObject panelGuns;
    void Start()
    {
        aliveSetter = GameObject.FindGameObjectWithTag("GameController");
        outoff = aliveSetter.GetComponent<outoffScene>();
        hp1 = GameObject.Find("Text").GetComponent<Text>();
        plauerCharrecter = GetComponent<plauerCharrecter>();
        gun = pushka.GetComponent<gun>();
    }

    // Update is called once per frame
    void Update()
    {
        hp1.text = Mathf.Ceil(plauerCharrecter.health).ToString();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (panelDed.activeSelf)
            {
                panelDed.SetActive(false);
                isInMenu = false;
                gun.canShoot = true;
            }
            else if (panelMap.activeSelf)
            {
                panelMap.SetActive(false);
                isInMenu = false;
                gun.canShoot = true;
            }
            else if (panelGuns.activeSelf)
            {
                panelGuns.SetActive(false);
                panelDed.SetActive(true);
            }
            else if (panel.activeSelf)
            {
                ispaused = false;
                panel.SetActive(false);
                gun.canShoot = true;
            }
            else if(outoff.canBePaused)
            {
                gun.canShoot = false;
                panel.SetActive(true);
                ispaused = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!panelDed.activeSelf&&!panelMap.activeSelf&&!panelGuns.activeSelf&&!panel.activeSelf)
            {
                panelMap.SetActive(true);
                isInMenu = true;
                gun.canShoot = false;
            }
            else
            {
                panelDed.SetActive(false);
                isInMenu = false;
                gun.canShoot = true;
                panelMap.SetActive(false);
                panelGuns.SetActive(false);
            }
        }
    }
}
