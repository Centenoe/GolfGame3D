using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public string methodName;
    public float dog = 1;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.gameObject.CompareTag("Player"))
        {
            collision.transform.gameObject.SendMessage(methodName, dog, SendMessageOptions.DontRequireReceiver);
        }
        Destroy(this.gameObject);
    }
    
}
