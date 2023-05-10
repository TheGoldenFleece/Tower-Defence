using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    [SerializeField] SceneFader sceneFader;
    public void PlayAgain()
    {
        sceneFader.FadeTo("LevelMenu");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
