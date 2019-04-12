using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimator : MonoBehaviour
{
    public Animation anim;
    public Animation animButton;
    private int inTrigger;

    void Start()
    {
        inTrigger = 0;
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider c) {
       inTrigger++;
       anim.Play("Door");
       animButton.Play("Press");
    }

    void OnTriggerExit(Collider c) {
    	inTrigger--;
    	anim.Play("close");

    	if(inTrigger < 1) {
    		animButton.Play("Depress");
    	}
    }
}
