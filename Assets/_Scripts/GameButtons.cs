using UnityEngine;
using UnityEngine.SceneManagement;

public class GameButtons : MonoBehaviour
{
    [SerializeField] private GameObject _winGame;
    [SerializeField] private GameObject _loseGame;
    [SerializeField] private GameObject _pauseGame;
    private GameFeedbackManager _gameFeedbackManager;

    private void Start()
    {
        _gameFeedbackManager = GetComponent<GameFeedbackManager>();
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
        _gameFeedbackManager.PlayWinFeedback();
    }

    public void LoseGameBehavior()
    {
        _loseGame.SetActive(true);
        _gameFeedbackManager.PlayLoseFeedback();
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