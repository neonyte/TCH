using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public static bool gamePaused;
    public GameObject pauseCanvas;

    public void Pause()
    {
        pauseCanvas.SetActive(true);
        Time.timeScale = 0f;
    }
    public void Resume()
    {
        pauseCanvas.SetActive(false);
        Time.timeScale = 1f;
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }
    public void BacktoMainMenu()
    {
        SceneManager.LoadScene(1);
    }
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
}
