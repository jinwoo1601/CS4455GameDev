using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

using System.Collections.Generic;       //Allows us to use Lists. 

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
    public GameObject keySpawningPos;

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
    }

    //Update is called every frame.
    void Update()
    {
        if (enemyCount <= 0 && !keySpawned)
        {
            keySpawned = true;
            SpawnKey();
        }
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
        SpawnKey();
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
        SceneManager.LoadScene(scene);
    }

    // On eneymy death, reduce count. Called by Enemy
    public void EnemyDeath()
    {
        if (enemyCount > 0)
        {
            enemyCount--;
        }
    }

    // Spawn the key to the next room when enemy count is zero.
    public void SpawnKey()
    {
        Instantiate(keyPrefab, keySpawningPos.transform.position, keySpawningPos.transform.rotation);
    }
}