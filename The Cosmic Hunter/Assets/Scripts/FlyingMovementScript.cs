using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingMovementScript : MonoBehaviour {
    public float speed;
    Transform munia;
    enemyScript flying;

    public GameObject bullet;

    void Start () {
        munia = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        flying = gameObject.GetComponent<enemyScript>();
        InvokeRepeating("Shoot",0,3);
    }

    void Update()
    {
        if (flying.willMove == true && flying.canMove == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, munia.position, speed * Time.deltaTime);
        }
    }

    void Shoot()
    {
        if (flying.triggerColliding == true)
        {
            bullet.SetActive(true);
            StartCoroutine(flying.MoveDelay(1f));
        }
    }
}
