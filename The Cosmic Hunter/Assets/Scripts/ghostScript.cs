using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghostScript : MonoBehaviour {
    SpriteRenderer sprite;
    float timer = 0.2f;

    void Start() {
        sprite = GetComponent<SpriteRenderer>();
        transform.position = MuniaControllerScript.instance.transform.position;
        transform.localScale = MuniaControllerScript.instance.transform.localScale;
        sprite.sprite = MuniaControllerScript.instance.psprite.sprite;
        sprite.color = new Vector4(50, 50, 50, 0.2f);
        StartCoroutine(Shine());
    }
    void OnEnable() { 
        transform.position = MuniaControllerScript.instance.transform.position;
        transform.localScale = MuniaControllerScript.instance.transform.localScale;
        sprite.sprite = MuniaControllerScript.instance.psprite.sprite;
        sprite.color = new Vector4(50, 50, 50, 0.2f);
        StartCoroutine(Shine());
    }
	IEnumerator Shine()
    {
        yield return new WaitForSeconds(timer);
        gameObject.SetActive(false);
    }
}
