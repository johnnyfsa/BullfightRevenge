using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class FXController : MonoBehaviour
{
    private ObjectPool<FXController> _pool;
    ParticleSystem _particle;
    public void Init(ObjectPool<FXController> pool)
    {
        _pool = pool;
        _particle = GetComponent<ParticleSystem>();
        this.gameObject.SetActive(true);
        StartCoroutine(Execute());
    }


    IEnumerator Execute()
    {
        yield return new WaitForSeconds(1f);
        _pool.Release(this);
    }


}
