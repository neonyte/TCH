using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class resetLevel : MonoBehaviour {
    public GameObject GameOver;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Munia")
        {
            GameOver.SetActive(true);
        }
    }
    
    
    
}
