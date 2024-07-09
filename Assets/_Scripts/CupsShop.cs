using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CupsShop : MonoBehaviour
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
    private AudioVibrationManager _audioVibrationManager;

    private void Start()
    {
        _audioVibrationManager = GetComponent<AudioVibrationManager>();
        playerMoney = PlayerPrefs.GetInt("GameScore", 0);
        _gameScoreText.text = playerMoney.ToString();
        if (!PlayerPrefs.HasKey("SelectedSkinCup"))
        {
            PlayerPrefs.SetInt("SelectedSkinCup", 0);
            PlayerPrefs.SetInt("0cup", 1);
        }

        foreach (var skin in skins)
        {
            if (PlayerPrefs.GetInt(skin.buttonName + "cup", 0) == 1)
            {
                skin.button.onClick.AddListener(() => SelectSkin(skin.buttonName));
                if (PlayerPrefs.GetInt("SelectedSkinCup") == int.Parse(skin.buttonName))
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
            _audioVibrationManager.PlayPurchaseFeedback();
            playerMoney -= skin.skinPrice;
            PlayerPrefs.SetInt("GameScore", playerMoney);
            _gameScoreText.text = playerMoney.ToString();
            PlayerPrefs.SetInt(skin.buttonName + "cup", 1);
            skin.buttonText.text = "Select";
            skin.button.onClick.RemoveAllListeners();
            skin.button.onClick.AddListener(() => SelectSkin(skin.buttonName));
            skin.notEnoughText.text = "";
            SelectSkin(skin.buttonName);
            PlayerPrefs.SetInt("SelectedSkinCup", int.Parse(skin.buttonName));
        }
        else
        {
            _audioVibrationManager.PlayClickFeedback();
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
                _audioVibrationManager.PlayClickFeedback();
                skin.button.image.color = new Color(0.2f, 1.0f, 0);
                skin.buttonText.text = "Selected";
                PlayerPrefs.SetInt("SelectedSkinCup", int.Parse(skinName));
            }
            else if (PlayerPrefs.GetInt(skin.buttonName + "cup", 0) == 1)
            {
                skin.button.image.color = Color.white;
                skin.buttonText.text = "Select";
            }
        }
    }
}
