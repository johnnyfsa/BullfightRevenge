using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemeySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemySwordThrowerPrefab;
    [SerializeField] GameObject enemyRodeoClownPrefab;

    private int zone; //zone 0:Top, 1:Right, 2:Bottom, 3:Left
    private Vector3 spawnPosition;
    private float spawnRotation;

    private void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
    }

    public void SpawnEnemy()
    {
        zone = Random.Range(0, 3);
        switch (zone)
        {
            case 0:
                spawnPosition = new Vector3(Random.Range(-10, 10), 0, 10);
                spawnRotation = 180.0f;
                SetEnemyType(Random.Range(0, 1));
                break;
            case 1:
                spawnPosition = new Vector3(10, 0, Random.Range(-10, 10));
                spawnRotation = -90.0f;
                SetEnemyType(Random.Range(0, 1));
                break;
            case 2:
                spawnPosition = new Vector3(Random.Range(-10, 10), 0, -10);
                spawnRotation = 0.0f;
                SetEnemyType(Random.Range(0, 2));
                break;
            case 3:
                spawnPosition = new Vector3(-10, 0, Random.Range(-10, 10));
                spawnRotation = 90.0f;
                SetEnemyType(Random.Range(0, 2));
                break;
        }
    }

    public void SetEnemyType(int enemyType)
    {
        switch (enemyType)
        {
            case 0:
                GameObject enemy = Instantiate(enemySwordThrowerPrefab);
                enemy.transform.position = spawnPosition;
                enemy.transform.rotation = Quaternion.Euler(0, spawnRotation, 0);
                SpawnManager.Instance.EnemyPool.SetObjectToPool(enemy);
                break;
            case 1:
                GameObject enemy2 = Instantiate(enemyRodeoClownPrefab);
                enemy2.transform.position = spawnPosition;
                SpawnManager.Instance.EnemyPool.SetObjectToPool(enemy2);
                break;
        }

    }
    IEnumerator SpawnEnemyRoutine()
    {
        while (true)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(5);
        }
    }
}
