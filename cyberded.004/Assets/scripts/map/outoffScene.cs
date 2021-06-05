using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class outoffScene : MonoBehaviour
{
    // Start is called before the first frame update
    public float hp;
    public Vector3 pos;
    public bool WallONLevel3Alive;
    public bool aliveWarm;
    public bool canBePaused;
    public string typeGun;
    void Start()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("GameController");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);

    }
    void Update()
    {
        
    }

}
