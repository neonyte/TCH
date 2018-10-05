using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSplash : MonoBehaviour {
    public GameObject bullet;

    private void OnEnable()
    {
        transform.position = bullet.transform.position;
        StartCoroutine(Disappear());
    }
    IEnumerator Disappear()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }
}
