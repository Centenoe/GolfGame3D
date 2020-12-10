using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.IO;

//AUTHORS: Charlie Christakos
public class Leaderboard : MonoBehaviour
{
    string path;

    [SerializeField] private InputField inputField;
    [SerializeField] private Text board;
    [SerializeField] private Text score;
    [SerializeField] private Text error;
    [SerializeField] private Button submit;

    //array of scores
    private Score[] highScores = new Score[15];
    //lenght of highScores
    private int length = 0;

    public class Score 
    {
        public string name;
        public float score;
        public Score(string n, float s)
        {
            name = n;
            score = s;
        }
    }

    //return to main menu
    public void mainMenuButton()
    {
        SceneManager.LoadScene("Menu");
        Scoring.totalScore = 0;
    }
    
    //saves name and score into an array and then displays on leaderboards
    public void submitButton()
    {
        //error message if name contains comma
        if (inputField.text.Contains(","))
        {
            error.enabled = true;
        }
        else
        {
            submit.enabled = false;
            error.enabled = false;
            //bext value of Board.text
            String boardString = "";

            //create new score
            Score newScore = new Score(inputField.text, Scoring.totalScore);
            Debug.Log("new score created with name " + newScore.name + " and score " + newScore.score);
            //check if array is full
            if (length == 15)
            {
                Debug.Log("leaderboard full, replacing smallest score");
                //if array is full, replace worst score
                float worstScore = highScores[0].score;
                int worstIndex = 0;
                for (int i = 1; i < 15; i++)
                {
                    if (highScores[i].score >= worstScore)
                    {
                        worstScore = highScores[i].score;
                        worstIndex = i;
                    }
                }
                Debug.Log("worst score in leaderboard is " + highScores[worstIndex].name + " with score of " + highScores[worstIndex].score);
                highScores[worstIndex] = newScore;
            }
            else
            {
                Debug.Log("room left in leaderboard, adding score");
                //else just add score in next available space
                highScores[length] = newScore;
                length++;
            }

            //sort array from highest score to lowest
            Debug.Log("sorting leaderboard");
            for (int i = 0; i < length - 1; i++)
            {
                for (int j = i + 1; j > 0; j--)
                {
                    if (highScores[j - 1].score > highScores[j].score)
                    {
                        Score temp = highScores[j - 1];
                        highScores[j - 1] = highScores[j];
                        highScores[j] = temp;
                    }
                }
            }

            //rewrite file with highScores
            File.WriteAllText(path, String.Empty);
            string[] tempArray = new string[length];
            for (int i = 0; i < length; i++)
            {
                string nextString = highScores[i].name + "," + highScores[i].score;
                tempArray[i] = nextString;
                Debug.Log("score '" + nextString + "' will be written to file");
            }
            System.IO.File.WriteAllLines(path, tempArray);


            //display leaderboard (name: score)
            for (int i = 0; i < length; i++)
            {
                boardString = boardString + highScores[i].name + ": " + highScores[i].score + "\r\n";
            }
            board.text = boardString;
        }    
    }

    // Start is called before the first frame update
    void Start()
    {
        path = Application.dataPath + "/LeaderboardText.txt";

        score.text = "SCORE: " + Scoring.totalScore;

        //load leaderboard from file into array
        string[] boardLines = System.IO.File.ReadAllLines(path);
        foreach (string line in boardLines)
        {
            string[] scoreValues = line.Split(',');
            Score nextScore = new Score(scoreValues[0], float.Parse(scoreValues[1]));
            highScores[length] = nextScore;
            length++;
            Debug.Log("Name " + nextScore.name + " with score " + nextScore.score + "has been read from file");
        }

        //display leaderboard (name: score)
        string boardString = "";
        for (int i = 0; i < length; i++)
        {
            boardString = boardString + highScores[i].name + ": " + highScores[i].score + "\r\n";
        }
        board.text = boardString;
    }
}
