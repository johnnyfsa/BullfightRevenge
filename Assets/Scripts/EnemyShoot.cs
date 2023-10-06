using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    Vector3 offset = new Vector3(0, 1.82f, 1.13f);
    [SerializeField] float shotCooldown = 1;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Shoot(shotCooldown));
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Shoot(float shotCooldown)
    {
        while (true)
        {
            yield return new WaitForSeconds(shotCooldown);
            projectilePrefab = SpawnManager.Instance.ProjectilePool.GetPooledObject();
            projectilePrefab.transform.position = transform.TransformPoint(offset);
            projectilePrefab.transform.rotation = transform.rotation;
        }

    }
}
