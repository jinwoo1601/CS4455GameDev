using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureChestScript : MonoBehaviour
{
    private bool TreasureNotCollected;
    private bool spacePressed;


    private void Start(){
        TreasureNotCollected = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            spacePressed = true;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            spacePressed = false;
        }
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.CompareTag("Player"))
        {
            CoinCollector kc = c.gameObject.GetComponent<CoinCollector>();
            if (kc != null)
            {
                if (TreasureNotCollected)
                {
                    BarbPlayerController.instance.setHintText("Press 'Space' to loot chest.");
                }
                else
                {
                    BarbPlayerController.instance.setHintText("You've already looted this chest.");
                }
            }
        }
    }

    private void OnTriggerStay(Collider c)
    {
        if (c.CompareTag("Player"))
        {
            CoinCollector kc = c.gameObject.GetComponent<CoinCollector>();
            if (kc != null)
            {
                if (spacePressed && TreasureNotCollected)
                {
                    kc.CollectTreasure();
                    TreasureNotCollected = false;
                    this.transform.GetChild(0).gameObject.SetActive(false);
                    EventManager.TriggerEvent<treasureEvent, Vector3>(transform.position);
                    //Destroy(this.gameObject);
                }
            }
        }
    }
}
