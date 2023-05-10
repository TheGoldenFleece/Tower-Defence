using JetBrains.Annotations;
using System.Collections;
using System.Linq;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static int EnemiesAlive;

    public Wave[] waves;

    Vector3 spawnPoint;

    [SerializeField] GameManager gameManager;

    [SerializeField] float waveDelay = 0f;
    [SerializeField] float gameStartDelay = 0f;
    int waveIndex = 0;

    public static float Timer { get; private set; }

    private void Start()
    {
        EnemiesAlive = 0;
        Debug.Log("Get PathList");
        spawnPoint = Path.PathList[0].Position;

        if (LevelSettings.IsBossLevel)
        {
            StartCoroutine(SpawnBossWave(gameStartDelay));
        }
        else
        {
            StartCoroutine(SpawnWave(gameStartDelay));
        }
    }

    void Update()
    {
        Timer -= Time.deltaTime;
        Timer = Mathf.Clamp(Spawner.Timer, 0.0f, Mathf.Infinity);
    }
    IEnumerator SpawnWave(float delay)
    {
        Timer = delay;
        yield return new WaitForSeconds(delay);

        if (waves.Length == 0) yield break;
        EnemiesAlive = waves[waveIndex].Count;

        SpawnEnemyWithoutDelay(waveIndex);

        for (int i = 1; i < waves[waveIndex].Count; i++)
        {
            SpawnCoroutine spawnEnemy = new SpawnCoroutine();
            StartCoroutine(spawnEnemy.SpawnEnemy(waves[waveIndex], spawnPoint, 1f / waves[waveIndex].Rate));
            
            yield return new WaitUntil(() => spawnEnemy.EnemyIsSpawned);
        }

        yield return new WaitUntil(() => EnemiesAlive == 0);

        waveIndex++;
        PlayerStats.Rounds++;

        if (waveIndex == waves.Length)
        {
            gameManager.WinLevel();
            StopAllCoroutines();
            yield break;
        }

        Balance.WSI++;
        StartCoroutine(SpawnWave(waveDelay));
    }

    IEnumerator SpawnBossWave(float delay)
    {
        Timer = delay;
        yield return new WaitForSeconds(delay);

        if (waves.Length == 0) yield break;

        // Amount of Enemies;
        for (int i = 0; i < waves.Length; i++)
        {
            EnemiesAlive += waves[i].Count;
        }

        //Spawn first enemies without delay
        for (int i = 0; i < waves.Length; i++)
        {
            SpawnEnemyWithoutDelay(i);
        }

        //Spawn all next enemies with delay
        for (int j = 1; j < waves[waveIndex].Count; j++)
        {
            SpawnCoroutine[] spawners = new SpawnCoroutine[waves.Length];

            for (int i = 0; i < spawners.Length; i++)
            {
                spawners[i] = new SpawnCoroutine();
                StartCoroutine(spawners[i].SpawnEnemy(waves[i], spawnPoint, 1f / waves[i].Rate));
            }

            yield return new WaitUntil(() =>
            {
                foreach (SpawnCoroutine coroutine in spawners)
                {
                    if (!coroutine.EnemyIsSpawned)
                    {
                        return false;
                    }
                }
                return true;
            });
        }

        yield return new WaitUntil(() => EnemiesAlive == 0);

        PlayerStats.Rounds++;
        gameManager.WinLevel();
        StopAllCoroutines();
        yield break;
    }

    void SpawnEnemyWithoutDelay(int i)
    {
        Instantiate(waves[i].GetPrefab(), spawnPoint, Quaternion.identity);
    }

    class SpawnCoroutine
    {
        public bool EnemyIsSpawned { get; private set; }

        public IEnumerator SpawnEnemy(Wave wave, Vector3 spawnPoint, float enemyDelay)
        {
            EnemyIsSpawned = false;
            yield return new WaitForSeconds(enemyDelay);

            Instantiate(wave.GetPrefab(), spawnPoint, Quaternion.identity);
            EnemyIsSpawned = true;
            yield break;
        }
    }
}


