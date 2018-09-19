using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shardCollect : MonoBehaviour {
    public AudioSource sfx;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Munia")
        {
            sfx.Play();
            MuniaControllerScript.instance.ShardCtr();
            Destroy(gameObject);
        }
    }
}
