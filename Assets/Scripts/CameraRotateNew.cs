using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotateNew : MonoBehaviour
{

    [SerializeField] private Camera cam;
    [SerializeField] private Transform target;
    
    private Vector3 PreviousPosition;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;


    private void FixedUpdate()
    {
        Vector3 limitPosition  = target.position + offset;
        Vector3 smoothPosition  = Vector3.Lerp(transform.position, limitPosition, smoothSpeed);
        transform.position = smoothPosition;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            PreviousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(1))
        {
            Vector3 direction = PreviousPosition - cam.ScreenToViewportPoint(Input.mousePosition);

            cam.transform.position = target.position;
            
            cam.transform.Rotate(new Vector3(1,0,0), direction.y *180);
            cam.transform.Rotate(new Vector3(0,1,0), -direction.x *180,Space.World);
            cam.transform.Translate(new Vector3(0, 0, -5));

            PreviousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        }
        

    }
    
    
}
