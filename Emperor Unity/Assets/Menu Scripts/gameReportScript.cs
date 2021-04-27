using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class gameReportScript : MonoBehaviour
{
    public Text PlayerText;
    public Text ScoreText;
    public Text MovesTakenText;
    public Text TimeText;

    public string Player;
    public int Score;
    public int MovesTaken;
    public string Time;
    // Start is called before the first frame update

    void OnEnable()
    {
        Player = PlayerPrefs.GetString("username");
        Score = PlayerPrefs.GetInt("score");
        MovesTaken = PlayerPrefs.GetInt("moves");
        Time = PlayerPrefs.GetString("time");
    }
    void Start()
    {
        print("Player: " + Player + " Score: " + Score + " Moves: " + MovesTaken + " Time: " + Time);
        PlayerText.text = Player;
        ScoreText.text = Score.ToString();
        MovesTakenText.text = MovesTaken.ToString();
        TimeText.text = Time;
    }

    void OnDisable(){
        PlayerPrefs.DeleteAll();
    }
}
