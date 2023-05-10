using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] SceneFader sceneFader;
    public void Play()
    {
        sceneFader.FadeTo("Level01");
    }
}
