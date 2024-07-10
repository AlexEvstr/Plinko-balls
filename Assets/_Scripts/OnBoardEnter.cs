using UnityEngine;

public class OnBoardEnter : MonoBehaviour
{
    [SerializeField] private GameObject _onBoard_1;
    [SerializeField] private GameObject _onBoard_2;
    [SerializeField] private GameObject _onBoard_3;
    [SerializeField] private GameObject _onBoard_4;
    [SerializeField] private GameObject _menuPanel;

    private void Start()
    {
        string entered = PlayerPrefs.GetString("IsItFirstEnter", "");
        if (entered == "")
        {
            _menuPanel.SetActive(false);
            _onBoard_1.SetActive(true);
        }
    }

    public void Open2Board()
    {
        _onBoard_1.SetActive(false);
        _onBoard_2.SetActive(true);
    }

    public void Open3Board()
    {
        _onBoard_2.SetActive(false);
        _onBoard_3.SetActive(true);
    }

    public void Open4Board()
    {
        _onBoard_3.SetActive(false);
        _onBoard_4.SetActive(true);
    }

    public void OpenGame()
    {
        _onBoard_4.SetActive(false);
        _menuPanel.SetActive(true);
        PlayerPrefs.SetString("IsItFirstEnter", "nope");
    }
}