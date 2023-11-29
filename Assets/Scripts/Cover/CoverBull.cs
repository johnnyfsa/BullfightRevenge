using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverBull : MonoBehaviour
{
    public event Action OnBullDestroyed;
    private Camera mainCamera;
    private float distance;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cameraPos = mainCamera.transform.position;
        Vector3 bullPos = transform.position;
        distance = Vector3.Distance(cameraPos, bullPos);

        if (distance < 2.0f)
        {
            OnBullDestroyed?.Invoke();
            Destroy(this.gameObject);
        }
    }
}
