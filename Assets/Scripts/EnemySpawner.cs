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

    private int currentWaveIndex = 0;
    private int currentEnemyIndex = 0;

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            if (currentWaveIndex < enemyWaves.Count)
            {
                List<GameObject> currentWave = enemyWaves[currentWaveIndex].enemies;

                if (currentEnemyIndex < currentWave.Count)
                {
                    GameObject enemyToSpawn = currentWave[currentEnemyIndex];
                    Instantiate(enemyToSpawn, spawnPoint.position, spawnPoint.rotation);
                    currentEnemyIndex++;
                }
                else
                {
                    currentWaveIndex++;
                    currentEnemyIndex = 0;
                }
            }
            else
            {
                break;
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
