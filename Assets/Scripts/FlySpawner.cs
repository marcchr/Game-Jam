using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class FlySpawner : MonoBehaviour
{
    Queue<EnemyController> _availableEnemies = new();

    [SerializeField] EnemyController _enemyPrefab;
    [SerializeField] EnemyData Data;

    [SerializeField] int _enemyLimit;
    [SerializeField] int _spawnInterval;

    private void Start()
    {
        InstantiateEnemies(Data);
        InvokeRepeating(nameof(SpawnEnemy), 1f, _spawnInterval);
    }

    private void InstantiateEnemies(EnemyData data)
    {
        Data = data;

        for (int i = 0; i < _enemyLimit; i++)
        {
            //spawn enemies
            var enemy = Instantiate(_enemyPrefab, transform);
            enemy.Initialize(this, data);
            enemy.gameObject.SetActive(false);
            _availableEnemies.Enqueue(enemy);
        }
    }

    public void ReturnEnemyToPool(EnemyController enemy)
    {
        _availableEnemies.Enqueue(enemy);
    }

    private void SpawnEnemy()
    {
        var spawnPosition = transform.position;
        var enemy = _availableEnemies.Dequeue();
        enemy.transform.position = spawnPosition;
        enemy.gameObject.SetActive(true);
    }

}
