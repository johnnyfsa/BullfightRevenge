using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveWhenOutOfBounds : MonoBehaviour
{
    [SerializeField] private float maxDistance = 17.0f;

    private void Update()
    {
        CheckBoundaries();
    }

    private void CheckBoundaries()
    {
        if (transform.position.magnitude > maxDistance)
        {
            SpawnManager.Instance.ProjectilePool.ReturnObjectToPool(gameObject);
            return;
        }

    }
}
