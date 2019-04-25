using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float CameraMoveSpeed = 120.0f;
    public GameObject Follow;
    public Vector3 FollowPos;
    public float ClampAngle = 80.0f;
    public float InputSensitivity = 105.0f;
    public GameObject CameraObject;
    public GameObject PlayerObj;
    public float camDistanceXToPlayer;
    public float camDistanceYToPlayer;
    public float camDistanceZToPlayer;
    public float mouseX;
    public float mouseY;
    public float mouseXBase;
    public float mouseYBase;
    public float finalInputX;
    public float finalInputY;
    public float smoothX;
    public float smoothY;
    public float smooth = 10.0f;
    private float rotY = 0.0f;
    private float rotX = 0.0f;

    private void Start()
    {
        Vector3 rot = transform.localRotation.eulerAngles;
        rotX = rot.x;
        rotY = rot.y;
    }

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            mouseXBase = -Input.GetAxis("Mouse Y");
            mouseYBase = Input.GetAxis("Mouse X");
        }

        else if (Input.GetMouseButton(1))
        {
            float inputX = Input.GetAxis("RightStickHorizontal");
            float inputY = Input.GetAxis("RightStickVertical");
            mouseX = -Input.GetAxis("Mouse Y") - mouseXBase;
            mouseY = Input.GetAxis("Mouse X") - mouseYBase;
            finalInputX = inputX + mouseX;
            finalInputY = inputY + mouseY;

            rotX += finalInputX * InputSensitivity * Time.deltaTime;
            rotY += finalInputY * InputSensitivity * Time.deltaTime;

            rotX = Mathf.Clamp(rotX, -ClampAngle, ClampAngle);

            Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
            transform.rotation = localRotation;
            PlayerObj.transform.rotation = Quaternion.Euler(0.0f, rotY, 0.0f);

        }
        else
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Follow.transform.rotation, smooth * Time.deltaTime);
        }
    }

    private void LateUpdate()
    {
        CameraUpdate();
    }

    void CameraUpdate()
    {
        Vector3 target = Follow.transform.position;
        target.y += 1;
        

        float step = CameraMoveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target, step);
    }
}
