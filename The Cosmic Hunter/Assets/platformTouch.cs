using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformTouch : MonoBehaviour {
    public Animator animPlayer;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Munia") {
            collision.collider.transform.SetParent(transform);
            animPlayer.SetBool("onMovingPlatform", true);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Munia")
        {
            collision.collider.transform.SetParent(null);
            animPlayer.SetBool("onMovingPlatform", false);
        }
    }
}
