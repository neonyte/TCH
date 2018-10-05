using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyAnimator : MonoBehaviour {
    Animator anim;
    enemyScript dummy;
    void Start () {
        dummy = gameObject.GetComponent<enemyScript>();
        anim = gameObject.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        if (dummy.rb.velocity.magnitude > 0)
        {
            anim.SetBool("Walking", true);
        }
        else
            anim.SetBool("Walking", false);
        if (dummy.toAttack == true)
        {
            anim.SetTrigger("Attack");
            dummy.toAttack = false;
        }
        if (dummy.dieTrigger == true)
        {
            
            anim.SetTrigger("Death");
            dummy.toAttack = false;
            dummy.dieTrigger = false;
        }
    }
}
