using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {
        //rotate the transform on the y axis by 1 degrees every frame 
        transform.Rotate(0, 1, 0);
    }
}
