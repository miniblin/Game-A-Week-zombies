using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreManager : MonoBehaviour
{

    private int score = 0;
    private int kills = 0;


    public Text scoreText;

    public Text killText;
    public Text highKillText;
    public Text highScoreText;

    static private ScoreManager _S;
    static public ScoreManager S
    {
        get
        {
            return _S;
        }
        set
        {
            if (_S != null)
            {
                // Debug.Log("you are attempting to set S singleton a second time");
            }
            _S = value;
        }
    }

    // Use this for initialization
    void Start()
    {
        S = this;
        S.scoreText.text = "0";
        S.highScoreText.text = CreateHighScoreString(PlayerPrefs.GetInt("High Score"), true);
        S.highKillText.text = CreateHighScoreString(PlayerPrefs.GetInt("High Kill"), true);
        InvokeRepeating("IncrementScore", 1f, 1f);
    }

    // Update is called once per frame
    public void IncrementScore()
    {
        // Debug.Log("Incrementing Score");
        S.score += 1;

        S.scoreText.text = CreateHighScoreString(S.score);
        if (S.score > PlayerPrefs.GetInt("High Score"))
        {
            PlayerPrefs.SetInt("High Score", S.score);
            S.highScoreText.text = CreateHighScoreString(PlayerPrefs.GetInt("High Score"), true);
        }

    }

    static public void IncrementKills()
    {
        // Debug.Log("Incrementing Score");
        S.kills += 1;

        S.killText.text = S.kills.ToString();
        if (S.kills > PlayerPrefs.GetInt("High Kill"))
        {
            PlayerPrefs.SetInt("High Kill", S.kills);
            S.highKillText.text = S.kills.ToString();
        }

    }

    public String CreateHighScoreString(int score, bool isHighScore = false)
    {
        return score.ToString();
    }
    public void SetHighScore()
    {

    }
}
