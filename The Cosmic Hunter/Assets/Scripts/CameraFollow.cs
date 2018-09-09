using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform target;
    Vector3 velocity = Vector3.zero;
    public float smoothTime = 0.15f;

    public bool YMaxEnabled = false;
    public float YMaxValue = 0;
    public bool YMinEnabled = false;
    public float YMinValue = 0;
    public bool XMaxEnabled = false;
    public float XMaxValue = 0;
    public bool XMinEnabled = false;
    public float XMinValue = 0;

    private void FixedUpdate()
    {
        Vector3 targetpos = target.position;

        if (YMinEnabled && YMaxEnabled)
            targetpos.y = Mathf.Clamp(target.position.y, YMinValue, YMaxValue);
        else if (YMinEnabled)
            targetpos.y = Mathf.Clamp(target.position.y, YMinValue, target.position.y);
        else if (YMaxEnabled)
            targetpos.y = Mathf.Clamp(target.position.y, target.position.y, YMaxValue);

        if (XMinEnabled && XMaxEnabled)
            targetpos.x = Mathf.Clamp(target.position.x, XMinValue, XMaxValue);
        else if (XMinEnabled)
            targetpos.x = Mathf.Clamp(target.position.x, XMinValue, target.position.x);
        else if (XMaxEnabled)
            targetpos.x = Mathf.Clamp(target.position.x, target.position.x, XMaxValue);


        targetpos.z = transform.position.z;
        transform.position = Vector3.SmoothDamp(transform.position, targetpos, ref velocity, smoothTime);
    }
}
