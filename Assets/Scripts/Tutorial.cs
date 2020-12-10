using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.IO;
//Authors: Charlie Christakos, Edgar Centeno
public class Tutorial : MonoBehaviour
{
    [SerializeField] private Text guide;
    private Boolean rotated = false;
    private Boolean aimed = false;
    private Boolean shot = false;
    private Boolean powerup = false;
    

    // Update is called once per frame
    void Update()
    {
        //if player is not rotating and left click is not down
        if (!rotated && Input.GetMouseButtonDown(1))
        {
            rotated = true;
            guide.text = "Hold and drag Left Click to aim";
        }
        //if player has rotated aimed and pressed down right click
        if (rotated && !aimed && Input.GetMouseButtonDown(0))
        {
            aimed = true;
            guide.text = "Release Left Click to shoot";
        }
        //if player has rotated aimed and leased left click 
        if (rotated && aimed && Input.GetMouseButtonUp(0))
        {
            powerup = true;
            guide.text = "The cube is a power up that will give you 10% more power!";
            
        }
    }
}
