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

    public void CollectTreasure()
    {
        int rand = Random.Range(20, 40);
        CoinCount += rand;
    }

    public int getCoinCount()
    {
        return CoinCount;
    }
}
