using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallTrap : MonoBehaviour {
    Rigidbody2D rb;
    Collider2D cd;
    Collider2D mun;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        cd = GetComponent<Collider2D>();
	}
    public void Triggered(Collider2D col)
    { 
            mun = col;
            rb.isKinematic = false;
            
            StartCoroutine(Disappear());
    }
    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.name.Equals("Munia"))
        {
            
            Physics2D.IgnoreCollision(cd, mun, true);
            StartCoroutine(Disappear());
        }
    }
    IEnumerator Disappear()
    {
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
    }
}
