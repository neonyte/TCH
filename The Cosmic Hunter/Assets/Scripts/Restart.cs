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
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(2);
    }
    public void BacktoMainMenu()
    {
        SceneManager.LoadScene(1);
    }
}
