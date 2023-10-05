using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private static SpawnManager instance;

    private ObjectPool projectilePool;
    private ObjectPool enemyPool;

    public static SpawnManager Instance { get; private set; }
    public ObjectPool ProjectilePool { get => projectilePool; set => projectilePool = value; }
    public ObjectPool EnemyPool { get => enemyPool; set => enemyPool = value; }

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }
    private void Start()
    {
        ProjectilePool = GetComponents<ObjectPool>()[0];
        EnemyPool = GetComponents<ObjectPool>()[1];
    }
    
}
