using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSkins : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _ball;
    [SerializeField] private Sprite[] _ballSprites;

    private void Start()
    {
        _ball.sprite = _ballSprites[PlayerPrefs.GetInt("SelectedSkinBall", 0)];
    }
}
