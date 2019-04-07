using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMOCameraController : MonoBehaviour
{
    public Transform playerCam, centerPoint;

    public float mouseX, mouseY;
    public float mouseSensitivity = 10f;

    public float zoom;
    public float zoomSpeed = 2;

    public float zoomMin = -2f;
    public float zoomMax = -10f;
    
    // Start is called before the first frame update
    void Start()
    {
        zoom = -3;
        playerCam.LookAt(centerPoint);
    }

    // Update is called once per frame
    void Update()
    {
        //zoom += Input.GetAxis("Moust ScrollWheel") * zoomSpeed;
        if (zoom > zoomMin)
            zoom = zoomMin;

        if (zoom < zoomMax)
            zoom = zoomMax;

        playerCam.transform.localPosition = new Vector3(0, 0, zoom);

        if (Input.GetMouseButton(1))
        {
            mouseX = Input.GetAxis("Mouse X");
            mouseY = Input.GetAxis("Mouse Y");
        }

        mouseY = Mathf.Clamp(mouseY, -60f, 60f);

        playerCam.LookAt(centerPoint);
        centerPoint.localRotation = Quaternion.Euler(mouseY, mouseX, 0);
    }
}
