using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform enemyPrefab;
    public Transform spawnPoint;
    public float spawnTime = 2f;
    
    // GameManager call this function by enemy count according to level ( enemy count = level * 2 + 1)
    public void SpawnEnemyFromGameManager(int enemyCount)
    {
        StartCoroutine(SpawnEnemy(enemyCount)); 
    }

    public IEnumerator SpawnEnemy(int enemyCount)
    {
        while (enemyCount > 0)
        {
            yield return new WaitForSeconds(spawnTime);
            Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
            enemyCount--;
        }
    }
}
