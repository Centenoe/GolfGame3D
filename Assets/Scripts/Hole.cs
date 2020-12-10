using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.UI;

//AUTHORS: Charlie Christakos, Chazong Lor
public class Hole : MonoBehaviour
{
    
    //int variable thisHole denotes which hole is being played from 0-11 (holes 1 through 12, excluding tutorial);
    private int thisHole;
    //array stores par for each hole
    private int[] pars = { 4, 7, 6, 6, 10, 4, 6, 16, 10, 30, 20, 20 };

    //name of next scene (eg: level2, submitScore, Leaderboards) 
    //public string sceneName;

    public GameObject continueMenu;

    private float holeTimer = 1;

    private Boolean inHole = false;

    public void LoadConMenu()
    {
        continueMenu.SetActive(true);
        Time.timeScale = 0;
    }

    //If ball in hole, load next scene
    void OnTriggerEnter(Collider ball)
    {
        inHole = true;
        //checks if the object in the hole is the ball
        if(ball.gameObject.tag == "Player")
        {
            //loads next scene
            //SceneManager.LoadScene(sceneName);
            //Tells you what you scored at each hole in the console
           
             //Returns the score of the current hole to zero, because you are starting a new hole.
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        thisHole = SceneManager.GetActiveScene().buildIndex - 2;
        Debug.Log("par is " + pars[thisHole]);
    }

    // Update is called once per frame
    void Update()
    {
        //wait a second before opening continue menue
        if(inHole == true && holeTimer > 0)
        {
            holeTimer -= Time.deltaTime;
        }
        else if(inHole == true && holeTimer <= 0)
        {
            LoadConMenu();
            inHole = false;
            holeTimer = 1;
        }

    }

}
