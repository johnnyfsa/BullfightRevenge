using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    Vector3 offset = new Vector3(0, 1.82f, 1.13f);

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Shoot());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Shoot()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            Instantiate(projectilePrefab, transform.TransformPoint(offset), transform.rotation);
        }

    }
}
