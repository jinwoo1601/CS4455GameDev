using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public static float maxHealth = 100;
    public static float curHealth = 100;
    public static float attackDamage;
    public static int level;
    public static int coinCount;
    public static float[] position;
    public static string scene;
    public static List<Buff.BuffType> buffs = new List<Buff.BuffType>();
    public static List<GameObject> buffAuroras = new List<GameObject>();

    public PlayerData()
    {
        curHealth = Player.HealthPoint;
        level = Player.Level;

        position = new float[3];
        position[0] = Player.Instance.transform.position.x;
        position[1] = Player.Instance.transform.position.y;
        position[2] = Player.Instance.transform.position.z;

        scene = SceneManager.GetActiveScene().name;
    }

}
