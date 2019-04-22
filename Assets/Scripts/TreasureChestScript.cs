using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureChestScript : MonoBehaviour
{
    //
    private bool collided;
    private Collider collidedObject;
    private bool TreasureNotCollected;
    public DialogueManager DM;


    private void Start(){
        TreasureNotCollected = true;
    }

    private void Update()
    {
        if (collided)
        {
            if (collidedObject.CompareTag("Player"))
            {
                CoinCollector kc = collidedObject.gameObject.GetComponent<CoinCollector>();
                if (kc != null)
                {
                    if (Input.GetKeyDown(KeyCode.Space) && TreasureNotCollected)
                    {
                        kc.CollectTreasure();
                        TreasureNotCollected = false;
                        this.transform.GetChild(0).gameObject.SetActive(false);
                        EventManager.TriggerEvent<treasureEvent, Vector3>(transform.position);
                        //Destroy(this.gameObject);
                    } else if(TreasureNotCollected){
                        DM.DisplayMessage("Press 'Space' to loot chest.");
                    } else {
                        DM.DisplayMessage("You've already looted this chest.");
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
        DM.HideMessage();
    }
}
