using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionController : MonoBehaviour
{
    public bool following = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        if (following)
        {
            gameObject.SetActive(false);
        }
    }


    public void picked()
    {
        Debug.Log("picked");
        following = true;
        transform.tag = "Untagged";
    }
}
