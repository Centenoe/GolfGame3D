using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author: Edgar Centeno
public class Pickup : MonoBehaviour
{
    public string methodName;
    public float power = 1;

    //do this on collision
    private void OnCollisionEnter(Collision collision)
    {
        //if the colission is from the player
        if (collision.transform.gameObject.CompareTag("Player"))
        {
            //send this to a method name that will be called in ballcontroller.cs
            collision.transform.gameObject.SendMessage(methodName, power, SendMessageOptions.DontRequireReceiver);
        }
        //destroy the object attached to the script. 
        Destroy(this.gameObject);
    }
    
}
