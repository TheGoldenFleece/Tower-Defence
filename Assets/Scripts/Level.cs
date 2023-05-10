using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Level
{
    [field: SerializeField]
    public string Name { get; private set; }
    [field: SerializeField]
    public int Index { get; private set; }
    public Level NextLevel { get; private set; }
    public static List<Level> Levels { private set; get; }
    public Level(List<Level> levels)
    {
        Levels = levels;
        
        Level level = Level.GetCurrentLevel();
        
        Name = level.Name;
        Index = level.Index;
        NextLevel = Levels.Find(level => level.Index > Index);
    }
    //public void UnlockNextLevel()
    //{
    //    if (Index < Levels.Count)
    //    {
    //        PlayerPrefs.SetInt("LevelReached", Index + 1);
    //    }
    //}

    public static Level GetCurrentLevel()
    {
        string levelName = SceneManager.GetActiveScene().name;
        return Levels.Find(level => level.Name == levelName);
    }
}
