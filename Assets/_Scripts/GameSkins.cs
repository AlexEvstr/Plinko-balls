using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSkins : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _ball;
    [SerializeField] private Sprite[] _ballSprites;
    [SerializeField] private Image _backgroundImage;
    [SerializeField] private Sprite[] _backgrounds;
    [SerializeField] private SpriteRenderer[] _cups;
    [SerializeField] private Sprite[] _cupsSprites;

    private void Start()
    {
        _ball.sprite = _ballSprites[PlayerPrefs.GetInt("SelectedSkinBall", 0)];
        _backgroundImage.sprite = _backgrounds[PlayerPrefs.GetInt("SelectedSkinBackground", 0)];
        foreach (var cup in _cups)
        {
            cup.sprite = _cupsSprites[PlayerPrefs.GetInt("SelectedSkinCup", 0)];
        }
    }
}
