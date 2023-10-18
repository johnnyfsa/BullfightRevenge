using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Enemy : MonoBehaviour
{
    [SerializeField] EnemyType enemyTypeSO;
    private ObjectPool<Enemy> _pool;


    public void Explode()
    {
        _pool.Release(this);
        GameManager.Instance.ChangeScore(enemyTypeSO.score_Value);
    }

    internal void Init(ObjectPool<Enemy> pool)
    {
        _pool = pool;
        this.gameObject.SetActive(true);
    }
}
