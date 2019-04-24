using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableKey : MonoBehaviour
{
    // code to place the key
    void OnTriggerStay(Collider c)
    {
        if (c.CompareTag("Player"))
        {
            BarbPlayerController.instance.setHintText("Press 'Space' to pick up key");

            KeyCollector kc = c.gameObject.GetComponent<KeyCollector>();
            if (kc != null)
            {
                if(Input.GetKeyDown("space")){
                    kc.CollectKey();
                    EventManager.TriggerEvent<keyEvent, Vector3>(transform.position);
                    Destroy(this.gameObject);
                }
            }
        }
    }
}
