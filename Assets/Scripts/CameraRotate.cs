using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    public float Speed = 5;
    public bool RotateAroundPlayer = true;
    void LateUpdate()
    {

        //if (RotateAroundPlayer)
        //{
        //    Quaternion camTurnAngle = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * Speed, Vector3.up);
            
      //  }
        if (Input.GetMouseButton(1))
        {
            transform.eulerAngles += Speed * new Vector3(0, Input.GetAxis("Mouse X"), 0);
        }
    }
}
