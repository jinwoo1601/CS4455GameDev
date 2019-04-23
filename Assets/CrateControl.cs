using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateControl : MonoBehaviour
{
    Rigidbody rbody;
    int timer = 30;
    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rbody.velocity.magnitude >= 0.1)
        {
            timer++;
            if(timer > 30)
            {
                EventManager.TriggerEvent<crateEvent, Vector3>(transform.position);
                timer = 0;
            }
            
        }
    }
}
