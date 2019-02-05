using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 camOffset;
    public Vector3 sightOffset;
    public float smoothRate = 0.7f;

    void LateUpdate()
    {
        if(target != null)
        {
            Vector3 desiredPosition = Vector3.Lerp(transform.position, target.TransformPoint(camOffset), smoothRate);
            transform.position = desiredPosition;

            Vector3 desiredLookAtPosition = target.TransformPoint(sightOffset);
            transform.LookAt(desiredLookAtPosition);
        }
    }
}
