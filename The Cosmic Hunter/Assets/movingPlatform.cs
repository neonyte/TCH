using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingPlatform : MonoBehaviour {

    private Vector3 posA;
    private Vector3 posB;
    private Vector3 nextPos;

    public float speed;

    public Transform childTransform;

    public Transform transformB;

	void Start () {
        posA = childTransform.localPosition;
        posB = transformB.localPosition;
        nextPos = posB;
	}
	

	void Update () {
        Move();
	}
    private void Move() {
        childTransform.localPosition = Vector3.MoveTowards(childTransform.localPosition, nextPos, speed*Time.deltaTime);
        if (Vector3.Distance(childTransform.localPosition, nextPos) <= 0.1)
        {
            ChangeDestination();
        }

    }
    private void ChangeDestination() {
        nextPos = nextPos != posA ? posA : posB;
    }

}
