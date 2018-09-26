using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

    public GameObject followThisObject;
    void OnEnable () {
        transform.position = followThisObject.transform.position;
    }
	void Update () {
        transform.position = followThisObject.transform.position;
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag.Equals("enemy"))
        {
            if (col.gameObject.GetComponent<enemyScript>().health <= 0)
            {
                Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), col.gameObject.GetComponent<Collider2D>(), true);
                
            }
        }
    }
}
