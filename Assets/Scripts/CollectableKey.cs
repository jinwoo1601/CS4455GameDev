using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableKey : MonoBehaviour
{
    // code to place the key
    void OntTriggerEnter(Collider c)
    {
        if (c.attachedRigidbody != null)
        {
            KeyCollector kc = c.attachedRigidbody.gameObject.GetComponent<KeyCollector>();
            if (kc != null)
            {
                kc.CollectKey();
                // sound event?
                Destroy(this.gameObject);
            }
        }
    }
}
