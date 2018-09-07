using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newObjectPooler : MonoBehaviour {
    public GameObject pooledObject;
    GameObject[] objects = null;
    public int poolsize;
    // Use this for initialization
    void Start() {
        objects = new GameObject[poolsize];
        for (int i = 0; i < poolsize; i++)
        {
            objects[i] = Instantiate(pooledObject) as GameObject;
            objects[i].transform.parent = gameObject.transform;
            objects[i].SetActive(false);

        }
    }
    public void ActivateObject()
    {

        for (int i = 0; i < poolsize; i++)
        {
            if (objects[i].activeInHierarchy == false)
            {
                objects[i].SetActive(true);
                return;
            }
        }
    }
}
