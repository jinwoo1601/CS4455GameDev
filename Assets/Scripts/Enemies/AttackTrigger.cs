using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour
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

        Debug.Log("attact in range\n");
        parent.SendMessage("PlayerInAttackRange", other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag != "Player")
            return;

        Debug.Log("attact out  range \n");
        parent.SendMessage("PlayerOutAttackRange", other.gameObject);
    }
}
