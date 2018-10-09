using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    public Transform pos;
    public float speed;
    Transform munia;
    Vector3 target;
    MuniaControllerScript muniaScript;
    Vector3 normalizeDirection;
    public bool noDirection = false;
    public GameObject bulletSplash;
    

    void Awake()
    {
        
        munia = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(munia.position.x, munia.position.y);
        muniaScript = munia.GetComponent<MuniaControllerScript>();
    }

    void Update()
    {
        if(!noDirection) transform.position += normalizeDirection * speed * Time.deltaTime;
        else
            transform.position -= normalizeDirection * speed * Time.deltaTime;
    }

    void OnEnable()
    {
        
        transform.position = pos.transform.position;
        target = new Vector3(munia.position.x, munia.position.y);
        normalizeDirection = (target - transform.position).normalized;
        noDirection = false;
        StartCoroutine(Inactive(2.9f));
    }
    private void OnDisable()
    {
        bulletSplash.SetActive(true);
    }
    IEnumerator Inactive(float timing)
    {
        yield return new WaitForSeconds(timing);
        gameObject.SetActive(false);
    }
    public void Disappear(float time)
    {
        noDirection = true;
        StartCoroutine(Inactive(time));
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject == munia.gameObject)
        {
            noDirection = true;
            StartCoroutine(Inactive(0.01f));
            
            if (muniaScript.attackState != 3)
            {
                
            }

        }
        if (col.gameObject.name.Equals("forcefield") || (col.gameObject.tag == "platform") || (col.gameObject.tag == "trap")) 
        {
            Disappear(0.01f);

        }

    }

}