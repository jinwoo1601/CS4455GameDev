using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Fences : MonoBehaviour
{

    private Rigidbody rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnCollisionEnter(Collision collision)
    {
        print(collision.gameObject.CompareTag("Player"));
        if (collision.gameObject.CompareTag("Player")) {
            this.rigidbody.MovePosition(rigidbody.position + new Vector3(0,40,0));
        }
    }
}
