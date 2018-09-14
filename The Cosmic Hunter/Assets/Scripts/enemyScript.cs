using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : MonoBehaviour {

    bool facingRight = false;
    Animator anim;
    //BoxCollider2D box;
    PolygonCollider2D circle;
    public int health;

    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        //box = gameObject.GetComponent<BoxCollider2D>();
        circle = gameObject.GetComponent<PolygonCollider2D>();
    }
    void Update () {

    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "Munia")
        {
            
            anim.SetTrigger("Attack");
            var player = col.gameObject.GetComponent<MuniaControllerScript>();

            if (player.transform.position.x < transform.position.x)
            {
                if (facingRight == true)
                {
                    Flip();
                }
                player.knockFromRight = true;
            }
            else
            {
                player.knockFromRight = false;
                if (facingRight == false)
                {
                    Flip();
                }
            }
            player.KnockBack();
        }
    }
    public void TakeDamage()
    {
        Debug.Log("Damage taken");
        health -= 1;
        if (health == 0)
        {
            anim.SetTrigger("Death");
            //box.enabled = false;
            circle.enabled = false;
            Destroy(gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length+0.5f);
        }
    }
    void Flip() //direction facing
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
