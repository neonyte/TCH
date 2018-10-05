using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dummyMovementScript : MonoBehaviour {

    enemyScript dummy;
    void Start()
    {
        dummy = gameObject.GetComponent<enemyScript>();
        
    }

    // Update is called once per frame
    void Update () {
        if (dummy.willMove == true && dummy.canMove ==true)
        {
                if (dummy.facingRight)
                {
                    dummy.rb.velocity = new Vector2(dummy.moveSpeed, 0);
                }
                else
                    dummy.rb.velocity = new Vector2(-dummy.moveSpeed, 0);
            
        }
    }
}
