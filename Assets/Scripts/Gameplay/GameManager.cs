using UnityEngine;
using System;
using System.Reflection;

public class GameManager : MonoBehaviour
{
    public static bool GameIsOver;
    [SerializeField] GameObject gameOverUI;
    [SerializeField] GameObject completeLevelUI;
    public static GameManager instance;

    [SerializeField] GameObject shop;
    [SerializeField] GameObject timer;

    private void Awake()
    {
        if (instance != null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        gameOverUI.SetActive(false);
        GameIsOver = false;
    }

    void Update()
    {
        if (GameIsOver) return;

        if (PlayerStats.Lives <= 0)
        {
            EndGame();
        }   
    }

    void EndGame()
    {
        GameIsOver = true;
        gameOverUI.SetActive(true);
    }

    public void WinLevel()
    {
        GameIsOver = true;

        shop.SetActive(false);
        timer.SetActive(false);

        int levelReached = PlayerStats.LevelReached;
        int currentLevelindex = LevelSettings.CurrentLevelIndex;

        if (PlayerStats.LevelReached < LevelSettings.CurrentLevelIndex + 1)
        {
            PlayerStats.LevelReached = LevelSettings.CurrentLevelIndex + 1;
            PlayerPrefs.SetInt("LevelReached", PlayerStats.LevelReached);
        }

        completeLevelUI.SetActive(true);
    }
}
