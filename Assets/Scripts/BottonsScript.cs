using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BottonsScript : MonoBehaviour
{
    [SerializeField] private GameObject quickMenu;

    private GameManager gameManager;

    void Awake()
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
        quickMenu.SetActive(!quickMenu.activeSelf);
        /*if (quickMenu.activeSelf) {
            quickMenu.SetActive(false);
        }
        else
        {
            quickMenu.SetActive(true);
        }*/
    }
    public void MusicActive()
    {
        gameManager.ChangeMusic();
        gameManager.PlayMusic(SceneManager.GetActiveScene().buildIndex);
    }
    public void SoundActive()
    {
        gameManager.ChangeSound();
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(gameManager.IsMusicOn()) gameManager.PlayMusic(scene.buildIndex);
    }
    private void OnEnable()
    {
        // Rejestrujemy callback
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // Odłączamy przy wyłączeniu obiektu
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
