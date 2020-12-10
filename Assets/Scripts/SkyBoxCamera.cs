using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Author: Edgar Centeno
public class SkyBoxCamera : MonoBehaviour
{
    [SerializeField] private Transform mainCamera;
    //you can set the scale of how fast the skybox moves 
    [SerializeField] private float skyboxScale;
    
    
    // Update is called once per frame
    private void LateUpdate()
    {
        //rotation of skybox and maincamera should be the same 
        transform.rotation = mainCamera.rotation;
        
        //this will make the movement of the camera to scale 
        transform.localPosition = mainCamera.position / skyboxScale;
    }
}
