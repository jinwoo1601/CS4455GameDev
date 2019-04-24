﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gateController : MonoBehaviour
{

	private bool animPlayed;
	public Animation anim;

    // Update is called once per frame
   	void Start() {
   		animPlayed = false;
   	}

    private void OnTriggerEnter(Collider other)
    {
        KeyCollector kc = other.gameObject.GetComponent<KeyCollector>();

        if (kc != null)
        {
            if (kc.getHasKey())
            {
                BarbPlayerController.instance.setHintText("Press 'Space' to unlock door");
            }
            else
            {
                BarbPlayerController.instance.setHintText("You need a key to unlock this door");
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
    	KeyCollector kc = other.gameObject.GetComponent<KeyCollector>();
        
        if(kc != null) {
        	if (Input.GetKeyDown("space") && other.gameObject.tag == "Player" && animPlayed == false && kc.getHasKey())
        	{
        		animPlayed = true;
            	anim.Play("Raise bars");
        	}
        }
    }
}
