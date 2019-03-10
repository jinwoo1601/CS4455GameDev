using UnityEngine;
using System;
using System.Collections.Generic;       //Allows us to use Lists.
using Random = UnityEngine.Random;      //Tells Random to use the Unity Engine random number generator.


public class BoardManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject OrigSpawningPos;

    private void Awake()
    {
        GameObject player = (GameObject)Instantiate(playerPrefab, OrigSpawningPos.transform.position, OrigSpawningPos.transform.rotation);
        player.tag = "Player";
    }

}