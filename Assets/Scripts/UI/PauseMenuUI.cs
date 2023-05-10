using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuUI : MonoBehaviour
{
    [SerializeField] SceneFader sceneFader;
    public GameObject pauseMenuUI;
    private void Start()
    {
        //Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
    }
    public void Retry()
    {
        TogglePause();
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }
    public void Menu()
    {
        TogglePause();
        sceneFader.FadeTo("MainMenu");
    }
    public void TogglePause()
    {
        pauseMenuUI.SetActive(!pauseMenuUI.activeSelf);
        Time.timeScale = Convert.ToSingle(!pauseMenuUI.activeSelf);
    }
}
