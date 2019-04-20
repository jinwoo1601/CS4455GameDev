using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gateController : MonoBehaviour
{

	private bool animPlayed;
	public Animation anim;
	//public string message;
	public DialogueManager DM;


    // Update is called once per frame
   	void Start() {
   		animPlayed = false;
   	}


    void OnTriggerStay(Collider other)
    {
    	KeyCollector kc = other.gameObject.GetComponent<KeyCollector>();
        
        if(kc != null) {
        	if (Input.GetKeyDown("space") && other.gameObject.tag == "Player" && animPlayed == false && kc.getHasKey())
        	{
        		animPlayed = true;
            	anim.Play("Raise bars");
        	} else if (kc.getHasKey()) {
        		DM.DisplayMessage("Press 'Space' to unlock door");
        	} else {
        		DM.DisplayMessage("You need a key to unlock this door");
        	}
        } else {
        	DM.DisplayMessage("You need a key to unlock this door");
        }
    }


    void OnTriggerExit(Collider other) {
    	DM.HideMessage();
    }
}
