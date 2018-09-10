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
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "Munia")
        {
            anim.SetTrigger("Attack");
            var player = col.gameObject.GetComponent<MuniaControllerScript>();
            player.knockbackCount = player.knockbackLength;

            if (player.transform.position.x < transform.position.x)
            {
                player.knockFromRight = true;
            }
            else
                player.knockFromRight = false;

        }
    }
    public void TakeDamage()
    {
        Debug.Log("Damage taken");
        anim.SetTrigger("Death");
    }
}
