using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spikeTrap : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.name.Equals("Munia"))
        {
            var player = coll.gameObject.GetComponent<MuniaControllerScript>();
            if (player.transform.position.x < transform.position.x)
            {
                player.knockFromRight = true;
            }
            else
            {
                player.knockFromRight = false;
            }
            player.KnockBack();
        }
    }
}
