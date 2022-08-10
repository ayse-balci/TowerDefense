using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform enemyPrefab;
    public Transform spawnPoint;
    public float spawnTime = 2f;
    private int enemyCount = 0;
    private int enemyNumber = 1;
    private void Start()
    {
        
        StartCoroutine(SpawnEnemy()); 
        enemyCount++;
        
    }

    IEnumerator SpawnEnemy()
    {
        while (enemyCount < 5)
        {
            yield return new WaitForSeconds(spawnTime);
            Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
            enemyCount++;
        }
    }
}
