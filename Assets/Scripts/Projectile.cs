using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;


public class Projectile : MonoBehaviour
{
    [SerializeField] private float maxDistance = 17.0f;
    [SerializeField] private int damage;
    private ObjectPool<Projectile> _pool;

    public int Damage { get => damage; set => damage = value; }

    public void Init(ObjectPool<Projectile> poll)
    {
        _pool = poll;
        gameObject.SetActive(true);
    }
    private void Update()
    {
        CheckBoundaries();
        DetectCollisionWithPlayer();
    }

    private void CheckBoundaries()
    {
        if (transform.position.magnitude > maxDistance)
        {
            DestroyProjectile();
        }

    }

    public void DestroyProjectile()
    {
        _pool.Release(this);
    }

    private void DetectCollisionWithPlayer()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 1.0f))
        {
            if (hit.collider.TryGetComponent<Player>(out Player player))
            {
                player.AddLives(-damage);
                DestroyProjectile();
            }
        }



    }
}
