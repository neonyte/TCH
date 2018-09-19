using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour {
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartLevel();
        }
    }
    public void RestartLevel()
    {
        StartCoroutine(DelayRestart());
    }
    IEnumerator DelayRestart()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(1);
    }
}
