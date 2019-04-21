using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureChestScript : MonoBehaviour
{
    //
    private bool collided;
    private Collider collidedObject;

    private void Update()
    {
        if (collided)
        {
            if (collidedObject.CompareTag("Player"))
            {
                CoinCollector kc = collidedObject.gameObject.GetComponent<CoinCollector>();
                if (kc != null)
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        kc.CollectTreasure();
                        // sound event?
                        Destroy(this.gameObject);
                    }
                }
            }
        }
    }

    void OnTriggerEnter(Collider c)
    {
        Debug.Log("trigger enter: " + c.tag);
        collided = true;
        collidedObject = c;
    }

    void OnTriggerExit(Collider other)
    {
        collided = false;
    }
}
