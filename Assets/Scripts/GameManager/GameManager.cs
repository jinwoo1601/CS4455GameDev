﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

using System.Collections.Generic;       //Allows us to use Lists. 
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.
    public static bool GameEnd = false;
    //private BoardManager boardScript;                       //Store a reference to our BoardManager which will set up the level.
    private int level = 3;                                  //Current level number, expressed in game as "Day 1".
    public bool paused = false;

    public int enemyCount;
    private bool keySpawned = false;

    public GameObject keyPrefab;

    public GameObject coinPreFab;

    public GameObject loading_bar;

    public List<Sprite> loading_bar_sprites;

    //Awake is always called before any Start functions
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        //DontDestroyOnLoad(gameObject);

        //Get a component reference to the attached BoardManager script
        //boardScript = GetComponent<BoardManager>();

        //Call the InitGame function to initialize the first level 
        InitGame();
        loading_bar_sprites = new List<Sprite>();
        for (int i = 1; i <= 5; i++) {
            loading_bar_sprites.Add(Resources.Load<Sprite>(string.Format("image/%d", i)));
        }
        
    }

    //Update is called every frame.
    void Update()
    {
    }

    //Initializes the game for each level.
    void InitGame()
    {
        //Call the SetupScene function of the BoardManager script, pass it current level number.
        //boardScript.SetupScene(level);
        //SceneManager.LoadScene(SceneStore.MENU);

    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneStore.LEVEL1);
    }

    public void LoadSetting()
    {
        SceneManager.LoadScene(SceneStore.SETTING);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1.0f;
        ResumeGame();
        GameEnd = false;
    }

    public void PauseGame()
    {
        paused = true;
        //Time.timeScale = 0.0f;
    }

    public void ResumeGame()
    {
        paused = false;
        //Time.timeScale = 1.0f;
    }

    public void LoadGame()
    {
        PlayerData playerData = SaveSystem.LoadPlayer();
        Player.Instance.LoadData(playerData);
        SceneManager.LoadScene(playerData.scene);

    }

    public void SaveGame()
    {
        SaveSystem.SavePlayer();
    }

    public void MoveToScene(string scene)
    {
        AsyncOperation  async = SceneManager.LoadSceneAsync(scene);
        loading_bar.SetActive(true);
        while (!async.isDone) {
            float prog = async.progress;
            if (prog > 0.1f && prog < 0.4f)
            {
                loading_bar.GetComponentInChildren<Image>().sprite = loading_bar_sprites[1];
            }
            else if (prog >= 0.4f && prog < 0.6f)
            {
                loading_bar.GetComponentInChildren<Image>().sprite = loading_bar_sprites[2];
            }
            else if (prog >= 0.6f && prog < 0.9f) {
                loading_bar.GetComponentInChildren<Image>().sprite = loading_bar_sprites[3];
            } else if (prog >= 0.9f) {
                loading_bar.GetComponentInChildren<Image>().sprite = loading_bar_sprites[4];
            }
        }
    }

    // On eneymy death, reduce count and spawn a random number of coins. Called by Enemy
    public void EnemyDeath(Vector3 deathPosition, Quaternion deathRotation)
    {
        if (enemyCount > 0)
        {
            enemyCount--;
        }
        int numCoin = Random.Range(1, 4);
        if (numCoin == 1)
        {
            SpawnCoin(deathPosition, deathRotation);
        } else if (numCoin==2)
        {
            SpawnCoin(deathPosition, deathRotation);
            SpawnCoin(deathPosition, deathRotation);
        } else
        {
            SpawnCoin(deathPosition, deathRotation);
            SpawnCoin(deathPosition, deathRotation);
            SpawnCoin(deathPosition, deathRotation);
        }
    }

    // Spawn the key to the next room when enemy count is zero.
    public void SpawnKey(Transform trans)
    {
        Instantiate(keyPrefab, trans.position, trans.rotation);
    }

    //Spawn a coin in a random location relative to the enemy's death location.
    public void SpawnCoin(Vector3 coinSpawnPoint, Quaternion deathRotation)
    {
        ////Random.InitState(System.Environment.TickCount);
        int randXDir = Random.Range(0, 2);
        int randZDir = Random.Range(0, 2);
        int randXMag = Random.Range(1, 4);
        int randZMag = Random.Range(1, 4);
        if (randXDir == 0) { randXDir = -1; }
        if (randZDir == 0) { randZDir = -1; }
        coinSpawnPoint = new Vector3(coinSpawnPoint.x + randXDir * randXMag, coinSpawnPoint.y+1, coinSpawnPoint.z + randZDir * randZMag);
        Instantiate(coinPreFab, coinSpawnPoint, deathRotation);
    }
}