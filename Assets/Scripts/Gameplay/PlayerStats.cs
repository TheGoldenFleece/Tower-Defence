using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    public static int Money;
    public int startMoney = 400;

    public static int Lives;
    public int startLives = 20;

    public static int Rounds;

    public static int LevelReached;

    void Start()
    {
        LevelReached = PlayerPrefs.GetInt("LevelReached", 1);
        Money = startMoney;
        Lives = startLives;
        Rounds = 0;
    }
}
