//Author: Edgar Centeno
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform target;

    //the higher the value the faster the camera will lock onto target
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    void FixedUpdate()
    {
        Vector3 limitPosition  = target.position + offset;
        Vector3 smoothPosition  = Vector3.Lerp(transform.position, limitPosition, smoothSpeed);
        transform.position = smoothPosition;
    }
}