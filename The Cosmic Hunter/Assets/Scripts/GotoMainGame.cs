using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GotoMainGame : MonoBehaviour {

	// Update is called once per frame
	public void GotoGame () {
        SceneManager.LoadScene(2);
    }
}
