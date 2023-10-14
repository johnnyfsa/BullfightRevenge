using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField] Projectile projectilePrefab;
    Vector3 offset = new Vector3(0, 1.82f, 1.13f);
    [SerializeField] float shotCooldown = 1;
    [SerializeField] float rotationSpeed = 5f;
    private ObjectPool<Projectile> _projectilePool;
    private Player player;

    void Awake()
    {
        _projectilePool = new ObjectPool<Projectile>(CreateProjectile, null, OnReturnToPool, defaultCapacity: 50);
    }
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        StartCoroutine(Shoot(shotCooldown));
    }

    // Update is called once per frame
    void Update()
    {
        TurnToPlayer();
    }

    private void TurnToPlayer()
    {
        Vector3 playerDirection = player.transform.position - transform.position;
        transform.forward = Vector3.Slerp(transform.forward, playerDirection, rotationSpeed * Time.deltaTime);
    }

    private Projectile CreateProjectile()
    {
        var projectile = Instantiate(projectilePrefab);
        return projectile;
    }

    private void OnReturnToPool(Projectile projectile)
    {
        projectile.gameObject.SetActive(false);
    }

    IEnumerator Shoot(float shotCooldown)
    {
        while (true)
        {
            yield return new WaitForSeconds(shotCooldown);
            var projectile = _projectilePool.Get();
            projectile.transform.position = transform.TransformPoint(offset);
            projectile.transform.rotation = transform.rotation;
            projectile.Init(_projectilePool);
        }

    }
}
