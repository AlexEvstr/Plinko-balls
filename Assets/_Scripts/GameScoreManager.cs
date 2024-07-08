using UnityEngine;
using TMPro;

public class GameScoreManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    private int _winScore;
    public int Score;

    private void Awake()
    {
        Score = PlayerPrefs.GetInt("GameScore", 0);
        _winScore = 0;
        _scoreText.text = Score.ToString();
    }

    public void UpdateScoreText(int factor)
    {
        _winScore = factor * 100;
        Score += _winScore;
        _scoreText.text = Score.ToString();
        PlayerPrefs.SetInt("GameScore", Score);
    }
}