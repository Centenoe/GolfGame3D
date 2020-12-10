using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//Author: Edgar Centeno
//This will indicate the the game is over and the ball has to restart. 
public class GameRestart : MonoBehaviour
{
    bool gameOver = false;
    public float restartDelay = 1f;

    
    //once the game is over due to the ball falling off the map it will restart the scene
    public void EndGame()
    {
        if (gameOver == false)
        {
            gameOver = true;
            
            Debug.Log("game restart");
            //set a deplay for the game to restart so it is not instant
            Invoke("Restart", restartDelay);
        }
    }

    //restart the scene
    void Restart()
    {
        //instead of hard coding the scene name you can use this to get the name 
        //of the active scene the player is on
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
