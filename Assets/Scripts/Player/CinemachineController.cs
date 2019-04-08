using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineController : MonoBehaviour
{
    public CinemachineFreeLook cam;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("mouse down");
            cam.m_XAxis.m_InputAxisName = "Mouse X";
            cam.m_YAxis.m_InputAxisName = "Mouse Y";
        }

        if (Input.GetMouseButtonUp(1))
        {
            Debug.Log("mouse up");
            cam.m_XAxis.m_InputAxisName = "Horizontal";
            cam.m_YAxis.m_InputAxisName = "";
            cam.m_XAxis.m_InputAxisValue = 0;
            cam.m_YAxis.m_InputAxisValue = 0;
        }
        //if (Input.GetMouseButton(1))
        //{

        //    Debug.Log("mouse button 1");
        //    cam.m_XAxis
        //}
    }
}
