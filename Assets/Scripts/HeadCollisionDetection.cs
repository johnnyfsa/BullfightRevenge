using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadCollisionDetection : MonoBehaviour
{
    private float headbuttDistance = 1.0f;

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, headbuttDistance))
        {
            if(hit.transform.TryGetComponent(out Enemy enemy))
            {
                enemy.Explode();
            }
            
        }
    }
}
