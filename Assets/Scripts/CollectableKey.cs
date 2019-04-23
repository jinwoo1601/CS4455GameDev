using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableKey : MonoBehaviour
{
    public DialogueManager DM;
    // code to place the key
    void OnTriggerStay(Collider c)
    {
        Debug.Log("trigger enter: " + c.tag);
        if (c.CompareTag("Player"))
        {
            DM.DisplayMessage("Press 'Space' to pick up key");

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


    private void OnTriggerExit(Collider other){
        DM.HideMessage();
    }

    private void OnDestroy(){
        DM.HideMessage();
    }
}
