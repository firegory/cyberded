using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class background : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform camera;
    public float speed = 0.2f;
    private float cameraX;
    private float cameraY;
    void Start()
    {
        cameraX = camera.transform.position.x;
        cameraY = camera.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        float deltaX = camera.position.x - cameraX;
        cameraX = camera.transform.position.x;
        float deltaY = camera.position.y - cameraY;
        cameraY = camera.transform.position.y;
        transform.position = new Vector3(transform.position.x + speed * deltaX, transform.position.y + deltaY,50);
    }
}
