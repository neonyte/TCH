using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flashdamage : MonoBehaviour
{
    bool touchingEnemy;
    SpriteRenderer spriteR;
    Color[] flashbetween = { Color.white, Color.red };

    // Use this for initialization
    void Start()
    {
        spriteR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.tag == "enemy")
        {
            touchingEnemy = true;
            StartCoroutine(onCoroutine());

        }
    }
    IEnumerator onCoroutine()
    {
        float elapsedTime = 0f;
        int index = 0;
        while (elapsedTime < 0.2f)
        {

            spriteR.color = flashbetween[index % 2];

            elapsedTime += Time.deltaTime;
            index++;
            yield return new WaitForSeconds(0.07f);
        }
        spriteR.color = Color.white;
        
    }
}
