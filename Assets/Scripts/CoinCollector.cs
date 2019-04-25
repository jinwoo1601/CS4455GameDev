using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinCollector : MonoBehaviour
{
    public Text countText;

    public void Start()
    {
        UpdateCountText();
    }

    private void Update()
    {
        UpdateCountText();
    }

    public void CollectCoin()
    {
        PlayerData.coinCount++;
    }

    public void CollectTreasure()
    {
        int rand = Random.Range(10, 20);
        PlayerData.coinCount += rand;
    }

    public int getCoinCount()
    {
        return PlayerData.coinCount;
    }

    private void UpdateCountText()
    {
        countText.text = PlayerData.coinCount.ToString();
    }
}
