using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallTrap : MonoBehaviour {
    Rigidbody2D rb;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name.Equals("Munia"))
        {
            rb.isKinematic = false;
            StartCoroutine(Disappear());
        }
    }
    IEnumerator Disappear()
    {
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
    }
}
