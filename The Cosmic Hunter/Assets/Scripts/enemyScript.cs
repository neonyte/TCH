using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : MonoBehaviour {

    Animator anim;
    BoxCollider2D box;
    CircleCollider2D circle;
    public int health;

    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        box = gameObject.GetComponent<BoxCollider2D>();
        circle = gameObject.GetComponent<CircleCollider2D>();
    }
    void Update () {

    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "Munia")
        {
            anim.SetTrigger("Attack");
            var player = col.gameObject.GetComponent<MuniaControllerScript>();
            player.knockbackBool = true;
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
        health -= 1;
        if (health == 0)
        {
            anim.SetTrigger("Death");
            box.enabled = false;
            circle.enabled = false;
            Destroy(gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length+0.5f);
        }
    }
}
