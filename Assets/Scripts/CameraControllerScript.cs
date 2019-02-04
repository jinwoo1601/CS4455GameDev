using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerScript : MonoBehaviour
{

    public GameObject player;

    private Vector3 offset;

    float distance;
    Vector3 playerPrevPos, playerMoveDir;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.transform.position;

        distance = offset.magnitude;
        playerPrevPos = player.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        playerMoveDir = player.transform.position - playerPrevPos;
        if (playerMoveDir != Vector3.zero)
        {
            playerMoveDir.Normalize();
            transform.position = player.transform.position - playerMoveDir * distance;

            transform.position = new Vector3(transform.position.x, transform.position.y + 20f, transform.position.y); // required height
            transform.rotation = Quaternion.Euler(transform.rotation.x-30f, transform.rotation.y, transform.rotation.z);

            transform.LookAt(player.transform.position);

            playerPrevPos = player.transform.position;
        }

    }
}
