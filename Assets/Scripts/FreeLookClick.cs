using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
//This script makes CinamachineFreeLook camera only move when right mouse mutton is clicked. 
[RequireComponent(typeof(CinemachineFreeLook))]
public class FreeLookClick : MonoBehaviour
{
    
    
    private CinemachineFreeLook freeLookCamera;
    

    // Start is called before the first frame update 
    // initialize freeLookCamera and set Xaxis and Yaxis to nothing   
    void Start()
    {
        
        //initialize the freecam from Cinemachine
        freeLookCamera = GetComponent<CinemachineFreeLook>();
        
        //set initial axis to nothing normally called XAXIS AND YAXIS 
        //by doing this you remove the default movement of FreeLookCamera and can set your own. 
        freeLookCamera.m_XAxis.m_InputAxisName = "";
        freeLookCamera.m_YAxis.m_InputAxisName = "";
    }

    
    // Update is called once per frame
    // Here we will set the FreeLook Camera to only move when right click is pressed. 
    void Update()
    {
        // If the left mouse button is pressed then get the mouse x and y axis
        if (Input.GetMouseButton(1))
        {
            freeLookCamera.m_XAxis.m_InputAxisValue = Input.GetAxis("Mouse X");
            freeLookCamera.m_YAxis.m_InputAxisValue = Input.GetAxis("Mouse Y");
        }
        //else set the the x and y axis values to 0
        else
        {
            freeLookCamera.m_XAxis.m_InputAxisValue = 0;
            freeLookCamera.m_YAxis.m_InputAxisValue = 0;
        }
    }
}