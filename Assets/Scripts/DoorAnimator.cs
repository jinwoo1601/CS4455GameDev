using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimator : MonoBehaviour
{
    public Animation anim;

    // void Start()
    // {
    //     anim = GetComponent<Animation>();
    // }

    // Update is called once per frame
    void OnTriggerEnter(Collider c) {
       anim.Play("Door");
    }

    void OnTriggerExit(Collider c) {
    	anim.Play("close");
    }
}
