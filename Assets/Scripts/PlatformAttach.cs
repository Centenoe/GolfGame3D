using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Author: Edgar Centeno
//Make sure the ball is stuck to the moving platform
public class PlatformAttach : MonoBehaviour
{

    public GameObject Player;

    //if ball collides with moving platform
    private void OnTriggerEnter(Collider other)
    {
        //make the moving platform the parent and move with it
        if (other.gameObject == Player)
        {
            Player.transform.parent = transform;
        }
    }

    //once the it exists moving platform
    private void OnTriggerExit(Collider other)
    {
        //deselect the parent so it is no longer moving with the moving platform
        if (other.gameObject == Player)
        {
            Player.transform.parent = null;
        }
    }
}
