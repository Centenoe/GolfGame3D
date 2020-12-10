using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBoxCamera : MonoBehaviour
{
    [SerializeField] private Transform mainCamera;

    [SerializeField] private float skyboxScale;
    // Update is called once per frame
    private void LateUpdate()
    {
        transform.rotation = mainCamera.rotation;
        transform.localPosition = mainCamera.position / skyboxScale;
    }
}
