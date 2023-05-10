using UnityEngine;
using UnityEngine.SceneManagement;


public class GameOver : MonoBehaviour
{
    public SceneFader sceneFader;
     public void Menu()
    {
        sceneFader.FadeTo("MainMenu");
    }
    public void Retry()
    {
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }
}
