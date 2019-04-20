using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    int CoinCount;

    public void Start()
    {

    }

    public void CollectCoin()
    {
        CoinCount++;
    }

    public int getCoinCount()
    {
        return CoinCount;
    }
}
