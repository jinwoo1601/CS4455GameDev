using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance
    {
        get
        {
            if (_instance == null)
                _instance = new Player();
            return _instance;
        }
    }


    public GameObject[] inventories;
    public static int HealthPoint
    {
        get
        {
            return _healthPoint;
        }
        set
        {
            if(value > _maxHP)
            {
                _healthPoint = _maxHP;
            }
            else if(value < 0) 
            {
                Debug.Log("Cannot set negative health points\n");
            }
            else
            {
                _healthPoint = value;
            }
        }
    }

    public static int Level
    {
        get
        {
            return _level;
        }
        set
        {
            _level = value;
        }
    }

    private static int _healthPoint;
    private static int _level;
    private static int _maxHP;
    private static Player _instance;

    public static Vector3 SpawningPos = Vector3.zero;

    private Camera _camera;

    private Player() {
        _instance = this;
        Debug.Log("Player constructed\n");
    }

    ~Player()
    {
        Debug.Log("player destructed");
        //_camera.enabled = false;
    }

    private void Awake()
    {
        //DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        //_camera = GetComponent<Camera>();
        //if (_camera == null)
        //    Debug.Log("could not load camera.");

        //_camera.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadData(PlayerData data)
    {
        HealthPoint = data.healthPoint;
        Level = data.level;
        SpawningPos = new Vector3(data.position[0], data.position[1], data.position[2]);
    }

}
