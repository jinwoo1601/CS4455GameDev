using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureChestScript : MonoBehaviour
{
    // 
    void OnTriggerEnter(Collider c)
    {
        Debug.Log("trigger enter: " + c.tag);
        if (c.CompareTag("Player"))
        {
            CoinCollector kc = c.gameObject.GetComponent<CoinCollector>();
            if (kc != null)
            {
                if (Input.GetKeyDown(KeyCode.Space)) {
                    kc.CollectTreasure();
                    // sound event?
                    Destroy(this.gameObject);
                }
            }
        }
    }
}
