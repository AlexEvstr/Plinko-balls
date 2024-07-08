using UnityEngine;

public class LevelController : MonoBehaviour
{
    public int CurrentLevel;
    private int _bestLevel;

    private void Awake()
    {
        CurrentLevel = PlayerPrefs.GetInt("currentLevel", 1);
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