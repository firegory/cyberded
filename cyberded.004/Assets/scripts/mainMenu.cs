using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    public GameObject panel;
    public GameObject panelMap;
    public GameObject panelDed;
    public GameObject panelGuns;
    public GameObject charrecter;
    public move3 move;
    public GameObject pushka;
    public gun gun;
    public GameObject aliveSetter;
    public outoffScene outoff;
    void Start()
    {
    }
    public void StartGame()
    {
        aliveSetter = GameObject.FindGameObjectWithTag("GameController");
        outoff = aliveSetter.GetComponent<outoffScene>();
        outoff.pos = new Vector3(-21.21f, 13.92f, -2);
        SceneManager.LoadScene("SampleScene");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void resume()
    {
        gun = pushka.GetComponent<gun>();
        gun.canShoot = true;
        charrecter.GetComponent<hp>().ispaused = false;
        panel.gameObject.SetActive(false);
    }
    public void dedPanel()
    {
        panelMap.SetActive(false);
        panelDed.SetActive(true);
    }
    public void mapPanel()
    {
        panelMap.SetActive(true);
        panelDed.SetActive(false);
    }
    public void gunspanel()
    {
        panelGuns.SetActive(true);
        panelDed.SetActive(false);
    }
    public void setSimpleGun()
    {
        gun = pushka.GetComponent<gun>();
        gun.gunTipe = "SimpleGun";
        aliveSetter = GameObject.FindGameObjectWithTag("GameController");
        outoff = aliveSetter.GetComponent<outoffScene>();
        outoff.typeGun = "SimpleGun";
    }
    public void setShotgun()
    {
        aliveSetter = GameObject.FindGameObjectWithTag("GameController");
        outoff = aliveSetter.GetComponent<outoffScene>();
        outoff.typeGun = "Shotgun";
        gun = pushka.GetComponent<gun>();
        gun.gunTipe = "Shotgun";
    }
}
