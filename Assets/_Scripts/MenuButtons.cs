using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour
{
    [SerializeField] private GameObject _startButton;
    [SerializeField] private GameObject _settingsButton;
    [SerializeField] private GameObject _shopButton;
    [SerializeField] private GameObject _backFromShopButton;
    [SerializeField] private GameObject _backFromSettingsButton;

    [SerializeField] private GameObject _menuPanel;
    [SerializeField] private GameObject _settingsPanel;
    [SerializeField] private GameObject _shopPanel;

    private void Start()
    {
        Time.timeScale = 1;
        _startButton.GetComponent<Button>().onClick.AddListener(StartGameBtn);
        _settingsButton.GetComponent<Button>().onClick.AddListener(SettingsButton);
        _shopButton.GetComponent<Button>().onClick.AddListener(ShopButton);
        _backFromShopButton.GetComponent<Button>().onClick.AddListener(BackFromShopButton);
        _backFromSettingsButton.GetComponent<Button>().onClick.AddListener(BackFromSettingsButton);
    }

    public void StartGameBtn()
    {
        StartCoroutine(StartGameBehavior());
    }

    private IEnumerator StartGameBehavior()
    {
        _startButton.transform.localScale = new Vector2(1.1f, 1.1f);
        yield return new WaitForSeconds(0.1f);
        _startButton.transform.localScale = new Vector2(1f, 1f);
        yield return new WaitForSeconds(0.25f);
        SceneManager.LoadScene("GameScene");
    }

    public void SettingsButton()
    {
        StartCoroutine(SettingsButtonBehavior());
    }

    private IEnumerator SettingsButtonBehavior()
    {
        _settingsButton.transform.localScale = new Vector2(1.1f, 1.1f);
        yield return new WaitForSeconds(0.1f);
        _settingsButton.transform.localScale = new Vector2(1f, 1f);
        yield return new WaitForSeconds(0.25f);
        _menuPanel.SetActive(false);
        _settingsPanel.SetActive(true);
    }

    public void ShopButton()
    {
        StartCoroutine(ShopButtonBehavior());
    }

    private IEnumerator ShopButtonBehavior()
    {
        _shopButton.transform.localScale = new Vector2(1.1f, 1.1f);
        yield return new WaitForSeconds(0.1f);
        _shopButton.transform.localScale = new Vector2(1f, 1f);
        yield return new WaitForSeconds(0.25f);
        _menuPanel.SetActive(false);
        _shopPanel.SetActive(true);
    }

    public void BackFromShopButton()
    {
        StartCoroutine(BackFromShopButtonBehavior());
    }

    private IEnumerator BackFromShopButtonBehavior()
    {
        _backFromShopButton.transform.localScale = new Vector2(1.1f, 1.1f);
        yield return new WaitForSeconds(0.1f);
        _backFromShopButton.transform.localScale = new Vector2(1f, 1f);
        yield return new WaitForSeconds(0.25f);
        _shopPanel.SetActive(false);
        _menuPanel.SetActive(true);
    }

    public void BackFromSettingsButton()
    {
        StartCoroutine(BackFromSettingsButtonBehavior());
    }

    private IEnumerator BackFromSettingsButtonBehavior()
    {
        _backFromSettingsButton.transform.localScale = new Vector2(1.1f, 1.1f);
        yield return new WaitForSeconds(0.1f);
        _backFromSettingsButton.transform.localScale = new Vector2(1f, 1f);
        yield return new WaitForSeconds(0.25f);
        _settingsPanel.SetActive(false);
        _menuPanel.SetActive(true);
    }
}