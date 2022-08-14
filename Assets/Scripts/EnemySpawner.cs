using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform enemyPrefab;
    public Transform spawnPoint;
    public float spawnTime = 2f;
    
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
