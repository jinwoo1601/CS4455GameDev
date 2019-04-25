using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{

    public string nextScene;
    private bool startLoading = false;
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
        //if (other.attachedRigidbody != null)
        //{
        KeyCollector kc = other.gameObject.GetComponent<KeyCollector>();
        if (kc != null && !startLoading)
        {
            if (kc.getHasKey())
            {
                GameManager.instance.MoveToScene(nextScene);
                startLoading = true;
            }
            else
            {
                // maybe prompt a message like "
            }
        }
        //}
    }
}
