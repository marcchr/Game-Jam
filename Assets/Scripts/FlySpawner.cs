using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class FlySpawner : MonoBehaviour
{
    Queue<EnemyController> _availableEnemies = new();
    Queue<RareEnemyController> _availableRareEnemies = new();
    Queue<BadEnemyController> _availableBadEnemies = new();

    [SerializeField] EnemyController _enemyPrefab;
    [SerializeField] EnemyData Data;

    [SerializeField] RareEnemyController _rareEnemyPrefab;
    [SerializeField] EnemyData RareData;

    [SerializeField] BadEnemyController _badEnemyPrefab;
    [SerializeField] EnemyData BadData;

    [SerializeField] int _enemyLimit;
    [SerializeField] int _spawnInterval;

    [SerializeField] int _rareEnemyLimit;
    [SerializeField] int _rareSpawnInterval;

    [SerializeField] int _badEnemyLimit;
    [SerializeField] int _badSpawnInterval;

    [SerializeField] int badDelay;
    [SerializeField] int rareDelay;

    private void Start()
    {
        InstantiateEnemies(Data, RareData, BadData);
        InvokeRepeating(nameof(SpawnEnemy), 1f, _spawnInterval);
        Invoke(nameof(SpawnRares), rareDelay);
        Invoke(nameof(SpawnBads), badDelay);

    }

    private void InstantiateEnemies(EnemyData data, EnemyData rareData, EnemyData badData)
    {
        Data = data;
        RareData = rareData;
        BadData = badData;

        for (int i = 0; i < _enemyLimit; i++)
        {
            //spawn enemies
            var enemy = Instantiate(_enemyPrefab, transform);
            enemy.Initialize(this, data);
            enemy.gameObject.SetActive(false);
            _availableEnemies.Enqueue(enemy);
        }

        for (int i = 0; i < _rareEnemyLimit; i++)
        {
            var rare = Instantiate(_rareEnemyPrefab, transform);
            rare.Initialize(this, rareData);
            rare.gameObject.SetActive(false);
            _availableRareEnemies.Enqueue(rare);
        }

        for (int i = 0; i < _badEnemyLimit; i++)
        {
            var bad = Instantiate(_badEnemyPrefab, transform);
            bad.Initialize(this, badData);
            bad.gameObject.SetActive(false);
            _availableBadEnemies.Enqueue(bad);
        }
    }

    public void ReturnEnemyToPool(EnemyController enemy)
    {
        _availableEnemies.Enqueue(enemy);
    }

    public void ReturnRareEnemyToPool(RareEnemyController enemy)
    {
        _availableRareEnemies.Enqueue(enemy);
    }
    public void ReturnBadEnemyToPool(BadEnemyController enemy)
    {
        _availableBadEnemies.Enqueue(enemy);
    }
    private void SpawnEnemy()
    {
        var spawnPosition = new Vector3(Random.Range(-6.5f, 6.5f), Random.Range(-1f, 3f), 0);
        var enemy = _availableEnemies.Dequeue();
        enemy.transform.position = spawnPosition;
        enemy.gameObject.SetActive(true);
    }

    private void SpawnRareEnemy()
    {
        var spawnPosition = new Vector3(Random.Range(-6.5f, 6.5f), Random.Range(-1f, 3f), 0);
        var enemy = _availableRareEnemies.Dequeue();
        enemy.transform.position = spawnPosition;
        enemy.gameObject.SetActive(true);
    }
    private void SpawnBadEnemy()
    {
        var spawnPosition = new Vector3(Random.Range(-6.5f, 6.5f), Random.Range(-1f, 3f), 0);
        var enemy = _availableBadEnemies.Dequeue();
        enemy.transform.position = spawnPosition;
        enemy.gameObject.SetActive(true);
    }

    private void SpawnRares()
    {
        InvokeRepeating(nameof(SpawnRareEnemy), 1f, _rareSpawnInterval);
    }

    private void SpawnBads()
    {
        InvokeRepeating(nameof(SpawnBadEnemy), 1f, _badSpawnInterval);
    }

}
