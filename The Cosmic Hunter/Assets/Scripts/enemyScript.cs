using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : MonoBehaviour {

    bool facingRight = false;
    Animator anim;
    AudioSource esfx;
    PolygonCollider2D circle;
    public int health;
    GameObject munia;
    MuniaControllerScript muniaScript;
    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        esfx = gameObject.GetComponent<AudioSource>();
        circle = gameObject.GetComponent<PolygonCollider2D>();
        munia = GameObject.FindGameObjectWithTag("Player");
        muniaScript = munia.GetComponent<MuniaControllerScript>();
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject == munia)
        {
            
            anim.SetTrigger("Attack");
            

            if (munia.transform.position.x < transform.position.x)
            {
                if (facingRight == true)
                {
                    Flip();
                }
                muniaScript.knockFromRight = true;
            }
            else
            {
                muniaScript.knockFromRight = false;
                if (facingRight == false)
                {
                    Flip();
                }
            }
            muniaScript.KnockBack();
            esfx.clip = Resources.Load<AudioClip>("SoundMusic/punch1");
            esfx.Play();
        }
    }
    public void TakeDamage()
    {
        esfx.clip = Resources.Load<AudioClip>("SoundMusic/stab3");
        esfx.Play();
        if (munia.transform.position.x < transform.position.x)
        {
            if (facingRight == true)
            {
                Flip();
            }
            muniaScript.knockFromRight = true;
        }
        else
        {
            muniaScript.knockFromRight = false;
            if (facingRight == false)
            {
                Flip();
            }
        }
        health -= 1;
        if (health == 0)
        {
            anim.SetTrigger("Death");
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
