using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashDamage : MonoBehaviour {


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
        if (collision.gameObject.tag == "beacon")
        {
            BeaconScript bs = collision.gameObject.GetComponent<BeaconScript>();
            bs.TakeDamage(1);
        }
    }
}
