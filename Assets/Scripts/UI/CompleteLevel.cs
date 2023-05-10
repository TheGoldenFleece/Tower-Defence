using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteLevel : MonoBehaviour
{
    public SceneFader sceneFader;
    public void Continue()
    {
        sceneFader.FadeTo($"{LevelSettings.NextLevel}");
    }

    public void Menu()
    {
        sceneFader.FadeTo("MainMenu");
    }
}
