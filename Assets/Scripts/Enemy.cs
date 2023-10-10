using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Enemy : MonoBehaviour
{
    private ObjectPool<Enemy> _pool;
    public void Explode()
    {
        _pool.Release(this);
    }

    internal void Init(ObjectPool<Enemy> pool)
    {
        _pool = pool;
        this.gameObject.SetActive(true);
    }
}
