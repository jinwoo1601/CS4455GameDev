using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(9999)]
public class FollowUpdate : MonoBehaviour
{
    public Transform toFollow;
    public Vector3 offset;
    private void FixedUpdate()
    {
        transform.position = toFollow.position + offset;
        transform.rotation = toFollow.rotation;
    }
}
