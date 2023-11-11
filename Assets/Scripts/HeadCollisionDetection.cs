using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadCollisionDetection : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            //Debug.Log("Head collided with enemy:" + enemy.name);
            enemy.Explode();
        }
    }
}
