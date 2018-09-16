using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuniaControllerScript : MonoBehaviour {
    public float maxSpeed = 10f;
    Rigidbody2D rb;
    Animator anim;
    public SpriteRenderer psprite;
    bool attackCollider = false;

    public AudioClip[] attackClips = new AudioClip[0];
    public AudioClip[] jumpClips = new AudioClip[0];
    public AudioClip[] hurtClips = new AudioClip[0];
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
    bool knockbackBool = false;

    int leftTotal = 0;
    float leftTimeDelay = 0;
    int rightTotal = 0;
    float rightTimeDelay = 0;
    public float dashSpeed;
    bool dashing;
    public float dashCooldown;
    float nextDash = 0;

    public float maxHealth = 3;
    public float currHealth = 3;
    public Text healthText;
    public int shardsCollected = 0;
    public GameObject party;
    public Image ImageHealth;
    

    public static MuniaControllerScript instance;
    public GameObject ObjectPool;

    

    private void Awake()
    {
        instance = this;
    }
    void Start () {
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        psprite = gameObject.GetComponent<SpriteRenderer>();
    }

	void Update () {

        healthText.text = "Health = " + currHealth + "   Shards = " + shardsCollected;
        if (currHealth == 0) {
            lol.Play();
            Instantiate(party, transform.position, Quaternion.identity);
            currHealth = -1;
            gameObject.SetActive(false);
            
        }

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
                if (dashing == false) {
                    float move = Input.GetAxis("Horizontal");
                    anim.SetFloat("Speed", Mathf.Abs(move));
                    rb.velocity = new Vector2(move * maxSpeed, rb.velocity.y);

                    if (move > 0 && !facingRight)
                        Flip();
                    else if (move < 0 && facingRight)
                        Flip();
                }
                if (Time.time > nextDash)
                {
                    if (Input.GetKeyDown(KeyCode.D)) { rightTotal += 1; }
                    if (rightTotal == 1 && rightTimeDelay < .4) { rightTimeDelay += Time.deltaTime; }
                    if (rightTotal == 1 && rightTimeDelay >= .4)
                    {
                        rightTimeDelay = 0;
                        rightTotal = 0;
                    }
                    if (rightTotal == 2 && rightTimeDelay < .4)
                    {
                        anim.SetTrigger("dash");
                        dashing = true;
                        rb.velocity = new Vector2(dashSpeed, rb.velocity.y);
                        rightTotal = 0;
                        Physics2D.IgnoreLayerCollision(8, 10, true);
                        jumpTimeCounter = 0;
                        nextDash = Time.time + dashCooldown;
                        StartCoroutine(DashStun());
                    }
                    if (Input.GetKeyDown(KeyCode.A)) { leftTotal += 1; }
                    if (leftTotal == 1 && leftTimeDelay < .4) { leftTimeDelay += Time.deltaTime; }
                    if (leftTotal == 1 && leftTimeDelay >= .4)
                    {
                        leftTimeDelay = 0;
                        leftTotal = 0;
                    }
                    if (leftTotal == 2 && leftTimeDelay < .4)
                    {
                        anim.SetTrigger("dash");
                        dashing = true;
                        rb.velocity = new Vector2(-dashSpeed, rb.velocity.y);
                        leftTotal = 0;
                        Physics2D.IgnoreLayerCollision(8, 10, true);
                        jumpTimeCounter = 0;
                        nextDash = Time.time + dashCooldown;
                        StartCoroutine(DashStun());
                    }
                }
            }

        }
        if (attackCollider == true)
        {
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemy);
            if (enemiesToDamage.Length > 0)
            {
                StartCoroutine(cameraShake.Shake(.15f, .4f));
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<enemyScript>().TakeDamage();
                    enemiesToDamage[i].GetComponent<flashDamageEnemy>().Damaged();
                }
                attackCollider = false;
            }
            
        }
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            attackEffect.SetActive(false);
            attackCollider = false;
        }
        //end of update
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
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
        rb.velocity = new Vector2(rb.velocity.x/2, rb.velocity.y);
    }
    public void Hurt()
    {
        index = Random.Range(0,hurtClips.Length);
        voiceSource.clip = hurtClips[index];
        voiceSource.Play();
        float ratio = currHealth / maxHealth;
        ImageHealth.rectTransform.localScale = new Vector3(ratio, 1, 1);
        Debug.Log(ratio);
    }
    public void KnockBack()
    {
        knockbackBool = true;
        anim.SetTrigger("hurt");
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
        Physics2D.IgnoreLayerCollision(8, 10, true);
        yield return new WaitForSeconds(0.5f);
        knockbackBool = false;
        Physics2D.IgnoreLayerCollision(8, 10, false);
        Debug.Log("knockback");
    }
    IEnumerator DashStun()
    {
        yield return new WaitForSeconds(0.2f);
        rb.velocity = new Vector2(rb.velocity.x / 2, rb.velocity.y);
        yield return new WaitForSeconds(0.1f);
        dashing = false;
        yield return new WaitForSeconds(0.3f);
        Physics2D.IgnoreLayerCollision(8, 10, false);
    }
    public void Slash() {
        if (attackEffect.activeInHierarchy == true)
        {
            attackEffect.SetActive(false);
            attackCollider = false;
        }
        else
        {
            attackEffect.SetActive(true);
            attackCollider = true;
        }

    }
}
