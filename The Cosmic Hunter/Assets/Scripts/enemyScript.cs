using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : MonoBehaviour {

    public bool facingRight = false;
    AudioSource esfx;
    Collider2D circle;
    public int health;
    public GameObject munia;
    MuniaControllerScript muniaScript;
    public bool toAttack = false;

    public bool triggerColliding = false;
    public float moveSpeed = 2f;
    public bool canMove = true;
    public bool willMove = false;

    public Rigidbody2D rb;
    SpriteRenderer spriteR;
    Shader shaderGUItext;
    Shader shaderSpritesDefault;
    Shader[] shaders = { null, null };
    public bool canBeSmashed = true;
    public bool isDying = false;
    public bool dieTrigger = false;
    public float deathTime;


    private void Start()
    {
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
        
        if (canMove == true && munia.activeInHierarchy == true)
        {
            if (triggerColliding == true && health > 0)
            {
                TriggerFlip();

                willMove = true;
            }
        }
            if (triggerColliding == false && health > 0)
            {
                willMove = false;
                rb.velocity = new Vector2(0, 0);
            }
        
    }
        private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject == munia)
        {
            toAttack = true;
            
                if (muniaScript.attackState != 3)
            {

                esfx.clip = Resources.Load<AudioClip>("SoundMusic/punch1");
                esfx.Play();
            }
            StartCoroutine(MoveDelay(1.5f));
        }
        if (col.gameObject.name.Equals("forcefield") && health >0)
        {
            StartCoroutine(MoveDelay(1f));
            StartCoroutine(Knockback(0,8f));
        }
        
    }
    
    public IEnumerator MoveDelay(float seconds)
    {
        canMove = false;
        yield return new WaitForSeconds(seconds);
        canMove = true;
    }
    public void TakeDamage(int h, float knock)
    {
        if (health > 0)
        {
            esfx.clip = Resources.Load<AudioClip>("SoundMusic/staby");
            esfx.Play();
            StartCoroutine(muniaScript.cameraShake.Shake(.15f, .4f));
            health -= h;
        }
        
        if (health > 0)
        {
            StartCoroutine(MoveDelay(0.5f));
            StartCoroutine(Flashing());
            StopCoroutine(Knockback(0.2f,knock));
            StartCoroutine(Knockback(0.2f,knock));
        }
        if (health <= 0 && isDying == false)
        {
            isDying = true;
            dieTrigger = true;
            StartCoroutine(muniaScript.cameraShake.Shake(.15f, .4f));
            canMove = false;
            StopAllCoroutines();
            spriteR.material.shader = shaderSpritesDefault;
            Physics2D.IgnoreCollision(circle, munia.GetComponent<Collider2D>(), true);
            
            Destroy(gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length+deathTime);
        }
    }
    public void TriggerFlip()
    {
        
            if (munia.transform.position.x < transform.position.x)
        {
            
            if (facingRight == true)
            {
                Flip();
            }
            
        }
        else
        {
            
            if (facingRight == false)
            {
                Flip();
            }
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
    public void Smashing()
    {
        StartCoroutine(BigSlashed());
    }
    IEnumerator BigSlashed()
    {
        canBeSmashed = false;
        yield return new WaitForSeconds(1f);
        canBeSmashed = true;

    }
}
