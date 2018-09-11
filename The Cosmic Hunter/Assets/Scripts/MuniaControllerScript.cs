﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuniaControllerScript : MonoBehaviour {
    public float maxSpeed = 10f;
    public Rigidbody2D rb;
    Animator anim;
    public SpriteRenderer psprite;
    

    public AudioClip[] attackClips = new AudioClip[0];
    public AudioClip[] jumpClips = new AudioClip[0];
    public AudioSource voiceSource;
    int index;
    public AudioSource lol;

    bool facingRight = true;
    bool isGrounded;
    bool ultActive = false;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;
    bool isJumping;
    public float jumpForce;
    float jumpTimeCounter;
    public float jumpTime;
    public Transform attackPos;
    public float attackRange;
    public LayerMask whatIsEnemy;
    public CameraShake cameraShake;
    public GameObject attackEffect;

    public float knockback;
    
    public bool knockFromRight;
    public bool knockbackBool = false;
    public int knockbackState = 0;

    public static MuniaControllerScript instance;

    public GameObject ObjectPool;

    private void Awake()
    {
        instance = this;
    }
    void Start () {
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
	}

	void Update () {

        psprite = gameObject.GetComponent<SpriteRenderer>();
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);
        if (isGrounded == true)
        {
            anim.SetBool("isGrounded", true);
        }
        if (isGrounded == true && Input.GetKeyDown(KeyCode.Space)) { // jump
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;
            index = Random.Range(0, jumpClips.Length);
            voiceSource.clip = jumpClips[index];
            voiceSource.Play();
        }
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack")) //not working if attacking
        {
            if (Input.GetKey(KeyCode.Space) && isJumping == true) // jump higher
            {
                if (jumpTimeCounter > 0)
                {
                    rb.velocity = Vector2.up * jumpForce;
                    jumpTimeCounter -= Time.deltaTime;
                }
                else
                {
                    isJumping = false;
                }
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                isJumping = false;
            }
            if (Input.GetKeyDown(KeyCode.U)) //ult
            {
                if (ultActive == false)
                {
                    ultActive = true;
                    
                    //lol.Play();
                }
                else
                {
                    ultActive = false;
                    //lol.Pause();
                }
            }
            if (Input.GetKeyDown(KeyCode.N)) //attack
            {
                AttackAndDamage();
            }
        }
        if (rb.velocity.y != 0) // jumping and falling animations
        {
            anim.SetBool("isGrounded", false);
            if (rb.velocity.y < 0)
            {
                anim.SetBool("isFalling", true);
            }
            else
            {
                anim.SetBool("isFalling", false);
            }
        }
        if (ultActive == true) //ult mechanics
        {
            if (rb.velocity.magnitude > 0)
            {
                newObjectPooler activate = GameObject.Find("ObjectPooler").GetComponent<newObjectPooler>();
                activate.ActivateObject();

            }
        }
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack")) //left and right
        {
            if (knockbackBool == false)
            {
                float move = Input.GetAxis("Horizontal");
                anim.SetFloat("Speed", Mathf.Abs(move));
                rb.velocity = new Vector2(move * maxSpeed, rb.velocity.y);

                if (move > 0 && !facingRight)
                    Flip();
                else if (move < 0 && facingRight)
                    Flip();
            }

        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
    void FixedUpdate()
    {
        
        
    }
    void Flip() //direction facing
    {
        facingRight = !facingRight; 
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    void AttackAndDamage() {
        anim.SetTrigger("attack");
        jumpTimeCounter = 0;
        index = Random.Range(0, attackClips.Length);
        voiceSource.clip = attackClips[index];
        voiceSource.Play();
        
    }
    public void KnockBack()
    {
        knockbackBool = true;
        if (knockFromRight == true)
        {
            rb.velocity = new Vector2(-knockback, knockback);
            //rb.AddForce(-transform.right*knockback);
            //rb.AddForce(transform.up * knockback);
        }
        else
        {
            rb.velocity = new Vector2(knockback, knockback);
            //rb.AddForce(transform.right * knockback);
            //rb.AddForce(transform.up * knockback);
        }
        
        StartCoroutine(KnockbackStun());
    }
    IEnumerator KnockbackStun() {
        yield return new WaitForSeconds(0.5f);
        knockbackBool = false;
    }
    public void Slash() {
        if (attackEffect.activeInHierarchy == true)
        {
            attackEffect.SetActive(false);
        }
        else
        {
            attackEffect.SetActive(true);
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemy);
            if (enemiesToDamage.Length > 0)
            {
                StartCoroutine(cameraShake.Shake(.15f, .4f));
            }
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                enemiesToDamage[i].GetComponent<enemyScript>().TakeDamage();

            }
        }
        }
}
