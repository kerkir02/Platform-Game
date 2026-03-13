using AK.Wwise;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private List<AK.Wwise.Event> musicLibrary;

    public int score = 0;
    private int saveScore;
    private bool isSoundOn;
    private bool isMusicOn;
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
        isSoundOn = true;
        isMusicOn = false;
        Invoke(nameof(PlayMenuMusic), 0.5f);
        InvokeRepeating(nameof(IsMusicOn), 1f, 1f);
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
    public void ChangeSound()
    {
        isSoundOn = !isSoundOn;
    }
    public void ChangeMusic()
    {
        isMusicOn= !isMusicOn;
    }
    public void PlayMusic(int index)
    {
        if (isMusicOn)
        {
            StopMusic();
            musicLibrary[index].Post(gameObject);
        }
        else
        {
            StopMusic();
        }
    }
    public void StopMusic()
    {
        for (int i = 0; i < musicLibrary.Count; i++)
        {
            musicLibrary[i].Stop(gameObject);
        }
    }
    private void PlayMenuMusic()
    {
        isMusicOn = true;
        musicLibrary[0].Post(gameObject);
    }
    public bool IsMusicOn()
    {
        return isMusicOn;
    }
}
