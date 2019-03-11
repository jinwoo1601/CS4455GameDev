using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSelector : MonoBehaviour
{
    GameObject parent;

    private void Awake()
    {
        parent = transform.parent.gameObject;
    }

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("enter: " + other.tag);
        if (other.tag != "Enemy")
        {
            return;
        }
        Debug.Log("enter target trigger");
        parent.GetComponentInParent<PlayerController>().SetTarget(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("exit: " + other.tag);

        if (other.tag != "Enemy")
        {
            return;
        }
        Debug.Log("exit target trigger");
        parent.SendMessage("RemoveTarget", other.gameObject);
    }
}
