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

    [SerializeField] private InputField inputField;
    [SerializeField] private Text board;
    [SerializeField] private Text score;

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

        //bext value of Board.text
        String boardString = "";

        //create new score
        Score newScore = new Score(inputField.text, Scoring.totalScore);

        //check if array is full
        if (length == 15)
        {
            //if array is full, replace smallest score
            float smallestScore = highScores[0].score;
            int smallestIndex = 0;
            for (int i = 1; i < 15; i++)
            {
                if (highScores[i].score <= smallestScore)
                {
                    smallestScore = highScores[i].score;
                    smallestIndex = i;
                }
            }
            highScores[smallestIndex] = newScore;
        }
        else
        {
            //else just add score in next available space
            highScores[length] = newScore;
            length++;
        }

        //sort array from highest score to lowest
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
        File.WriteAllText(@"Assets\TextFiles\LeaderboardText.txt", String.Empty);
        string[] tempArray = new string[length];
        for (int i = 0; i <length; i++)
        {
            string nextString = highScores[i].name + "," + highScores[i].score;
            tempArray[i] = nextString;
        }
        System.IO.File.WriteAllLines(@"Assets\TextFiles\LeaderboardText.txt", tempArray);


        //display leaderboard (name: score)
        for (int i = 0; i < length; i++)
        {
            boardString = boardString + highScores[i].name + ": " + highScores[i].score + "\r\n";
        }
        board.text = boardString;
    }

    // Start is called before the first frame update
    void Start()
    {
        score.text = "Score: " + Scoring.totalScore;



        //load leaderboard from file into array
        string[] boardLines = System.IO.File.ReadAllLines(@"Assets\TextFiles\LeaderboardText.txt");
        foreach (string line in boardLines)
        {
            string[] scoreValues = line.Split(',');
            Score nextScore = new Score(scoreValues[0], float.Parse(scoreValues[1]));
            highScores[length] = nextScore;
            length++;
        }

        //display leaderboard (name: score)
        string boardString = "";
        for (int i = 0; i < length; i++)
        {
            boardString = boardString + highScores[i].name + ": " + highScores[i].score + "\r\n";
        }
        board.text = boardString;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
