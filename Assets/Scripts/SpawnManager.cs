using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UIElements;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] List<Enemy> enemies;
    [SerializeField] List<PowerUp> powerUps;
    [SerializeField] FXController explosionPrefab;
    private ObjectPool<Enemy> _enemyPool;
    private ObjectPool<PowerUp> _powerUpPool;
    private ObjectPool<FXController> _explosionPool;
    [SerializeField] float enemySpawnTimer = 1.0f;
    [SerializeField] float powerUpSpawnTimer = 2.0f;
    [SerializeField] int MaxNumEnemies = 20;
    [SerializeField] int MaxNumPowerUps = 5;

    private int previousEnemyType;
    private int timesEnemyRepeated = 0;
    private List<int> previousPowerups = new List<int>();

    public float EnemySpawnTimer { get => enemySpawnTimer; set => enemySpawnTimer = value; }

    void Awake()
    {
        GameManager.Instance.OnDifficultyChange += ChangeEnemySpawnTimer;
        GameManager.Instance.OnGameOver += StopSpawning;
        _enemyPool = new ObjectPool<Enemy>(SpawnEnemy, OnTakeEnemyFromPool, OnReturnEnemyToPool, defaultCapacity: 20);
        _powerUpPool = new ObjectPool<PowerUp>(SpawnPowerUp, OnTakePowerUpFromPool, OnReturnPowerUpToPool, defaultCapacity: 5);
        _explosionPool = new ObjectPool<FXController>(SpawnExplosion, OnTakeExplosionFromPool, OnReturnExplosionToPool, defaultCapacity: 20);
        previousEnemyType = -1;
    }

    private void StopSpawning()
    {
        this.gameObject.SetActive(false);
    }

    private void ChangeEnemySpawnTimer(float spawnTimer)
    {
        enemySpawnTimer = spawnTimer;
    }

    private void OnDestroy()
    {
        _enemyPool.Dispose();
        _powerUpPool.Dispose();
        _explosionPool.Dispose();
        GameManager.Instance.OnGameOver -= StopSpawning;
        GameManager.Instance.OnDifficultyChange -= ChangeEnemySpawnTimer;
    }

    private void OnTakeExplosionFromPool(FXController controller)
    {
        controller.gameObject.SetActive(true);
    }

    private void OnTakePowerUpFromPool(PowerUp up)
    {
        up.gameObject.SetActive(true);
    }

    private void OnTakeEnemyFromPool(Enemy enemy)
    {
        enemy.gameObject.SetActive(true);
    }

    private void OnReturnExplosionToPool(FXController controller)
    {
        controller.gameObject.SetActive(false);
    }

    private FXController SpawnExplosion()
    {
        FXController explosion = Instantiate(explosionPrefab);
        return explosion;
    }

    private void OnReturnPowerUpToPool(PowerUp powerUp)
    {
        powerUp.gameObject.SetActive(false);
    }

    private PowerUp SpawnPowerUp()
    {
        int powerUpToSpawn = GetUniquePowerUp();
        StorePreviousPowerUp(powerUpToSpawn);
        Vector3 powerUpPosition = new Vector3(UnityEngine.Random.Range(-9f, 10f), 0.3f, UnityEngine.Random.Range(-9f, 10f));
        PowerUp powerUp = Instantiate(powerUps[powerUpToSpawn]);
        powerUp.transform.position = powerUpPosition;
        powerUp.Init(_powerUpPool);
        return powerUp;
    }

    private int GetUniquePowerUp()
    {
        int powerUpToSpawn = UnityEngine.Random.Range(0, powerUps.Count);
        while (previousPowerups.Contains(powerUpToSpawn))
        {
            powerUpToSpawn = UnityEngine.Random.Range(0, powerUps.Count);
        }
        return powerUpToSpawn;
    }

    private void StorePreviousPowerUp(int powerUp)
    {
        previousPowerups.Add(powerUp);
        if (previousPowerups.Count > powerUps.Count - 1)
        {
            previousPowerups.RemoveAt(0);
        }
    }

    private void Start()
    {
        StartCoroutine(StartEnemySpawn());
        StartCoroutine(StartPowerUpSpawn());
    }

    private void OnReturnEnemyToPool(Enemy enemy)
    {
        enemy.gameObject.SetActive(false);
    }

    private int AvoidEnemyRepetition(int enemyType, int timesAllowedToRepeat)
    {
        //evade the enemy to repeat itself too many times
        if (enemyType == previousEnemyType)
        {
            timesEnemyRepeated++;
        }
        if (timesEnemyRepeated >= timesAllowedToRepeat)
        {
            while (enemyType == previousEnemyType)
            {
                enemyType = UnityEngine.Random.Range(0, enemies.Count);
            }
            timesEnemyRepeated = 0;
        }
        previousEnemyType = enemyType;
        return enemyType;
    }

    private Enemy SpawnEnemy()
    {
        Enemy enemy;
        float enemyRotation;
        Vector3 enemyPos;
        //decide which enemy to spawn
        int enemyType = UnityEngine.Random.Range(0, enemies.Count);
        enemyType = AvoidEnemyRepetition(enemyType, 3);
        //instantiate enemy
        switch (enemyType)
        {
            case 1:
                enemy = Instantiate(enemies[1]);
                break;
            default:
                enemy = Instantiate(enemies[0]);
                break;
        }
        //decide where to spawn enemy
        int location = UnityEngine.Random.Range(0, 4); //0 = left, 1 = top, 2 = right, 3 = bottom
        switch (location)
        {
            //left
            case 0:
                enemyPos = new Vector3(-10.0f, enemy.transform.position.y, UnityEngine.Random.Range(-10, 10));
                enemyRotation = 90.0f;
                enemy.transform.position = enemyPos;
                enemy.transform.rotation = Quaternion.Euler(0, enemyRotation, 0);
                enemy.Init(_enemyPool, _explosionPool);
                break;
            //top
            case 1:
                enemyPos = new Vector3(UnityEngine.Random.Range(-10, 10), enemy.transform.position.y, 10.0f);
                enemyRotation = 180.0f;
                enemy.transform.position = enemyPos;
                enemy.transform.rotation = Quaternion.Euler(0, enemyRotation, 0);
                enemy.Init(_enemyPool, _explosionPool);
                break;
            //right
            case 2:
                enemyPos = new Vector3(10, enemy.transform.position.y, UnityEngine.Random.Range(-10, 10));
                enemyRotation = -90.0f;
                enemy.transform.position = enemyPos;
                enemy.transform.rotation = Quaternion.Euler(0, enemyRotation, 0);
                enemy.Init(_enemyPool, _explosionPool);
                break;
            //bottom
            case 3:
                enemyPos = new Vector3(UnityEngine.Random.Range(-10, 10), enemy.transform.position.y, -10.0f);
                enemyRotation = 0;
                enemy.transform.position = enemyPos;
                enemy.transform.rotation = Quaternion.Euler(0, enemyRotation, 0);
                enemy.Init(_enemyPool, _explosionPool);
                break;

        }
        //return enemy spawned
        return enemy;
    }

    IEnumerator StartEnemySpawn()
    {
        while (true)
        {
            if (_enemyPool.CountActive <= MaxNumEnemies)
            {
                _enemyPool.Get();
            }
            yield return new WaitForSeconds(EnemySpawnTimer);
        }

    }
    IEnumerator StartPowerUpSpawn()
    {
        while (true)
        {
            if (_powerUpPool.CountActive <= MaxNumPowerUps)
            {
                _powerUpPool.Get();
            }
            yield return new WaitForSeconds(powerUpSpawnTimer);
        }

    }
}
