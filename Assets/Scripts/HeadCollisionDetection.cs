using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadCollisionDetection : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {
        bool hit = Physics.Raycast(transform.position, transform.forward, 1.0f);
        if (hit)
        {
            Debug.Log("Hit");
        }
    }
}
