using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flashdamage : MonoBehaviour
{
    SpriteRenderer spriteR;
    Color notsoRed = new Color(1,0.5f,0.5f,1);
    Color[] flashbetween = { Color.white,Color.white };
    

    void Start()
    {
        spriteR = GetComponent<SpriteRenderer>();
        flashbetween[0] = Color.white;
        flashbetween[1] = notsoRed;

    }

    void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.tag == "enemy")
        {
            MuniaControllerScript.instance.currHealth -= 1;
            MuniaControllerScript.instance.Hurt();
            
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
