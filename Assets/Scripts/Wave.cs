using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public List<OneTypeEnemies> enemies;
    public float Rate;
    public int Count { get => enemies.Sum(enemy => enemy.count); }
    int index = 0;
    public GameObject GetPrefab()
    {
        if (enemies[index].Index == 0)
        {
            index++;
        }

        if (enemies[index] == null) return null;

        enemies[index].Index--;
        return enemies[index].prefab;
    }
}

[System.Serializable]
public class OneTypeEnemies
{
    public GameObject prefab;
    public int count;
    int index = 0;
    public int Index { get => count - index; set => index++; }
}

