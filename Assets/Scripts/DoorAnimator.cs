using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimator : MonoBehaviour
{
    public Animation anim;
    public Animation animButton;
    private bool boxOnPlate;
    private Collider tempCollider1;
    private Collider tempCollider2;

    void Start()
    {
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider c) {
        if (c.CompareTag("crate"))
        {
            boxOnPlate = true;
            anim.Play("Door");
            animButton.Play("Press");
            EventManager.TriggerEvent<doorEvent, Vector3>(transform.position);
        }
    }

    void OnTriggerExit(Collider c) {
        if (!boxOnPlate)
        {
            anim.Play("close");
            animButton.Play("Depress");
            EventManager.TriggerEvent<doorEvent, Vector3>(transform.position);
        }
    }
}
