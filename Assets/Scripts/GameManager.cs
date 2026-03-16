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
    [SerializeField] private AK.Wwise.Event jumpSound;
    [SerializeField] private AK.Wwise.Event hitSound;
    [SerializeField] private AK.Wwise.Event gameOverSound;
    [SerializeField] private AK.Wwise.Event hurtSound;
    [SerializeField] private AK.Wwise.Event coinSound;
    [SerializeField] private AK.Wwise.Event heartSound;
    [SerializeField] private AK.Wwise.Event gemSound;
    [SerializeField] private AK.Wwise.Event winSound;
    [SerializeField] private AK.Wwise.Event clickSound;

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
    public void PlayJumpSound()
    {
        if (isSoundOn) jumpSound.Post(gameObject);
    }
    public void PlayHitSound()
    {
        if (isSoundOn) hitSound.Post(gameObject);
    }
    public void PlayGameOverSound()
    {
        if (isSoundOn) gameOverSound.Post(gameObject);
    }
    public void PlayHurtSound()
    {
        if (isSoundOn) hurtSound.Post(gameObject);
    }
    public void PlayCoinSound()
    {
        if (isSoundOn) coinSound.Post(gameObject);
    }
    public void PlayHeartSound()
    {
        if (isSoundOn) heartSound.Post(gameObject);
    }
    public void PlayGemSound()
    {
        if (isSoundOn) gemSound.Post(gameObject);
    }
    public void PlayWinSound()
    {
        if (isSoundOn) winSound.Post(gameObject);
    }
    public void PlayClickSound()
    {
        if (isSoundOn) clickSound.Post(gameObject);
    }
}
