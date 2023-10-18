using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Enemy : MonoBehaviour
{
    [SerializeField] EnemyType enemyTypeSO;
    private ObjectPool<Enemy> _pool;
    private ObjectPool<FXController> _fxPool;


    public void Explode()
    {
        _pool.Release(this);
        var explosion = _fxPool.Get();
        explosion.transform.position = this.transform.position;
        explosion.Init(_fxPool);
        GameManager.Instance.ChangeScore(enemyTypeSO.score_Value);
    }

    internal void Init(ObjectPool<Enemy> pool, ObjectPool<FXController> fxPool)
    {
        _pool = pool;
        _fxPool = fxPool;
        this.gameObject.SetActive(true);
    }
}
