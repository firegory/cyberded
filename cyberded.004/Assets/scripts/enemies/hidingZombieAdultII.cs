using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hidingZombieAdultII : MonoBehaviour
{
    public GameObject zombie;
    public zombieII zii;
    public GameObject hideobject;
    public reactivething hii;
    public GameObject charrecter;
    public float s;
    public float tm;
    void Start()
    {
        zii = zombie.GetComponent<zombieII>();
        hii = hideobject.GetComponent<reactivething>();
        charrecter = zii.charrecter;
    }

    // Update is called once per frame
    void Update()
    {
        if (zii.alive)
        {
            tm += Time.deltaTime;
            hii.transform.position = new Vector2(zombie.transform.position.x,zombie.transform.position.y-0.15f);
            if (hii.alive)
            {
                if (s<7)
                {
                    tm = 0;
                }
                s = Mathf.Abs(zombie.transform.position.x - charrecter.transform.position.x) + Mathf.Abs(zombie.transform.position.y - charrecter.transform.position.y);
                if (s<5)
                {
                    hideobject.SetActive(false);
                    zombie.SetActive(true);
                }
                else if(!zii.isAgred&&!zii.canWalk()&&tm>3)
                {
                    hideobject.SetActive(true);
                    zombie.SetActive(false);
                }
            }
            else
            {
                if (!zombie.activeSelf)
                {
                    StartCoroutine(zombieactivate());
                }
            }
        }
        else
        {
            StartCoroutine(death());
        }
    }
    private IEnumerator death()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }
    private IEnumerator zombieactivate()
    {
        yield return new WaitForSeconds(0.1f);
        zombie.SetActive(true);
    }
}
