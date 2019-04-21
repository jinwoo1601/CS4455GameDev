using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinCollector : MonoBehaviour
{
    int CoinCount;
    public Text countText;

    public void Start()
    {
        CoinCount = 0;
        UpdateCountText();
    }

    public void CollectCoin()
    {
        CoinCount++;
        UpdateCountText();
    }

    public void CollectTreasure()
    {
        int rand = Random.Range(20, 40);
        CoinCount += rand;
        UpdateCountText();
    }

    public int getCoinCount()
    {
        return CoinCount;
    }

    private void UpdateCountText()
    {
        countText.text = CoinCount.ToString();
    }
}
