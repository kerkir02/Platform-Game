using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BottonsScript : MonoBehaviour
{
    [SerializeField] GameObject quickMenu;

    GameManager gameManager;
    void Start()
    {
        gameManager = GameManager.Instance;
    }
    //Buttons management
    public void NextLevelLoad()
    {
        gameManager.SetSaveScore();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void LevelLoad(int level)
    {
        SceneManager.LoadScene(level);
    }
    public void RestartLevel()
    {
        gameManager.LoadSaveScore();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void LoadMenu()
    {
        gameManager.MenuScore();
        SceneManager.LoadScene(0);
    }
    public void QuickMenuOpen()
    {
        if (quickMenu.activeSelf) {
            quickMenu.SetActive(false);
        }
        else
        {
            quickMenu.SetActive(true);
        }
    }
    public void MusicActive()
    {

    }
    public void SoundActive()
    {

    }
}
