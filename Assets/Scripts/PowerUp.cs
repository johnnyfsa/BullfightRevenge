using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Pool;

public class PowerUp : MonoBehaviour
{
    protected ObjectPool<PowerUp> _pool;
    
    public virtual void Init(ObjectPool<PowerUp> pool)
    {
        _pool = pool;
    }

    public virtual void Activate(Player player)
    {

    }

    public virtual void DestroySelf()
    {
        _pool.Release(this);
    }
}
