using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameButtons : MonoBehaviour
{
    [SerializeField] private GameObject _winGame;
    [SerializeField] private GameObject _loseGame;
    [SerializeField] private GameObject _pauseGame;

    private void Start()
    {
        Time.timeScale = 1;
    }

    public void RestartLevelButton()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void MenuButton()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void WinGameBehavior()
    {
        _winGame.SetActive(true);
    }

    public void LoseGameBehavior()
    {
        _loseGame.SetActive(true);
    }

    public void OpenPause()
    {
        _pauseGame.SetActive(true);
        Time.timeScale = 0;
    }

    public void ClosePause()
    {
        Time.timeScale = 1;
        _pauseGame.SetActive(false);
    }
}