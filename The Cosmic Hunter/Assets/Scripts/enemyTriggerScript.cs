using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyTriggerScript : MonoBehaviour {
    enemyScript dummy;
    public GameObject munia;
    void Start () {
        dummy = transform.parent.gameObject.GetComponent<enemyScript>();
        munia = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject == munia)
        {
            dummy.triggerColliding = true;
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject == munia)
        {
            dummy.triggerColliding = false;
        }
    }
}
