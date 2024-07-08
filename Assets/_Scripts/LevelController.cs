using UnityEngine;
using TMPro;

public class LevelController : MonoBehaviour
{
    [SerializeField] private TMP_Text _levelText;
    public int CurrentLevel;
    private int _bestLevel;

    private void Awake()
    {
        CurrentLevel = PlayerPrefs.GetInt("currentLevel", 1);
        _levelText.text = $"Level: {CurrentLevel}";
        _bestLevel = PlayerPrefs.GetInt("bestLevel", 1);
    }

    public void IncreaseCurrentLevel()
    {
        CurrentLevel++;
        PlayerPrefs.SetInt("currentLevel", CurrentLevel);
        if (CurrentLevel > _bestLevel)
        {
            _bestLevel = CurrentLevel;
            PlayerPrefs.SetInt("bestLevel", _bestLevel);
        }
    }
}