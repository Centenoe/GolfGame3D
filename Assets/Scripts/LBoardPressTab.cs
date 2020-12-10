using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.IO;

public class LBoardPressTab : MonoBehaviour
{
    [SerializeField] private Text board;

    //array of scores
    private Score[] highScores = new Score[15];
    //length of highScores
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

    // Start is called before the first frame update
    void Start()
    {
        //load leaderboard from file into array
        string[] boardLines = System.IO.File.ReadAllLines(@"Assets\LeaderboardText.txt");
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
}

