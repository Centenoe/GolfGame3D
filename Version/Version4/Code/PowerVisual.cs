using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using System;

/**This is a .cs file meant to alter the force seen on screen for each hit**/
public class PowerVisual : MonoBehaviour
{ 
    // Update is called once per frame
    void Update()
    {
        //Updates the force seen in the bottom right of your screen to tell you your force each hit
        GetComponent<Text>().text = "" + Math.Floor(BallController.outputForce) + "%";
    }
}
