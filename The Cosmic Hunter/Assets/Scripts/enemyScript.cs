using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : MonoBehaviour {

    bool facingRight = false;
    Animator anim;
    AudioSource esfx;
    Collider2D circle;
    public int health;
    public GameObject munia;
    MuniaControllerScript muniaScript;
    public static enemyScript instance;
    public bool triggerColliding = false;
    public float moveSpeed = 2f;
    public bool willMove = true;
    Rigidbody2D rb;
    SpriteRenderer spriteR;
    Shader shaderGUItext;
    Shader shaderSpritesDefault;
    Shader[] shaders = { null, null };

    private void Start()
    {
        instance = this;
        anim = gameObject.GetComponent<Animator>();
        esfx = gameObject.GetComponent<AudioSource>();
        circle = gameObject.GetComponent<Collider2D>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        munia = GameObject.FindGameObjectWithTag("Player");
        muniaScript = munia.GetComponent<MuniaControllerScript>();
        spriteR = GetComponent<SpriteRenderer>();
        Material yourMaterial = (Material)Resources.Load("Mater", typeof(Material));
        shaderGUItext = yourMaterial.shader;
        shaderSpritesDefault = spriteR.material.shader;
        shaders[0] = shaderGUItext;
        shaders[1] = shaderSpritesDefault;
    }
    private void Update()
    {

        if (rb.velocity.magnitude > 0)
        {
            anim.SetBool("Walking", true);
        }
        else
            anim.SetBool("Walking", false);

        if (triggerColliding == true && health > 0)
        {

            TriggerFlip();
        }
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject == munia)
        {
            
            anim.SetTrigger("Attack");
            muniaScript.KnockBack();
            esfx.clip = Resources.Load<AudioClip>("SoundMusic/punch1");
            esfx.Play();
            
            StartCoroutine(MoveDelay(1.5f));
        }
        if (col.gameObject.name.Equals("forcefield"))
        {
            StartCoroutine(MoveDelay(1f));
            StartCoroutine(Knockback(0,8f));
        }
    }
    IEnumerator MoveDelay(float seconds)
    {
        willMove = false;
        yield return new WaitForSeconds(seconds);
        willMove = true;
    }
    public void TakeDamage()
    {
        if (health > 0)
        {
            esfx.clip = Resources.Load<AudioClip>("SoundMusic/staby");
            esfx.Play();
            StartCoroutine(muniaScript.cameraShake.Shake(.15f, .4f));
        }
        health -= 1;
        if (health > 0)
        {
            StartCoroutine(MoveDelay(0.5f));
            StartCoroutine(Flashing());
            StartCoroutine(Knockback(0.2f,3f));
        }

        if (health == 0)
        {
            anim.SetTrigger("Death");
            StartCoroutine(muniaScript.cameraShake.Shake(.15f, .4f));
            willMove = false;
            StopAllCoroutines();
            spriteR.material.shader = shaderSpritesDefault;
            Physics2D.IgnoreCollision(circle, munia.GetComponent<Collider2D>(), true);
            
            Destroy(gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length+0.5f);
        }
    }
    public void TriggerFlip()
    {
        if (munia.transform.position.x < transform.position.x)
        {
            muniaScript.knockFromRight = true;
            if (facingRight == true)
            {
                Flip();
            }
            
        }
        else
        {
            muniaScript.knockFromRight = false;
            if (facingRight == false)
            {
                Flip();
            }
        }
        
        if (willMove == true && munia.activeInHierarchy == true) {
            if (facingRight)
            rb.velocity = new Vector2(moveSpeed, 0);
            else
            rb.velocity = new Vector2(-moveSpeed, 0);
        }
    }
    void Flip() //direction facing
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        StartCoroutine(MoveDelay(1f));
    }
    IEnumerator Flashing()
    {

        float elapsedTime = 0f;
        int index = 0;
        while (elapsedTime < 0.1f)
        {
            spriteR.material.shader = shaders[index % 2];
            elapsedTime += Time.deltaTime;
            index++;
            yield return new WaitForSeconds(0.07f);
        }
        spriteR.material.shader = shaderSpritesDefault;
    }
    IEnumerator Knockback(float seconds, float distance)
    {
        yield return new WaitForSeconds(seconds);
        if (facingRight)
            rb.velocity = new Vector2(-distance, 0);
        else
            rb.velocity = new Vector2(distance, 0);
    }
}
