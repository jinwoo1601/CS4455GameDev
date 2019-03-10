using System;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerData
{
    public int healthPoint;
    public int level;
    public float[] position;
    public string scene;

    public PlayerData()
    {
        healthPoint = Player.HealthPoint;
        level = Player.Level;

        position = new float[3];
        position[0] = Player.Instance.transform.position.x;
        position[1] = Player.Instance.transform.position.y;
        position[2] = Player.Instance.transform.position.z;

        scene = SceneManager.GetActiveScene().name;
    }

}
