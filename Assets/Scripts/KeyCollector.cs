using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCollector : MonoBehaviour
{
    private bool hasKey;

    public void CollectKey()
    {
        hasKey = true;
    }

    public bool getHasKey()
    {
        return hasKey;
    }
}
