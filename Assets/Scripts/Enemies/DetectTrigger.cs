using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectTrigger : MonoBehaviour
{
    GameObject parent;

    private void Awake()
    {
        parent = gameObject.transform.parent.gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
            return;

        Debug.Log("Player detect\n");
        parent.SendMessage("PlayerDetected", other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag != "Player")
            return;

        Debug.Log("Player loss\n");
        parent.SendMessage("PlayerLoss", other.gameObject);
    }
}
