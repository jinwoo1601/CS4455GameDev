using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(9999)]
public class FollowUpdate : MonoBehaviour
{
    public Transform toFollow;
    private void FixedUpdate()
    {
        transform.position = toFollow.position;
        transform.rotation = toFollow.rotation;
    }
}
