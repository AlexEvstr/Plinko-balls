using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BackgroundShop : MonoBehaviour
{
    [System.Serializable]
    public class Skin
    {
        public string buttonName;
        public int skinPrice;
        public Button button;
        public TMP_Text buttonText;
        public TMP_Text notEnoughText;
    }

    public Skin[] skins;
    private int playerMoney;
    [SerializeField] private TMP_Text _gameScoreText;
    [SerializeField] private Image _currentBackground;
    [SerializeField] private Sprite[] _backgroundSprites;
    //private MenuEffects _menuEffects;

    private void Start()
    {
        //_menuEffects = GetComponent<MenuEffects>();
        playerMoney = PlayerPrefs.GetInt("GameScore", 0);
        _gameScoreText.text = playerMoney.ToString();
        if (!PlayerPrefs.HasKey("SelectedSkinBackground"))
        {
            PlayerPrefs.SetInt("SelectedSkinBackground", 0);
            PlayerPrefs.SetInt("0background", 1);
        }

        foreach (var skin in skins)
        {
            if (PlayerPrefs.GetInt(skin.buttonName + "background", 0) == 1)
            {
                skin.button.onClick.AddListener(() => SelectSkin(skin.buttonName));
                if (PlayerPrefs.GetInt("SelectedSkinBackground") == int.Parse(skin.buttonName))
                {
                    skin.buttonText.text = "Selected";
                    skin.button.image.color = new Color(0.2f, 1.0f, 0);
                }
                else
                {
                    skin.buttonText.text = "Select";
                }
            }
            else
            {
                skin.buttonText.text = $"Buy ({skin.skinPrice})";
                skin.button.onClick.AddListener(() => BuySkin(skin));
            }
        }
    }

    private void BuySkin(Skin skin)
    {
        playerMoney = PlayerPrefs.GetInt("GameScore", 0);
        if (playerMoney >= skin.skinPrice)
        {
            //_menuEffects.PlayPurchaseSound();
            playerMoney -= skin.skinPrice;
            PlayerPrefs.SetInt("GameScore", playerMoney);
            _gameScoreText.text = playerMoney.ToString();
            PlayerPrefs.SetInt(skin.buttonName + "background", 1);
            skin.buttonText.text = "Select";
            skin.button.onClick.RemoveAllListeners();
            skin.button.onClick.AddListener(() => SelectSkin(skin.buttonName));
            skin.notEnoughText.text = "";
            SelectSkin(skin.buttonName);
            PlayerPrefs.SetInt("SelectedSkinBackground", int.Parse(skin.buttonName));
        }
        else
        {
            //_menuEffects.PlayClickSound();
            StartCoroutine(ShowNoteEnoughText(skin));
        }
    }

    private IEnumerator ShowNoteEnoughText(Skin skin)
    {
        skin.notEnoughText.text = "Not enough money!";
        yield return new WaitForSeconds(1.0f);
        skin.notEnoughText.text = "";

    }

    private void SelectSkin(string skinName)
    {
        foreach (var skin in skins)
        {
            if (skin.buttonName == skinName)
            {
                //_menuEffects.PlayClickSound();
                _currentBackground.sprite = _backgroundSprites[int.Parse(skinName)];
                skin.button.image.color = new Color(0.2f, 1.0f, 0);
                skin.buttonText.text = "Selected";
                PlayerPrefs.SetInt("SelectedSkinBackground", int.Parse(skinName));
            }
            else if (PlayerPrefs.GetInt(skin.buttonName + "background", 0) == 1)
            {
                skin.button.image.color = Color.white;
                skin.buttonText.text = "Select";
            }
        }
    }
}
