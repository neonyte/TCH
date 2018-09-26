using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flashDamageEnemy : MonoBehaviour {
    SpriteRenderer spriteR;
    Shader shaderGUItext;
    Shader shaderSpritesDefault;
    Shader[] shaders = { null, null };

    void Start()
    {
        spriteR = GetComponent<SpriteRenderer>();
        Material yourMaterial = (Material)Resources.Load("Mater", typeof(Material));
        shaderGUItext = yourMaterial.shader;
        shaderSpritesDefault = spriteR.material.shader;
        shaders[0] = shaderGUItext;
        shaders[1] = shaderSpritesDefault;

    }
    public void Damaged()
    {

        StartCoroutine(Flashing());
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
}
