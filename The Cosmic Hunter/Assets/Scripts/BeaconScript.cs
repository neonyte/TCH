using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeaconScript : MonoBehaviour {
    AudioSource esfx;
    public float maxHealth = 10;
    float currhealth;
    MuniaControllerScript muniaScript;
    SpriteRenderer spriteR;
    Shader shaderGUItext;
    Shader shaderSpritesDefault;
    Shader[] shaders = { null, null };
    float shakeAmount = 10.0f;
    bool dieTrigger = false;
    bool desuTrue = false;
    Vector2 currPos;
    public Image beaconHealth;
    float ratio;
    Animator anim;
    public GameObject party;
    public GameObject victoryScreen;


    void Start () {
        esfx = GetComponent<AudioSource>();
        muniaScript = GameObject.FindGameObjectWithTag("Player").GetComponent<MuniaControllerScript>();
        spriteR = GetComponent<SpriteRenderer>();
        Material yourMaterial = (Material)Resources.Load("Mater", typeof(Material));
        shaderGUItext = yourMaterial.shader;
        shaderSpritesDefault = spriteR.material.shader;
        shaders[0] = shaderGUItext;
        shaders[1] = shaderSpritesDefault;
        anim = GetComponent<Animator>();
        currhealth = maxHealth;
        currPos = transform.position;
    }
    private void Update()
    {
        ratio = currhealth / maxHealth;
        beaconHealth.fillAmount = ratio;
        if (dieTrigger == true)
        {
            anim.speed = 0;
            Vector2 newPos = Random.insideUnitCircle * (Time.deltaTime * shakeAmount);
            transform.position = currPos + newPos;
            if (desuTrue == true)
            {
                desuTrue = false;
                StartCoroutine(Desu());
            }
        }
        if (muniaScript.currHealth == 0) { StopAllCoroutines(); }
    }
    IEnumerator Desu()
    {
        yield return new WaitForSeconds(2.0f);
        Instantiate(party, transform.position, Quaternion.identity);
        victoryScreen.SetActive(true);
        yield return new WaitForSeconds(0.01f);
        gameObject.SetActive(false);
    }
    public void TakeDamage(int h)
    {
        if (currhealth > 0)
        {
            esfx.clip = Resources.Load<AudioClip>("SoundMusic/staby");
            esfx.Play();
            StartCoroutine(muniaScript.cameraShake.Shake(.15f, .4f));
            currhealth -= h;
            if (currhealth >0) StartCoroutine(Flashing());
        }
        if (currhealth <= 0 && dieTrigger == false)
        {
            dieTrigger = true;
            desuTrue = true;
        }
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
