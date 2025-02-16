using UnityEngine;

public class ChangeSpawnSpeed : MonoBehaviour
{
    public float newSpawnInterval = 3f;

    private void Start()
    {
        EnemySpawner[] enemySpawners = FindObjectsOfType<EnemySpawner>();

        foreach (var spawner in enemySpawners)
        {
            spawner.spawnInterval = newSpawnInterval;
        }

        if (gameObject.tag != "Original")
        {
            Destroy(gameObject);
        }
    }
}
