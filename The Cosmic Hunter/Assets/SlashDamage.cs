using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashDamage : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "enemy")
        {
            if (this.gameObject.name == "Slasheffect")
            {
                enemyScript es = collision.gameObject.GetComponent<enemyScript>();
                es.TakeDamage(1, 5);
            }
            else if (this.gameObject.name == "Pusheffect")
            {
                enemyScript es = collision.gameObject.GetComponent<enemyScript>();
                es.TakeDamage(1, 7);
            }
        }
    }
}
