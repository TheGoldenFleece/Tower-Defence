using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] SceneFader sceneFader;
    public void Play()
    {
        sceneFader.FadeTo("LevelMenu");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
