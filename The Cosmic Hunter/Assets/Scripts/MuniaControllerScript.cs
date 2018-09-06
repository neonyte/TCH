using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuniaControllerScript : MonoBehaviour {
    public float maxSpeed = 10f;
    Rigidbody2D rb;
    Animator anim;
    public SpriteRenderer psprite;
    bool facingRight = true;

    bool isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;
    bool isJumping;
    public float jumpForce;
    float jumpTimeCounter;
    public float jumpTime;
    public static MuniaControllerScript instance;

    public ObjectPooler theObjectPool;

    private void Awake()
    {
        instance = this;
    }
    void Start () {
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        
	}
    void Flip() {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

	void Update () {
        psprite = gameObject.GetComponent<SpriteRenderer>();
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);
        if (isGrounded == true)
        {
            anim.SetBool("isGrounded", true);
        }
        if (isGrounded == true && Input.GetKeyDown(KeyCode.Space)) {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;
        }
        if (Input.GetKey(KeyCode.Space) && isJumping == true) {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else {
                isJumping = false;
            }
        }
        if (Input.GetKeyUp(KeyCode.Space)) {
            isJumping = false;
        }
        if (rb.velocity.y != 0)
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
        if (rb.velocity.magnitude > 0)
        {
            GameObject newGhost = theObjectPool.GetPooledObject();
            newGhost.transform.position = transform.position;
            newGhost.transform.rotation = transform.rotation;
            
            newGhost.SetActive(true);
            
        }
        
    }

    void FixedUpdate()
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
