using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : MonoBehaviour {

    Animator anim;
    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }
    void Update () {
		if (Input.GetKeyDown(KeyCode.Y)) anim.SetTrigger("Attack");
        if (Input.GetKeyDown(KeyCode.G)) anim.SetTrigger("Idle");
        if (Input.GetKeyDown(KeyCode.H)) anim.SetTrigger("Death");
        if (Input.GetKeyDown(KeyCode.J)) anim.SetTrigger("Walk");
    }
}
