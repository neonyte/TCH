using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shardCollect : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Munia")
        {
            MuniaControllerScript.instance.shardsCollected += 1;
            Destroy(gameObject);
        }
    }
}
