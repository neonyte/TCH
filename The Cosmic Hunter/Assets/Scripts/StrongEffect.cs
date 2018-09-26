using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongEffect : MonoBehaviour {
    public Transform pos;
    void OnEnable()
    {
        transform.position = pos.transform.position;
    }
}
