using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableCoin : MonoBehaviour
{
    // code to place the key
    void OnTriggerEnter(Collider c)
    {
        Debug.Log("trigger enter: " + c.tag);
        if (c.CompareTag("Player"))
        {
            CoinCollector kc = c.gameObject.GetComponent<CoinCollector>();
            if (kc != null)
            {
                kc.CollectCoin();
                // sound event?
                Destroy(this.gameObject);
            }
        }
    }
}
