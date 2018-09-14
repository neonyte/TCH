﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flashdamage : MonoBehaviour
{
    SpriteRenderer spriteR;
    Color[] flashbetween = { Color.white, Color.red };

    void Start()
    {
        spriteR = GetComponent<SpriteRenderer>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.tag == "enemy")
        {
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
