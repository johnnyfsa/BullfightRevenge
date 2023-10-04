using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class EnemyMoveSideways : MonoBehaviour
{

    [SerializeField] float speed = 1f;

    private bool canMove = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        speed *= -1.0f;
    }




}
