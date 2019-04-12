using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableKey : MonoBehaviour
{
    // code to place the key
    void OnTriggerEnter(Collider c)
    {
        Debug.Log("trigger enter: " + c.tag);
        if (c.CompareTag("Player"))
        {
            KeyCollector kc = c.gameObject.GetComponent<KeyCollector>();
            if (kc != null)
            {
                kc.CollectKey();
                // sound event?
                Destroy(this.gameObject);
            }
        }
    }
}
