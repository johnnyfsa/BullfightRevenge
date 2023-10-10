using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] List<Enemy> enemies;
    private ObjectPool<Enemy> _enemyPool;

    [SerializeField] float spawnTimer = 1.0f;

    void Awake()
    {
        _enemyPool = new ObjectPool<Enemy>(SpawnEnemy, null, OnReturnToPool, defaultCapacity: 20);
    }

    private void Start()
    {
        StartCoroutine(StartSpawning());
    }

    private void OnReturnToPool(Enemy enemy)
    {
        enemy.gameObject.SetActive(false);
    }

    private Enemy SpawnEnemy()
    {
        Enemy enemy;
        float enemyRotation;
        Vector3 enemyPos;
        //decide which enemy to spawn
        int enemyType = UnityEngine.Random.Range(0, enemies.Count);
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
                enemy.Init(_enemyPool);
                break;
            //top
            case 1:
                enemyPos = new Vector3(UnityEngine.Random.Range(-10, 10), enemy.transform.position.y, 10.0f);
                enemyRotation = 180.0f;
                enemy.transform.position = enemyPos;
                enemy.transform.rotation = Quaternion.Euler(0, enemyRotation, 0);
                enemy.Init(_enemyPool);
                break;
            //right
            case 2:
                enemyPos = new Vector3(10, enemy.transform.position.y, UnityEngine.Random.Range(-10, 10));
                enemyRotation = -90.0f;
                enemy.transform.position = enemyPos;
                enemy.transform.rotation = Quaternion.Euler(0, enemyRotation, 0);
                enemy.Init(_enemyPool);
                break;
            //bottom
            case 3:
                enemyPos = new Vector3(UnityEngine.Random.Range(-10, 10), enemy.transform.position.y, -10.0f);
                enemyRotation = 0;
                enemy.transform.position = enemyPos;
                enemy.transform.rotation = Quaternion.Euler(0, enemyRotation, 0);
                enemy.Init(_enemyPool);
                break;

        }
        //return enemy spawned
        return enemy;
    }

    IEnumerator StartSpawning()
    {
        while (true)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnTimer);
        }

    }

}
