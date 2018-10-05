using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongEffect : MonoBehaviour {
    public Transform pos;
    MuniaControllerScript muniaS;
    Rigidbody2D rb;
    bool faceRight = true;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        muniaS = GameObject.FindGameObjectWithTag("Player").GetComponent<MuniaControllerScript>();
    }
    void OnEnable()
    {
        transform.position = pos.transform.position;
        if (this.gameObject.name == "BigSlash")
        {
            if (muniaS.facingRight == true)
            {
                if (faceRight == false) Flip();
                rb.velocity = new Vector2(8f, 0);
            }
            else
            {
                if (faceRight == true) Flip();
                rb.velocity = new Vector2(-8f, 0);

            }
        }
        if (this.gameObject.name == "impacto")
        {
            StartCoroutine(Inactive(1));
        }
        if (this.gameObject.name == "BigSlash")
        {
            StartCoroutine(Inactive(1.2f));
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "enemy")
        {
            enemyScript es = collision.gameObject.GetComponent<enemyScript>();
            if (es.canBeSmashed == true)
            {
                if (this.gameObject.name == "impacto")
                {
                    es.TakeDamage(3, 7);
                    es.Smashing();
                }
                if (this.gameObject.name == "BigSlash")
                {
                    es.TakeDamage(2, 10);
                    es.Smashing();
                }
            }
        }
        if (collision.gameObject.name == "Bullet")
        {
            collision.gameObject.GetComponent<BulletScript>().Disappear(0.01f);
        }
    }
    void Flip() //direction facing
    {
        faceRight = !faceRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    IEnumerator Inactive(float timing)
    {
        yield return new WaitForSeconds(timing);
        gameObject.SetActive(false);
    }
}
