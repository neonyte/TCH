using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitCode : MonoBehaviour {
    GameObject flyingObject;
    public Transform orbitpt;
    enemyScript flying;
    Rigidbody2D rb;
    Animator anim;
    // Use this for initialization
    void Start () {
        flyingObject = transform.parent.gameObject;
        transform.position = flyingObject.transform.position;
        flying = flyingObject.GetComponent<enemyScript>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        if (flying.health>0) transform.position = orbitpt.transform.position;
        if (flying.dieTrigger == true)
        {
            transform.parent = null;
            rb.gravityScale = 1;
            anim.SetBool("Fall",true);
        }
        if (flyingObject == null) Destroy(this.gameObject);
    }
}
