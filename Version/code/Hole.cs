using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//AUTHORS: Charlie Christakos,
public class Hole : MonoBehaviour
{
    //name of next scene (eg: level2, submitScore, Leaderboards) 
    public string sceneName;

    //If ball in hole, load next scene
    void OnTriggerEnter(Collider ball)
    {
        //checks if the object in the hole is the ball
        if(ball.gameObject.tag == "Player")
        {
            //loads next scene
            SceneManager.LoadScene(sceneName);
            //Tells you what you scored at each hole in the console
            if (Scoring.currentScore == 1) //If you scored in one hit at the hole you're on
            {
                Debug.Log("Hole in one! Way to go!"); //Messaging in the console telling you how you finished
            }
            else if (Scoring.currentScore == 2) //If you scored in two hits at the hole you're on
            {
                Debug.Log("Birdie! Great job!"); //Messaging in the console telling you how you finished
            }
            else if (Scoring.currentScore == 3) //If you scored in three hits at the hole you're on
            {
                Debug.Log("You made par! Good job!"); //Messaging in the console telling you how you finished
            }
            else if (Scoring.currentScore == 4) //If you scored in four hits at the hole you're on
            {
                Debug.Log("Bogey. Ouch!"); //Messaging in the console telling you how you finished
            } 
            else if (Scoring.currentScore == 5) //If you scored in five hits at the hole you're on
            {
                Debug.Log("Double Bogey. Yikes!"); //Messaging in the console telling you how you finished
            }
            else if (Scoring.currentScore == 6) //If you scored in six hits at the hole you're on
            {
                Debug.Log("Triple Bogey. Geez!"); //Messaging in the console telling you how you finished
            }
            else //If you scored in seven or more hits at the hole you're on
            {
                Debug.Log("There is no term for what you just scored. You suck!"); //Messaging in the console telling you how you finished
            }
            Scoring.currentScore = 0; //Returns the score of the current hole to zero, because you are starting a new hole.
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
