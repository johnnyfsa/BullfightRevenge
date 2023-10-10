using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;


public class Projectile : MonoBehaviour
{
    [SerializeField] private float maxDistance = 17.0f;
    private ObjectPool<Projectile> _pool;

    public void Init(ObjectPool<Projectile> poll)
    {  
       _pool = poll;
       gameObject.SetActive(true); 
    }
    private void Update()
    {
        CheckBoundaries();
    }

    private void CheckBoundaries()
    {
        if (transform.position.magnitude > maxDistance)
        {
            _pool.Release(this);
            return;
        }

    }
}
