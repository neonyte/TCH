using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class playMain : MonoBehaviour {
    public VideoPlayer vp;

	void Start () {
        vp = gameObject.GetComponent<VideoPlayer>();
        vp.loopPointReached += CheckOver;
    }

    void CheckOver(UnityEngine.Video.VideoPlayer vp)
    {
        SceneManager.LoadScene(1);
    }

}
