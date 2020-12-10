using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

/**This is a .cs file meant to alter the score seen on screen at each hole**/
public class ScoreVisual : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        //Updates the score seen in the top right of your screen to tell you your score at the current hole
        GetComponent<Text>().text = "" + Scoring.currentScore; 
    }
}
