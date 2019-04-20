using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableCoin : MonoBehaviour
{

    public float y;

    // code to place the coin
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

    void Update(){
        transform.Rotate( new Vector3 (0,y,0) * Time.deltaTime);
    }
}
