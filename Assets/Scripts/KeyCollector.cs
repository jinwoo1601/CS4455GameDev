using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyCollector : MonoBehaviour
{
    private bool hasKey;
    public Image keyImage;

    public void Start()
    {
        hasKey = false;
        keyImage.enabled = false;
    }

    public void CollectKey()
    {
        hasKey = true;
        keyImage.enabled = true;
    }

    public bool getHasKey()
    {
        return hasKey;
    }
}
