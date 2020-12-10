using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Continue : MonoBehaviour
{
    //text box of continue menu
    [SerializeField] private Text parText;
    //int variable thisHole denotes which hole is being played from 0-11 (holes 1 through 12, excluding tutorial);
    int thisHole;
    //array stores par for each hole
    int[] pars = {4,7,6,6,10,4,6,16,10,30,20,20};

    // Start is called before the first frame update
    void Start()
    {
        thisHole = SceneManager.GetActiveScene().buildIndex - 2;
        Debug.Log("par is " + pars[thisHole]);
    }

    // Update is called once per frame
    void Update()
    {
        //find par
        //send comparison between current score and par to contunue menu
        if(Scoring.currentScore == pars[thisHole])
        {
            parText.text = "Par";
        }
        else if(Scoring.currentScore < pars[thisHole])
        {
            parText.text = "Better than Par";
        }
        else
        {
            parText.text = "Worse than Par";
        }
    }
}
