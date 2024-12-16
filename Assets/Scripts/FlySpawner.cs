using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class FlySpawner : MonoBehaviour
{
    public Queue<GameObject> enemyPool = new();
    public GameObject enemyToPool;
    public int amountToPool;
    public int spawnFrequency;
    //github testing
    private void Start()
    {
        GameObject fly;
        for (int i = 0; i < amountToPool; i++)
        {
            fly = Instantiate(enemyToPool);
            fly.SetActive(false);
            enemyPool.Enqueue(fly);
        }

        StartCoroutine(SpawnFlies());
    }

    public void ReturnEnemyToPool(GameObject enemy)
    {
        enemyPool.Enqueue(enemy);
    }

    private void SpawnEnemy()
    {
        var spawnPosition = transform.position;
        var enemy = enemyPool.Dequeue();
        enemy.transform.position = spawnPosition;
        enemy.gameObject.SetActive(true);
    }

    IEnumerator SpawnFlies()
    {
        while (gameObject.activeSelf)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnFrequency);
        }
        
    }
}
