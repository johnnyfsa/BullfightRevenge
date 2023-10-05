using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public void Explode()
    {
        SpawnManager.Instance.EnemyPool.ReturnObjectToPool(gameObject);
    }
}
