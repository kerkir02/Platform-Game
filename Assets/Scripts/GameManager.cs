using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.SocialPlatforms.Impl;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int score = 0;
    private int saveScore;
    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        MenuScore();
    }
    public void MenuScore()
    {
        score = 0;
        saveScore = 0;
    }

    public void SetSaveScore()
    {
        saveScore = score;
    }
    public void LoadSaveScore()
    {
        score = saveScore;
    }
    public string ScoreUpdate(int value)
    {
        score += value;
        if (score < 0)
        {
            score = 0;
        }
        return "Score:" + ScoreZeros() + score;
    }
    private string ScoreZeros()
    {
        string zeros = "";
        for (int i = 6; i > Mathf.Abs(score).ToString().Length; i--)
        {
            zeros += "0";
        }
        return zeros;
    }
}
