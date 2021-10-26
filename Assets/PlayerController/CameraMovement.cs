using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    Camera cam;
    public Vector3 camPosition;
    
    float camSpeed=20;

    void Start()
    {
        cam = Camera.main;
        
    }

    void Update()
    {

        Movement();
    }

    private void Movement()
    {
        if (Input.GetKey(KeyCode.W))
            camPosition.z += camSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.S))
            camPosition.z -= camSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.A))
            camPosition.x -= camSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.D))
            camPosition.x += camSpeed * Time.deltaTime;

        camPosition.x = Mathf.Clamp(camPosition.x, 0, 100);
        camPosition.z = Mathf.Clamp(camPosition.z, 0, 100);
        cam.transform.position = camPosition;
    }
}