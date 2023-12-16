using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverEnemy : MonoBehaviour
{
    private const string NAMETAG = "CoverBull";
    [SerializeField] ParticleSystem ExplosionPrefab;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(NAMETAG))
        {
            AudioManager.Instance.PlaySFX(SoundType.EnemyHit);
            Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
