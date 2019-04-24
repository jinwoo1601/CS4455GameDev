using System;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerData
{
    public static float maxHealth = 100;
    public static float curHealth = 100;
    public static float attackDamage;
    public static int level;
    public static float[] position;
    public static string scene;

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
