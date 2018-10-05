using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class MuniaControllerScript : MonoBehaviour {
    public GameObject GameOverScreen;
    public float maxSpeed = 10f;
    Rigidbody2D rb;
    Animator anim;
    public SpriteRenderer psprite;


    public AudioClip[] attackClips = new AudioClip[0];
    public AudioClip[] jumpClips = new AudioClip[0];
    public AudioClip[] hurtClips = new AudioClip[0];
    public AudioSource voiceSource;
    public AudioSource voiceSwish;
    int index;
    public AudioSource lol;
    public AudioSource sfx;
    AudioEchoFilter echoes;

    public bool facingRight = true;
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
    public Transform corner1;
    public Transform corner2;

    public LayerMask whatIsEnemy;
    public CameraShake cameraShake;
    public GameObject attack1Effect;
    public GameObject attack2Effect;
    public GameObject attack3Effect;
    bool isBlocking = false;
    public GameObject forcefield;
    public GameObject bigSlash;

    public float knockback;
    public bool knockFromRight;
    bool knockbackBool = false;

    public float dashWindow = 0.2f;
    int leftTotal = 0;
    float leftTimeDelay = 0;
    int rightTotal = 0;
    float rightTimeDelay = 0;
    public float dashSpeed;
    bool dashing;
    float dashCooldown =2;
    float nextDash = 0;
    public GameObject party;
    float blockCooldown = 2;
    float nextBlock = 0;
    bool didBlock = false;
    float nextSwish = 0;
    float swishCooldown = 2;
    public Image blockFill;
    float blockFillTimeStamp;
    public Image swishFill;
    float swishFillTimeStamp;

    public Image attackFill;
    float attackFillTimeStamp;

    public float maxHealth = 6;
    public float currHealth = 6;
    public float maxMana = 100;
    public float currMana = 0;
    public float blockManaRate = 0.3f;
    public float ManaRegenRate = 0.1f;
    public float dashCost = 15;
    public float smashCost = 25;
    public float swishCost = 25;
    public bool isAttacking;

    public int attackState = 0;
    float comboTimer = 0;
    bool setComboTimer = false;
    public float comboWindow = 0.4f;
    public float reAttackWindow = 0.35f;
    float reAttackTimer = 0;
    bool setAttackTimer = false;
    float nextAttack;
    public float attackDelay = 1;

    public int shardsCollected = 0;
    public Image[] shards = new Image[3];
    public Image ImageHealth;
    public Image ImageMana;
    float ratio;
    public float lerpSpeed = 4;

    public static MuniaControllerScript instance;
    public GameObject ObjectPool;

    Color notsoRed = new Color(1, 0.5f, 0.5f, 1);
    Color[] flashbetween = { Color.white, Color.white };


    public Text whatever;
    //ult: jumpForce, dashCooldown
    private void Awake()
    {
        instance = this;
        Physics2D.IgnoreLayerCollision(8, 10, false);
        Physics2D.IgnoreLayerCollision(8, 14, false);
    }
    void Start () {
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        psprite = gameObject.GetComponent<SpriteRenderer>();
        echoes = gameObject.GetComponent<AudioEchoFilter>();
        echoes.enabled = false;
        flashbetween[0] = Color.white;
        flashbetween[1] = notsoRed;
        for (int i = 0; i<shards.Length; i++)
        {
            shards[i].enabled = false;
        }
        InvokeRepeating("RegenMana", 0, ManaRegenRate);
        InvokeRepeating("BlockingMana", 0, blockManaRate);
    }
    IEnumerator Desu()
    {
        GameOverScreen.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        Instantiate(party, transform.position, Quaternion.identity);
        gameObject.SetActive(false);

    }
    void RegenMana()
    {
        if (currMana <= 100)
        {
            currMana += 1f;
        }
    }
    void BlockingMana()
    {
        if (isBlocking == true && currMana > 0)
        currMana -= 1;
    }

	void Update () {
        if (Input.GetKeyDown(KeyCode.Q)) { currMana += 10; }

        if (currMana > 100) currMana = 100;
        if (currHealth > maxHealth) currHealth = maxHealth;
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack") || anim.GetCurrentAnimatorStateInfo(0).IsName("Attack 2") || anim.GetCurrentAnimatorStateInfo(0).IsName("Attack 3"))
        {
            isAttacking = true;
        }
        else
        {
            isAttacking = false;

        }
        
        ratio = currHealth / maxHealth;
        if (currHealth >= 0)
        {
            if (ImageHealth.fillAmount != ratio)
            {
                ImageHealth.fillAmount = Mathf.Lerp(ImageHealth.fillAmount, ratio, Time.deltaTime*lerpSpeed);
                ImageHealth.color = Color.Lerp(Color.red, Color.green, ratio);
            }
        }
        if (currHealth <= 0) {
            lol.Play();
            currHealth = 0;
            StartCoroutine(Desu());
            
        }
        
        ImageMana.fillAmount = currMana/maxMana;
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);
        if (isGrounded == true)
        {
            anim.SetBool("isGrounded", true);
        }
        if (isAttacking == false) //not working if attacking
        {
            if (isBlocking == false)
            {
                if (isGrounded == true && CrossPlatformInputManager.GetButtonDown("Jump"))
                { // jump
                    isJumping = true;
                    jumpTimeCounter = jumpTime;
                    rb.velocity = Vector2.up * jumpForce;
                    index = Random.Range(0, jumpClips.Length);
                    voiceSource.clip = jumpClips[index];
                    voiceSource.Play();
                    sfx.clip = Resources.Load<AudioClip>("SoundMusic/jumps");
                    sfx.Play();
                }
            }

                if (CrossPlatformInputManager.GetButton("Jump") && isJumping == true) // jump higher
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
                if (CrossPlatformInputManager.GetButtonUp("Jump"))
                {
                    isJumping = false;
                }
                if (CrossPlatformInputManager.GetButtonDown("Ult")) //ult
                {
                    if (ultActive == false)
                    {
                        ultActive = true;
                    }
                    else
                    {
                        ultActive = false;
                    }
                }
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Swish"))               //SWISH
            {
                voiceSwish.Stop();
                if (CrossPlatformInputManager.GetButtonDown("Swish") && CanSpendMana(swishCost) == true && Time.time > nextSwish) 
                {

                    jumpTimeCounter = 0;
                    rb.velocity = new Vector2(rb.velocity.x, 0);
                    voiceSwish.Play();
                    anim.SetTrigger("swish");
                    
                }
            }
        }
        if (setAttackTimer == true) reAttackTimer += Time.deltaTime;            //ATTACK
        if (reAttackTimer >= reAttackWindow)
        {
            setAttackTimer = false;
            setComboTimer = true;
            reAttackTimer = 0;
        }
        if (isAttacking == false && attackState == 3) attackState = 0;
        if (CrossPlatformInputManager.GetButtonDown("Attack"))                                // ATTACK
        {
            if (reAttackTimer == 0 && isAttacking ==false && Time.time > nextAttack)
            {
                setAttackTimer = true;
                if (attackState == 0)
                {
                    attackState = 1;
                    Attack1();
                    
                    
                }
            }
                if (comboTimer > 0 && comboTimer <= comboWindow && attackState == 1)
                {
                SlashDisappear();
                    attackState = 2;
                    comboTimer = 0;
                setComboTimer = false;
                reAttackTimer = 0;
                setAttackTimer = true;

                Attack2();
                    anim.SetBool("attackQueue", true);
                
            }
        }
       
        if (CrossPlatformInputManager.GetButtonDown("Special"))
        {

            if (comboTimer > 0 && comboTimer <= comboWindow && attackState == 2 && CanSpendMana(smashCost) == true)
            {
                    SlashDisappear();
                    PushDisappear();
                    attackState = 3;
                    setComboTimer = false;
                    comboTimer = 0;
                    Attack3();
                    anim.SetBool("attackQueue", true);
                
            }
        }

        if (setComboTimer == true)
        {
            comboTimer += Time.deltaTime;
        }
        if (comboTimer > comboWindow)
        {
            nextAttack = Time.time + attackDelay;
            attackFillTimeStamp = Time.time;
            setComboTimer = false;
            comboTimer = 0;
            if(attackState!=3) attackState = 0;
            setAttackTimer = false;
            reAttackTimer = 0;
            anim.SetBool("attackQueue", false);
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
            dashCooldown = 0.5f;
            echoes.enabled = true;
            if (rb.velocity.magnitude > 0)
            {
                newObjectPooler activate = GameObject.Find("ObjectPooler").GetComponent<newObjectPooler>();
                activate.ActivateObject();
                
            }
        }
        else
        {
            dashCooldown = 2;
            echoes.enabled = false;
        }
        if (isBlocking == false) {
            if (isAttacking == false) //left and right
            {
                
                if (knockbackBool == false)
                {
                    if (dashing == false)
                    {
                        float move = CrossPlatformInputManager.GetAxis("Horizontal");
                        anim.SetFloat("Speed", Mathf.Abs(move));

                        rb.velocity = new Vector2(move * maxSpeed, rb.velocity.y);

                        if (move > 0 && !facingRight)
                            Flip();
                        else if (move < 0 && facingRight)
                            Flip();
                    }
                    if (Time.time > nextDash && CanSpendMana(dashCost) == true)
                    {
                        if (CrossPlatformInputManager.GetButtonDown("Right")) { rightTotal += 1; }
                        if (rightTotal == 1 && rightTimeDelay < dashWindow) { rightTimeDelay += Time.deltaTime; }
                        if (rightTotal == 1 && rightTimeDelay >= dashWindow)
                        {
                            rightTimeDelay = 0;
                            rightTotal = 0;
                        }
                        if (rightTotal == 2 && rightTimeDelay < dashWindow)
                        {
                            anim.SetTrigger("dash");
                            dashing = true;
                            rb.velocity = new Vector2(dashSpeed, rb.velocity.y);
                            rightTotal = 0;
                            currMana -= dashCost;
                            jumpTimeCounter = 0;
                            nextDash = Time.time + dashCooldown;
                            StopCoroutine(DashStun());
                            StartCoroutine(DashStun());
                        }
                        if (CrossPlatformInputManager.GetButtonDown("Left")) { leftTotal += 1; }
                        if (leftTotal == 1 && leftTimeDelay < dashWindow) { leftTimeDelay += Time.deltaTime; }
                        if (leftTotal == 1 && leftTimeDelay >= dashWindow)
                        {
                            leftTimeDelay = 0;
                            leftTotal = 0;
                        }
                        if (leftTotal == 2 && leftTimeDelay < dashWindow)
                        {
                            anim.SetTrigger("dash");
                            dashing = true;
                            rb.velocity = new Vector2(-dashSpeed, rb.velocity.y);
                            leftTotal = 0;
                            currMana -= dashCost;
                            jumpTimeCounter = 0;
                            nextDash = Time.time + dashCooldown;
                            StopCoroutine(DashStun());
                            StartCoroutine(DashStun());
                        }
                    }
                }
            }
        }
        
        if (isAttacking == false)
        {
            SlashDisappear();
            PushDisappear();
        }
        if (Time.time > nextBlock && CanSpendMana(5) == true)                       //BLOCK
        {
            if (CrossPlatformInputManager.GetButton("Block"))
            {
                isBlocking = true;
                jumpTimeCounter = 0;
                anim.SetBool("isBlocking", true);
                didBlock = true;
            }
        }

        if (CrossPlatformInputManager.GetButtonUp("Block") || currMana <5)
        {
            isBlocking = false;
            anim.SetBool("isBlocking", false);
            if (didBlock == true)
            {
                nextBlock = Time.time + blockCooldown;
                blockFillTimeStamp = Time.time;
            }
            didBlock = false;

        }
        if (isBlocking == true)
        {
            forcefield.SetActive(true);
            rb.velocity = new Vector2(rb.velocity.x / 2, rb.velocity.y);

        }
        else
        {
            forcefield.SetActive(false);
        }
        
        if (Time.time > nextBlock) blockFill.fillAmount = 0;
        else blockFill.fillAmount = (nextBlock - Time.time) / (nextBlock-blockFillTimeStamp);

        if (Time.time > nextSwish) swishFill.fillAmount = 0;
        else swishFill.fillAmount = (nextSwish - Time.time) / (nextSwish - swishFillTimeStamp);

        if (Time.time > nextAttack) attackFill.fillAmount = 0;
        else attackFill.fillAmount = (nextAttack - Time.time) / (nextAttack - attackFillTimeStamp);

        whatever.text = " // " + attackState + " // " +comboTimer + " //  " + reAttackTimer + " // "+Time.time;

        //END OF UPDATE
        //END OF UPDATE
        //END OF UPDATE
    }
    
    public bool CanSpendMana(float cost)
    {
        if (cost < currMana)
        {
            return true;
        }
        else return false;
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
    void Attack1() {
        anim.SetTrigger("attack");
        jumpTimeCounter = 0;
        index = Random.Range(0, attackClips.Length);
        voiceSource.clip = attackClips[index];
        voiceSource.Play();
        sfx.clip = Resources.Load<AudioClip>("SoundMusic/whoosh3");
        sfx.Play();
        rb.velocity = new Vector2(rb.velocity.x/2, rb.velocity.y);
    }
    void Attack2()
    {
        anim.SetTrigger("attack2");
        jumpTimeCounter = 0;
        index = Random.Range(0, attackClips.Length);
        voiceSource.clip = attackClips[index];
        voiceSource.Play();
        sfx.clip = Resources.Load<AudioClip>("SoundMusic/whoosh3");
        sfx.Play();
        rb.velocity = new Vector2(rb.velocity.x / 2, rb.velocity.y);
    }
    void Attack3()
    {
        currMana -= smashCost;
        anim.SetTrigger("attack3");
        jumpTimeCounter = 0;
        index = Random.Range(0, attackClips.Length);
        voiceSource.clip = attackClips[index];
        voiceSource.Play();
        sfx.clip = Resources.Load<AudioClip>("SoundMusic/whoosh3");
        sfx.Play();
        rb.velocity = new Vector2(rb.velocity.x / 2, rb.velocity.y);

        
    }
    public void SlashAppear()
    {
        attack1Effect.SetActive(true);

        comboTimer = 0;
        setComboTimer = false;
    }
    public void SlashDisappear()
    {
        attack1Effect.SetActive(false);

    }
    public void PushAppear()
    {
        attack2Effect.SetActive(true);

    }
    public void PushDisappear()
    {
        attack2Effect.SetActive(false);

    }
    public void SmashAppear() {
        attackState = 3;
        attack3Effect.SetActive(true);
        attack3Effect.GetComponent<AudioSource>().Play();
        comboTimer = 0;
        setComboTimer = false;
        StartCoroutine(cameraShake.Shake(.4f, .4f));
    }
    public void SwishAppear()
    {
        sfx.clip = Resources.Load<AudioClip>("SoundMusic/whoosh3");
        sfx.Play();
        currMana -= swishCost;
        bigSlash.SetActive(true);
        nextSwish = Time.time + swishCooldown;
        swishFillTimeStamp = Time.time;
    }
    public void StartComboTimer() { setComboTimer = true; }


    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "enemy" || col.gameObject.tag == "trap" || col.gameObject.tag == "bullet")
        {
            if (isAttacking == true && attackState == 3) return;
            if (col.gameObject.transform.position.x > transform.position.x) knockFromRight = true;
            else knockFromRight = false;
            Hurt();

            StartCoroutine(FlashDamage());
        }
    }
    IEnumerator FlashDamage()
    {
        float elapsedTime = 0f;
        int index = 0;
        while (elapsedTime < 0.2f)
        {

            psprite.color = flashbetween[index % 2];

            elapsedTime += Time.deltaTime;
            index++;
            yield return new WaitForSeconds(0.07f);
        }
        psprite.color = Color.white;


    }
    public void Hurt()
    {
        setComboTimer = false;
        comboTimer = 0;
        if (attackState != 3) attackState = 0;
        setAttackTimer = false;
        reAttackTimer = 0;
        anim.SetBool("attackQueue", false);
        SlashDisappear();
        PushDisappear();
        currHealth -= 1;
        ratio = currHealth / maxHealth;
        index = Random.Range(0,hurtClips.Length);
        voiceSource.clip = hurtClips[index];
        voiceSource.Play();
        knockbackBool = true;
        anim.SetTrigger("hurt");
        if (knockFromRight == true)
        {
            rb.velocity = new Vector2(-knockback, knockback);
        }
        else
        {
            rb.velocity = new Vector2(knockback, knockback);
        }

        StartCoroutine(KnockbackStun());
    }
    
    IEnumerator KnockbackStun() {
        Physics2D.IgnoreLayerCollision(8, 10, true);
        Physics2D.IgnoreLayerCollision(8, 14, true);
        yield return new WaitForSeconds(0.5f);
        knockbackBool = false;
        Physics2D.IgnoreLayerCollision(8, 10, false);
        Physics2D.IgnoreLayerCollision(8, 14, false);
    }
    IEnumerator DashStun()
    {
        Physics2D.IgnoreLayerCollision(8, 10, true);
        yield return new WaitForSeconds(0.2f);
        rb.velocity = new Vector2(rb.velocity.x / 2, rb.velocity.y);
        yield return new WaitForSeconds(0.1f);
        dashing = false;
        yield return new WaitForSeconds(1f);
        Physics2D.IgnoreLayerCollision(8, 10, false);
    }
    
    public void ShardCtr() {
        if (shardsCollected < 3)
        {
            shards[shardsCollected].enabled = true;
            shardsCollected += 1;
            
        }
    }
}
