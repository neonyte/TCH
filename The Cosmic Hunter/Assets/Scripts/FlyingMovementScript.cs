using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingMovementScript : MonoBehaviour {

    Transform munia;
    enemyScript flying;
    public Animator anim;
    public GameObject bullet;

    void Start () {
        munia = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        flying = gameObject.GetComponent<enemyScript>();
        InvokeRepeating("Shoot",3,3);
        anim = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        if (flying.willMove == true && flying.canMove == true && flying.health >0)
        {
            transform.position = Vector2.MoveTowards(transform.position, munia.position, flying.moveSpeed * Time.deltaTime);
        }
        if (flying.dieTrigger == true)
        {
            anim.SetTrigger("Death");
            
            flying.rb.freezeRotation = false;
            flying.rb.gravityScale = 1;
            flying.rb.AddTorque(Random.Range(-40f,40f));
            flying.toAttack = false;
            StartCoroutine(DeathDelay());
        }
        
    }
    public IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(0.01f);
        flying.dieTrigger = false;
    }
    void Shoot()
    {
        if (flying.triggerColliding == true && flying.health >1)
        {
            anim.SetTrigger("Attack");
            StartCoroutine(BulletDelay(0.5f));
            StartCoroutine(flying.MoveDelay(1.5f));
        }
    }
    public IEnumerator BulletDelay(float seconds)
    {
        
        yield return new WaitForSeconds(seconds);
        bullet.SetActive(true);
    }

}
