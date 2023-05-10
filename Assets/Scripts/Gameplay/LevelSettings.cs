using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSettings : MonoBehaviour
{
    [SerializeField]
    private TurretItem[] turrets;

    [SerializeField] string nextLevel;
    public static string NextLevel;

    [SerializeField] int currentLevelIndex;
    public static int CurrentLevelIndex;

    public static bool IsBossLevel;
    private void OnValidate()
    {
        for (int i = 0; i < turrets.Length; i++)
        {
            if (!turrets[i].enabled)
            {
                turrets[i].turret.SetActive(false);
            }
            else
            {
                turrets[i].turret.SetActive(true);
            }
        }
    }
    private void Start()
    {

        if (SceneManager.GetActiveScene().name == "Level04")
        {
            IsBossLevel = true;
        }
        CurrentLevelIndex = currentLevelIndex;
        NextLevel = nextLevel;
    }
}

[Serializable]
public class TurretItem
{
    public GameObject turret;
    public bool enabled;
}
