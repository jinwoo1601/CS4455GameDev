using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{

    public string nextScene;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody != null)
        {
            KeyCollector kc = other.attachedRigidbody.GetComponent<KeyCollector>();
            if (kc != null)
            {
                if (kc.getHasKey())
                {
                    GameManager.instance.MoveToScene(nextScene);
                } else
                {
                    // maybe prompt a message like "
                }
            }
        }
    }
}
