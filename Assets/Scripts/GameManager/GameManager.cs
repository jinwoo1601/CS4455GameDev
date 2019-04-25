using UnityEngine;
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

    private List<Sprite> loading_bar_sprites;

    //Awake is always called before any Start functions
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)
            Destroy(gameObject);

        if (SceneManager.GetActiveScene().name == "MenuScene")
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        InitGame();
    }

    private void Start()
    {
        loading_bar_sprites = new List<Sprite>();
        for (int i = 1; i <= 5; i++)
        {
            loading_bar_sprites.Add(Resources.Load<Sprite>(string.Format("image/{0}", i)));
        }

        if (loading_bar != null)
            loading_bar.SetActive(false);
    }

    //Update is called every frame.
    void Update()
    {
    }

    //Initializes the game for each level.
    void InitGame()
    {
        //Call the SetupScene function of the BoardManager script, pass it current level number.
        enemyCount = FindObjectsOfType<SmallEnemyController>().Length;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Intro");
    }

    public void LoadSetting()
    {
        SceneManager.LoadScene(SceneStore.SETTING);
    }

    public void QuitGame()
    {
        Debug.Log("quit pressed");
        Application.Quit();
    }

    public void RestartGame()
    {
        Debug.Log("work please");
        PlayerData.curHealth = 100f;
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
        SceneManager.LoadScene(PlayerData.scene);

    }

    public void SaveGame()
    {
        SaveSystem.SavePlayer();
    }

    public void MoveToScene(string scene)
    {
        Debug.Log("move to scene");
        loading_bar.SetActive(true);
        StartCoroutine(LoadScene(scene));
    }


    IEnumerator LoadScene(string scene)
    {
        yield return null;

        //Begin to load the Scene you specify
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(scene);
        //Don't let the Scene activate until you allow it to
        asyncOperation.allowSceneActivation = false;
        for (int i = 0; i < 300; i++)
        {
            if (i < 100)
            {
                loading_bar.GetComponentInChildren<Image>().sprite = loading_bar_sprites[1];
            }
            else if (i >= 100 && i < 200)
            {
                loading_bar.GetComponentInChildren<Image>().sprite = loading_bar_sprites[2];
            }
            else if (i >= 200)
            {
                loading_bar.GetComponentInChildren<Image>().sprite = loading_bar_sprites[3];
            }
            if (i % 10 == 0)
            {
                yield return null;
            }
        }
        loading_bar.GetComponentInChildren<Image>().sprite = loading_bar_sprites[4];
        asyncOperation.allowSceneActivation = true;
        while (!asyncOperation.isDone)
        {
            yield return null;
        }
    }

    // On eneymy death, reduce count and spawn a random number of coins. Called by Enemy
    public void EnemyDeath(Vector3 deathPosition, Quaternion deathRotation)
    {
        if (enemyCount > 0)
        {
            enemyCount--;
            if (enemyCount == 0)
            {
                SpawnKey(deathPosition, deathRotation);
            }
        }
        int luckiness = BarbPlayerController.instance.luckiness;
        int numCoin = Random.Range(1+luckiness, 4+luckiness);
        for (int i = 0; i < numCoin; i++)
        {
            SpawnCoin(deathPosition, deathRotation);
        }
    }

    // Spawn the key to the next room when enemy count is zero.
    public void SpawnKey(Vector3 pos, Quaternion rot)
    {
        Instantiate(keyPrefab, pos, rot);
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
        coinSpawnPoint = new Vector3(coinSpawnPoint.x + randXDir * randXMag, coinSpawnPoint.y + 1, coinSpawnPoint.z + randZDir * randZMag);
        Instantiate(coinPreFab, coinSpawnPoint, deathRotation);
    }
}