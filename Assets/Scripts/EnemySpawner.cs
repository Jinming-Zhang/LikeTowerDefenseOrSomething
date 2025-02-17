using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyWave
{
    public List<GameObject> enemies;
}

public class EnemySpawner : MonoBehaviour
{
    public float spawnInterval = 3f;
    public List<EnemyWave> enemyWaves;
    public Transform spawnPoint;
    public Resources resources;

    private int currentWaveIndex = 0;
    private int currentEnemyIndex = 0;

    public bool waveRunning = true;

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private void Update()
    {
        if(!waveRunning)
        {
            waveRunning = true;
            Debug.Log(waveRunning);
            StartCoroutine(SpawnEnemies());
        }
    }

    private IEnumerator SpawnEnemies()
    {
        while (waveRunning)
        {
            if (currentWaveIndex < enemyWaves.Count)
            {
                List<GameObject> currentWave = enemyWaves[currentWaveIndex].enemies;

                if (currentEnemyIndex < currentWave.Count)
                {
                    GameObject toSpawn = currentWave[currentEnemyIndex];
                    GameObject enemy = Instantiate(toSpawn, spawnPoint.position, spawnPoint.rotation);
                    if (enemy.tag != "Spawner Modifiers")
                    {
                        enemy.tag = "Enemy";
                    }

                    currentEnemyIndex++;
                }
                else
                {
                    currentWaveIndex++;
                    currentEnemyIndex = 0;
                    CheckWaveStatus();
                }
            }
            else
            {
                break;
            }

            yield return new WaitForSeconds(spawnInterval);
        }

        //CheckWaveStatus();
    }

    private void CheckWaveStatus()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            waveRunning = false;
            if (resources.gainFromWave) { resources.amount += (currentWaveIndex + 1) * resources.gainFromWaveMultiplyer; }
        }
    }
}
